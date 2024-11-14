using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefab : MonoBehaviour
{
    public RuntimeAnimatorController[] animCon;
    private Animator anim;
    private SpriteRenderer spriter;
    private Vector3 lastPosition;
    private Vector3 currentPosition;
    private uint playerId;
    TextMeshPro myText;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        myText = GetComponentInChildren<TextMeshPro>();
    }


    public void Init(uint playerId, string id)
    {
        anim.runtimeAnimatorController = animCon[playerId];
        myText.text = id.Length > 5 ? id[..5] : id;
        myText.GetComponent<MeshRenderer>().sortingOrder = 6;
    }

    void OnEnable()
    {    
        anim.runtimeAnimatorController = animCon[playerId];
    }

    // 서버로부터 위치 업데이트를 수신할 때 호출될 메서드
    public void UpdatePosition(float x, float y)
    {
        lastPosition = transform.position;
        transform.position = new Vector3(x, y);
        UpdateAnimation();
    }

    void LateUpdate()
    {
        if (!GameManager.Instance.isLive)
        {
            return;
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        Vector2 direction = (Vector2)(transform.position - lastPosition);
        anim.SetFloat("Speed", direction.magnitude);
        spriter.flipX = direction.x < 0;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.Instance.isLive)
        {
            return;
        }
    }


}
