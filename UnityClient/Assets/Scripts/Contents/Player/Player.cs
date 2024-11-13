using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed;
    public string deviceId;
    public RuntimeAnimatorController[] animCon;

    private Rigidbody2D rigid;
    private SpriteRenderer spriter;
    private Animator anim;
    private TextMeshPro myText;

    private Vector2 targetPosition; // 서버로부터 받은 목표 위치를 저장할 변수
    private bool isTargetPositionSet = false; // 서버에서 목표 위치를 받은 상태인지

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        myText = GetComponentInChildren<TextMeshPro>();
    }

    void OnEnable()
    {
        if (deviceId.Length > 5)
        {
            myText.text = deviceId[..5];
        }
        else
        {
            myText.text = deviceId;
        }
        myText.GetComponent<MeshRenderer>().sortingOrder = 6;

        anim.runtimeAnimatorController = animCon[GameManager.Instance.playerId];
    }

    void Update()
    {
        if (!GameManager.Instance.isLive)
        {
            return;
        }

        // 입력값 받기
        inputVec.x = Input.GetAxisRaw("Horizontal");
        inputVec.y = Input.GetAxisRaw("Vertical");

        // 위치 이동 패킷 전송 -> 서버로
        NetworkManager.Instance.SendLocationUpdatePacket(rigid.position.x, rigid.position.y, inputVec.x, inputVec.y);
    }

    void FixedUpdate()
    {
        if (!GameManager.Instance.isLive)
        {
            return;
        }

        if (isTargetPositionSet)
        {
            // 서버로부터 받은 목표 위치로 이동
            rigid.MovePosition(targetPosition);
            isTargetPositionSet = false; // 목표 위치로 이동 후 상태 초기화
        }
        else
        {
            // 입력에 따른 이동
            Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
        }
    }

    void LateUpdate()
    {
        if (!GameManager.Instance.isLive)
        {
            return;
        }

        anim.SetFloat("Speed", inputVec.magnitude);

        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!GameManager.Instance.isLive)
        {
            return;
        }
    }

    // 서버에서 받은 위치 업데이트를 반영
    public void UpdatePositionFromServer(float x, float y)
    {
        targetPosition = new Vector2(x, y);
        isTargetPositionSet = true;
    }
}
