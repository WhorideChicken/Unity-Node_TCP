using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameNotification;
using Google.Protobuf;
using Common;
using Game;
using Response;
using Packets;

public class Spawner : Singleton<Spawner>
{
    private HashSet<string> currentUsers = new HashSet<string>();

    // LocationUpdate 데이터를 바탕으로 사용자 오브젝트 업데이트
    public void Spawn(LocationUpdate data)
    {
        if (!GameManager.Instance.isLive)
        {
            return;
        }

        HashSet<string> newUsers = new HashSet<string>();

        // data.Users에 포함된 각 사용자 위치 정보 업데이트
        foreach (LocationUpdate.Types.UserLocation user in data.Users) // user 데이터 접근
        {
            newUsers.Add(user.Id);

            // 사용자가 이미 존재하는 경우 위치만 업데이트
            GameObject player = GameManager.Instance.pool.Get(user);
            if (player != null)
            {
                PlayerPrefab playerScript = player.GetComponent<PlayerPrefab>();
                playerScript.UpdatePosition(user.X, user.Y);
            }
        }

        // 기존 사용자 중 새로운 사용자 목록에 없는 사용자 제거
        foreach (string userId in currentUsers)
        {
            if (!newUsers.Contains(userId))
            {
                GameManager.Instance.pool.Remove(userId); // 사용자 제거
            }
        }

        // 현재 사용자 목록을 업데이트
        currentUsers = newUsers;
    }
}