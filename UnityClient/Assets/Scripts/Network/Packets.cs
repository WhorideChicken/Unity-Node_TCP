namespace Packets
{
    public enum HandlerIds : uint
    {
        Init = 0,              // 초기화 요청
        LocationUpdate = 1,    // 위치 업데이트 요청
        Ping = 2               // Ping 요청
    }

    public enum PacketType : byte
    {
        Normal = 1,
        Location = 2,
        Ping = 3
    }
}