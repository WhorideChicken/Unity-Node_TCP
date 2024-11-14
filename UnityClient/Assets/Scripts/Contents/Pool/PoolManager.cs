using GameNotification;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using Common;
using Game;
using Response;

public class PoolManager : MonoBehaviour
{
    // 프리펩을 보관할 변수
    public GameObject[] prefabs;

    // 풀 담당 하는 리스트들
    private List<GameObject> pool = new List<GameObject>();

    // 유저를 관리할 딕셔너리
    Dictionary<string, GameObject> userDictionary = new Dictionary<string, GameObject>();


    public GameObject CreatePlayer(string id, float x, float y)
    {
        Debug.Log("호출 되었습니다");
        var newPlayer = Instantiate(prefabs[0], new Vector3(x, y), Quaternion.identity);
        if (newPlayer == null)
        {
            Debug.LogError("Failed to instantiate new player prefab.");
            return null;
        }

        newPlayer.GetComponent<PlayerPrefab>().Init(0, id);
        pool.Add(newPlayer);
        userDictionary[id] = newPlayer;
        return newPlayer;
    }

    public GameObject Get(LocationUpdate.Types.UserLocation user)
    {
        if (userDictionary.TryGetValue(user.Id, out var existingUser))
            return existingUser;

        var newPlayer = Instantiate(prefabs[0], transform);
        newPlayer.GetComponent<PlayerPrefab>().Init(user.PlayerId, user.Id);
        pool.Add(newPlayer);
        userDictionary[user.Id] = newPlayer;
        return newPlayer;
    }

    public void Remove(string userId)
    {
        if (userDictionary.TryGetValue(userId, out var userObject))
        {
            userObject.SetActive(false);
            userDictionary.Remove(userId);
        }
    }
}
