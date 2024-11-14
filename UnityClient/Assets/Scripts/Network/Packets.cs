using UnityEngine;
using Google.Protobuf;
using System.IO;
using System.Buffers;
using System.Collections.Generic;
using System;
using System.Text;

public class Packets
{
    public enum PacketType
    {
        Normal,
        Ping,
        Location = 3
    }

    public enum HandlerIds
    {
        Init = 0,
        LocationUpdate = 2
    }

    // ProtoBuf 기반의 직렬화 메서드
    public static void Serialize<T>(IBufferWriter<byte> writer, T data) where T : IMessage
    {
        data.WriteTo(writer);
    }

    // ProtoBuf 기반의 역직렬화 메서드
    public static T Deserialize<T>(byte[] data) where T : IMessage<T>, new()
    {
        try
        {
            return new MessageParser<T>(() => new T()).ParseFrom(data);
        }
        catch (Exception ex)
        {
            Debug.LogError($"Deserialize: Failed to deserialize data. Exception: {ex}");
            throw;
        }
    }
}