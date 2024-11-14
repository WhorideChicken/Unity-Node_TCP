using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameNotification;
using Google.Protobuf;
using Common;
using Game;
using Response;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Spawner : Singleton<Spawner>
{
    private HashSet<string> currentUsers = new HashSet<string>();

    public void SpawnInitialPlayer(string userId, float x, float y)
    {
        if (GameManager.Instance.pool == null)
        {
            Debug.LogError("PoolManager instance is not set in GameManager. Ensure it is initialized.");
            return;
        }

        if (currentUsers.Contains(userId))
        {
            Debug.Log("User already exists; skipping duplicate spawn.");
            return;
        }

        GameObject player = GameManager.Instance.pool.CreatePlayer(userId, x, y);
        if (player != null)
        {
            var playerScript = player.GetComponent<PlayerPrefab>();
            playerScript.Init(0, userId);
            currentUsers.Add(userId); // 현재 사용자 목록에 자신의 유저 ID 추가
        }
        else
        {
            Debug.LogError("Failed to create player in SpawnInitialPlayer.");
        }
    }


    public void Spawn(LocationUpdate data)
    {
        HashSet<string> newUsers = new HashSet<string>();

        // 자신의 유저 ID를 새로운 사용자 목록에 추가하여 삭제되지 않도록 함
        newUsers.Add(GameManager.Instance.deviceId);

        foreach (var user in data.Users)
        {
            // 자신의 유저 ID는 건너뜁니다.
            if (user.Id == GameManager.Instance.deviceId)
            {
                continue;
            }

            newUsers.Add(user.Id);

            GameObject player = GameManager.Instance.pool.Get(user);
            if (player != null)
            {
                var playerScript = player.GetComponent<PlayerPrefab>();
                playerScript.UpdatePosition(user.X, user.Y);
            }
        }

        // 기존 사용자 중 새로운 사용자 목록에 없는 사용자 제거
        foreach (string userId in currentUsers)
        {
            if (!newUsers.Contains(userId))
            {
                GameManager.Instance.pool.Remove(userId);
            }
        }

        // 현재 사용자 목록을 업데이트
        currentUsers = newUsers;
    }
}