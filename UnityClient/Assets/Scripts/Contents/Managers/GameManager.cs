using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class GameManager : Singleton<GameManager>
{
    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public int targetFrameRate;
    public string version = "1.0.0";
    public int latency = 2;

    [Header("# Player Info")]
    public uint playerId;
    public string deviceId;

    [Header("# Game Object")]
    public Player player;
    public PoolManager pool;

    public override void Awake()
    {
        base.Awake();
        playerId = (uint)Random.Range(0, 4);
    }

    public void GameStart()
    {
        player.deviceId = deviceId;
        isLive = true;
    }
}
