using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Google.Protobuf;
using Common;
using Game;
using GameNotification;
using Response;
using System.Linq;
using static UnityEditor.U2D.ScriptablePacker;
using static Packets;

public class NetworkManager : Singleton<NetworkManager>
{
    private TcpClient tcpClient;
    private NetworkStream stream;
    private WaitForSecondsRealtime wait = new WaitForSecondsRealtime(5);
    private byte[] receiveBuffer = new byte[4096];
    private List<byte> incompleteData = new List<byte>();

    private bool playerSpawned = false;
    
    public void StartConnect(string ip, string port)
    {
        if (IsValidPort(port))
        {
            int portNumber = int.Parse(port);
            GameManager.Instance.deviceId = GenerateUniqueID();

            if (ConnectToServer(ip, portNumber))
            {
                StartGame();
            }
        }
    }

    bool IsValidPort(string port) => int.TryParse(port, out int portNumber) && portNumber > 0 && portNumber <= 65535;

    bool ConnectToServer(string ip, int port)
    {
        try
        {
            tcpClient = new TcpClient(ip, port);
            stream = tcpClient.GetStream();
            Debug.Log($"Connected to {ip}:{port}");
            return true;
        }
        catch (SocketException e)
        {
            Debug.LogError($"SocketException: {e}");
            return false;
        }
    }

    string GenerateUniqueID() => Guid.NewGuid().ToString();

    void StartGame()
    {
        Debug.Log("Game Started");
        StartReceiving();
        SendInitialPacket();
    }

    async void SendPacket<T>(T payload, uint handlerId) where T : IMessage<T>
    {
        byte[] payloadData = payload.ToByteArray();

        var commonPacket = new CommonPacket
        {
            HandlerId = handlerId,
            UserId = GameManager.Instance.deviceId,
            Version = GameManager.Instance.version,
            Payload = ByteString.CopyFrom(payloadData)
        };

        byte[] data = commonPacket.ToByteArray(); // 직렬화된 CommonPacket 생성

        // 패킷 타입 설정 확인
        PacketType packetType = handlerId switch
        {
            (uint)HandlerIds.Init => PacketType.Normal,
            (uint)HandlerIds.LocationUpdate => PacketType.Location,
            _ => PacketType.Ping // 기본 값
        };

        // 패킷 헤더 생성 및 결합
        byte[] packetHeader = CreatePacketHeader(data.Length, packetType);
        byte[] packet = new byte[packetHeader.Length + data.Length];

        Array.Copy(packetHeader, 0, packet, 0, packetHeader.Length);
        Array.Copy(data, 0, packet, packetHeader.Length, data.Length);

        await Task.Delay(GameManager.Instance.latency);

        Debug.Log($"Sending packet of length {packet.Length}");

        stream.Write(packet, 0, packet.Length); // 전체 패킷 전송
    }

    byte[] CreatePacketHeader(int dataLength, PacketType packetType)
    {
        int packetLength = 4 + 1 + dataLength;
        byte[] header = new byte[5];
        byte[] lengthBytes = BitConverter.GetBytes(packetLength);

        if (BitConverter.IsLittleEndian) Array.Reverse(lengthBytes);
        Array.Copy(lengthBytes, 0, header, 0, 4);
        header[4] = (byte)packetType;

        return header;
    }

    void SendInitialPacket()
    {
        var initialPacket = new InitialPacket
        {
            DeviceId = GameManager.Instance.deviceId,
            PlayerId = GameManager.Instance.playerId,
            Latency = GameManager.Instance.latency,
        };

        Debug.Log(GameManager.Instance.deviceId);
        SendPacket(initialPacket, (uint)HandlerIds.Init);
    }

    public void SendLocationUpdatePacket(float x, float y, float inputX, float inputY)
    {
        var locationUpdatePayload = new LocationUpdatePayload
        {
            X = x,
            Y = y,
            InputX = inputX,
            InputY = inputY
        };
        Debug.Log("Sending LocationUpdatePacket to server.");
        SendPacket(locationUpdatePayload, (uint)HandlerIds.LocationUpdate);
    }

    void StartReceiving() => _ = ReceivePacketsAsync();

    async Task ReceivePacketsAsync()
    {
        while (tcpClient.Connected)
        {
            try
            {
                int bytesRead = await stream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length);
                if (bytesRead > 0) ProcessReceivedData(receiveBuffer, bytesRead);
            }
            catch (Exception e)
            {
                Debug.LogError($"Receive error: {e.Message}");
                break;
            }
        }
    }


    void ProcessReceivedData(byte[] data, int length)
    {
        incompleteData.AddRange(data.AsSpan(0, length).ToArray());

        while (true)
        {
            if (incompleteData.Count < 5)
            {
                // 패킷 헤더를 읽을 수 있을 만큼 데이터가 도착하지 않음
                break;
            }

            // 패킷 길이 및 타입 읽기
            byte[] lengthBytes = incompleteData.GetRange(0, 4).ToArray();
            if (BitConverter.IsLittleEndian) Array.Reverse(lengthBytes);
            int packetLength = BitConverter.ToInt32(lengthBytes, 0);

            if (packetLength <= 0)
            {
                Debug.LogError("Invalid packet length received.");
                incompleteData.Clear();
                break;
            }

            if (incompleteData.Count < packetLength)
            {
                // 전체 패킷이 도착하지 않음
                Debug.Log("Waiting for more data to complete the packet...");
                break;
            }

            // 패킷 데이터 추출 및 처리
            byte packetTypeByte = incompleteData[4];
            PacketType packetType = (PacketType)packetTypeByte;
            byte[] packetData = incompleteData.GetRange(5, packetLength - 5).ToArray();
            incompleteData.RemoveRange(0, packetLength);

            try
            {
                switch (packetType)
                {
                    case PacketType.Normal:
                        HandleNormalPacket(packetData);
                        break;
                    case PacketType.Location:
                        HandleLocationPacket(packetData);
                        break;
                    case PacketType.Ping:
                        HandlePingPacket(packetData);
                        break;
                    default:
                        Debug.LogWarning($"Unknown packet type: {packetType}");
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error handling packet of type {packetType}: {ex.Message}");
            }
        }
    }

    void HandleNormalPacket(byte[] packetData)
    {
        try
        {
            var response = Response.Response.Parser.ParseFrom(packetData);
            Debug.Log($"Response Code: {response.ResponseCode}");
            Debug.Log($"Handler ID: {response.HandlerId}");
            Debug.Log($"Data Length: {response.Data.Length}");

            if (response.ResponseCode != 0)
            {
                Debug.LogWarning("Response code is not zero, skipping packet handling.");
                return;
            }

            if (response.Data == null || response.Data.Length == 0)
            {
                Debug.LogWarning("Response data is empty.");
                return;
            }

            switch ((HandlerIds)response.HandlerId)
            {
                case HandlerIds.Init:
                    var initialResponse = InitialResponse.Parser.ParseFrom(response.Data.ToByteArray());
                    if (initialResponse != null)
                    {
                        HandleInitialResponse(initialResponse);
                    }
                    else
                    {
                        Debug.LogError("InitialResponse is null after parsing.");
                    }
                    break;
                default:
                    Debug.LogWarning("Unhandled handler ID in Normal Packet");
                    break;
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error handling packet of type Normal: {e.Message}");
        }
    }

    void ProcessResponseData(ByteString data)
    {
        try
        {
            string jsonString = Encoding.UTF8.GetString(data.ToByteArray());
            Debug.Log($"Processed SpecificDataType: {jsonString}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error processing response data: {e.Message}");
        }
    }

    void HandleLocationPacket(byte[] packetData)
    {
        try
        {
            var commonPacket = CommonPacket.Parser.ParseFrom(packetData);
            var locationUpdate = LocationUpdate.Parser.ParseFrom(commonPacket.Payload);

            // 위치 업데이트를 Spawner에서 처리
            Debug.Log("update");
            Spawner.Instance.Spawn(locationUpdate);
        }
        catch (Exception e)
        {
            Debug.LogError($"Error decoding LocationUpdate packet: {e.Message}");
        }
    }

    async void HandlePingPacket(byte[] data)
    {
        // Ping 응답
        var ping = Common.Ping.Parser.ParseFrom(data);
        Debug.Log($"Ping received: {ping.Timestamp}");
        var pingResponse = new Common.Ping { Timestamp = ping.Timestamp };
        byte[] packet = pingResponse.ToByteArray();

        await Task.Delay(GameManager.Instance.latency);
        stream.Write(packet, 0, packet.Length);
    }

    void HandleInitialResponse(InitialResponse initialResponse)
    {
        if (playerSpawned)
        {
            Debug.Log("Player has already been spawned; skipping duplicate spawn.");
            return;
        }
        Debug.Log($"Initial Response: Game ID: {initialResponse.GameId}, Position: ({initialResponse.X}, {initialResponse.Y})");
        Spawner.Instance.SpawnInitialPlayer(GameManager.Instance.deviceId, initialResponse.X, initialResponse.Y);
        playerSpawned = true;

    }
}
