using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerContriller : MonoBehaviour
{
    // 번외
    private SpriteRenderer sr;

    private Rigidbody2D rb;     // Rigidbody2D형 변수
    private float axisH = 0.0f;   // 입력
    public float speed = 3.0f;   // 이동 속도

    private void Start()
    {
        // Rigidbody2D 가져오기
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // 수평 방향으로의 입력 확인
        axisH = Input.GetAxis("Horizontal");

        // 방향 조절
        if (axisH > 0)
        {
            sr.flipX = false;
            // 오른쪽 이동
            // transform.localScale = new Vector3(1, 1);
        }
        else if (axisH < 0)
        {
             sr.flipX = true;
            // 왼쪽 이동
            // transform.localScale = new Vector3(-1, 1);
        }
    }

    private void FixedUpdate()
    {
        // 속도 갱신하기
        rb.velocity = new Vector2(speed * axisH, rb.velocity.y);
    }
}
