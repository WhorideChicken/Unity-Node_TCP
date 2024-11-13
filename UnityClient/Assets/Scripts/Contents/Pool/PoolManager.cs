using GameNotification;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using Common;
using Game;
using Response;
using Packets;

public class PoolManager : MonoBehaviour
{
    // 프리펩을 보관할 변수
    public GameObject[] prefabs;

    // 풀 담당 하는 리스트들
    List<GameObject> pool;

    // 유저를 관리할 딕셔너리
    Dictionary<string, GameObject> userDictionary = new Dictionary<string, GameObject>();

    void Awake()
    {
        pool = new List<GameObject>();
    }

    // LocationUpdate.UserLocation 타입의 사용자 정보를 인자로 받음
    public GameObject Get(LocationUpdate.Types.UserLocation user)
    {
        // 유저가 이미 존재하면 해당 유저 반환
        if (userDictionary.TryGetValue(user.Id, out GameObject existingUser))
        {
            return existingUser;
        }

        GameObject select = null;

        // 풀에서 비활성화된 오브젝트 찾기
        foreach (GameObject item in pool)
        {
            if (!item.activeSelf)
            {
                select = item;
                select.GetComponent<PlayerPrefab>().Init(user.PlayerId, user.Id);
                select.SetActive(true);
                userDictionary[user.Id] = select;
                break;
            }
        }

        // 비활성화된 오브젝트를 찾지 못한 경우 새로 생성
        if (select == null)
        {
            select = Instantiate(prefabs[0], transform);
            pool.Add(select);
            select.GetComponent<PlayerPrefab>().Init(user.PlayerId, user.Id);
            userDictionary[user.Id] = select;
        }

        return select;
    }

    public void Remove(string userId)
    {
        if (userDictionary.TryGetValue(userId, out GameObject userObject))
        {
            Debug.Log($"Removing user: {userId}");
            userObject.SetActive(false);
            userDictionary.Remove(userId);
        }
        else
        {
            Debug.Log($"User {userId} not found in dictionary");
        }
    }
}
