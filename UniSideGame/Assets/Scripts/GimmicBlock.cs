using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GimmicBlock : MonoBehaviour
{
    public float length = 0.0f; // 자동 낙하 탐지 거리
    public bool isDelete = false; // 낙하 후 제거할지 여부

    public bool isFell = false;        // 낙하 플래그
    private float fadeTime = 0.5f;  // 페이드 아웃 시간

    private Rigidbody2D rb;
    public GameObject player;

    private void Start()
    {
        // Rigidbody2D 물리 시뮬레이션 정리
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private void Update()
    {
        if (player != null)
        {
            // 플레이어와의 거리 계산
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (length >= distance)
            {
                // Rigidbody2D 물리  시뮬레이션 시작
                rb.bodyType = RigidbodyType2D.Dynamic;
            }
        }

        if (isFell)
        {
            // 낙하
            // 투명도를 변경해 페이드아웃 효과
            fadeTime -= Time.deltaTime; // 이전 프레임과의 차이만큼 시간 차감
            Color color = GetComponent<SpriteRenderer>().color; // 컬러 값 가져오기
            color.a = fadeTime; // 투명도 변경
            GetComponent<SpriteRenderer>().color = color; // 컬러 값을 재설정

            if (fadeTime <= 0)
            {
                // 0보다 작으면 (투명) 제거
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 따로 설정 필요
        if (isDelete)
        {
            isFell = true;  // 낙하 플래그 true
        }
    }
}
