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
using Packets;
using System.Linq;
using static UnityEditor.U2D.ScriptablePacker;

public class NetworkManager : Singleton<NetworkManager>
{
    private TcpClient tcpClient;
    private NetworkStream stream;
    private WaitForSecondsRealtime wait = new WaitForSecondsRealtime(5);
    private byte[] receiveBuffer = new byte[4096];
    private List<byte> incompleteData = new List<byte>();


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

        // CommonPacket 생성
        var commonPacket = new CommonPacket
        {
            HandlerId = handlerId,
            UserId = GameManager.Instance.deviceId,
            Version = GameManager.Instance.version,
            Payload = ByteString.CopyFrom(payloadData)
        };

        byte[] data = commonPacket.ToByteArray(); // CommonPacket 직렬화

        PacketType packetType = handlerId switch
        {
            (uint)HandlerIds.Init => PacketType.Normal,
            (uint)HandlerIds.LocationUpdate => PacketType.Location,
            _ => PacketType.Ping
        };

        // 헤더 생성 및 패킷 결합
        byte[] packetHeader = CreatePacketHeader(data.Length, packetType);
        byte[] packet = new byte[packetHeader.Length + data.Length];

        Array.Copy(packetHeader, 0, packet, 0, packetHeader.Length); // 헤더 추가
        Array.Copy(data, 0, packet, packetHeader.Length, data.Length); // 데이터 추가

        Debug.Log($"Final Packet to Send: {BitConverter.ToString(packet)}"); // 디버깅 로그

        await Task.Delay(GameManager.Instance.latency);

        stream.Write(packet, 0, packet.Length); // 헤더와 데이터 포함한 전체 패킷 전송
    }
    byte[] CreatePacketHeader(int dataLength, PacketType packetType)
    {
        int packetLength = 4 + 1 + dataLength; // 전체 패킷 길이
        byte[] header = new byte[5];
        byte[] lengthBytes = BitConverter.GetBytes(packetLength);

        if (BitConverter.IsLittleEndian) Array.Reverse(lengthBytes); // 엔디안 변환
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

        while (incompleteData.Count >= 5)
        {
            // 패킷 길이와 타입을 확인
            byte[] lengthBytes = incompleteData.GetRange(0, 4).ToArray();
            if (BitConverter.IsLittleEndian) Array.Reverse(lengthBytes);
            int packetLength = BitConverter.ToInt32(lengthBytes, 0);

            // 잘못된 패킷 길이 확인
            if (packetLength <= 5 || packetLength > receiveBuffer.Length)
            {
                Debug.LogError($"Invalid packet length: {packetLength}. Discarding data.");
                incompleteData.Clear(); // 잘못된 데이터는 제거
                return;
            }

            // 5번째 바이트에서 PacketType 확인
            PacketType packetType = (PacketType)incompleteData[4];
            Debug.Log($"Received packet - Type: {packetType}, Length: {packetLength}");

            // 필요한 데이터가 아직 도착하지 않은 경우
            if (incompleteData.Count < packetLength)
            {
                Debug.Log("Waiting for more data to complete the packet...");
                return;
            }

            // 유효한 데이터가 도착한 경우 데이터 추출
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
        var response = Response.Response.Parser.ParseFrom(packetData);
        if (response.ResponseCode != 0) return;

        if (response.Data.Length > 0)
        {
            switch ((HandlerIds)response.HandlerId)
            {
                case HandlerIds.Init:
                    var initialResponse = InitialResponse.Parser.ParseFrom(response.Data.ToByteArray());
                    HandleInitialResponse(initialResponse);
                    break;
            }

            ProcessResponseData(response.Data);
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
            // 1. CommonPacket 디코딩
            var commonPacket = CommonPacket.Parser.ParseFrom(packetData);
            Debug.Log($"Received CommonPacket: {commonPacket}");

            // 2. payload에서 LocationUpdate 디코딩
            var locationUpdate = LocationUpdate.Parser.ParseFrom(commonPacket.Payload);
            Debug.Log($"LocationUpdate received: {locationUpdate}");

            // 3. 위치 동기화 및 사용자 생성/제거
            Spawner.Instance.Spawn(locationUpdate);  // LocationUpdate 데이터를 스폰 처리
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
        Debug.Log($"Initial Response: Game ID: {initialResponse.GameId}, Position: ({initialResponse.X}, {initialResponse.Y})");
    }
}
