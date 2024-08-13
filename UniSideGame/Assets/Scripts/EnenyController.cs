using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnenyController : MonoBehaviour
{
    public float speed = 3.0f;
    public string direction = "left";
    public float range = 0.0f;
    private Vector3 defPos;

    private Rigidbody2D rb;

    // =====================================================

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (direction == "right")
        {
            transform.localScale = new Vector2(-1, 1);
        }

        // 시작 위치
        defPos = transform.position;
    }

    private void Update()
    {
        if (range > 0.0f)
        {
            if (transform.position.x < defPos.x - (range / 2))
            {
                direction = "right";
                transform.localScale = new Vector2(-1, 1);  // 방향 변경
            }
            else if (transform.position.x > defPos.x + (range / 2))
            {
                direction = "left";
                transform.localScale = new Vector2(1, 1);   // 방향 변경
            }
        }
    }

    private void FixedUpdate()
    {
        // 속도 갱신
        if (direction == "right")
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }
    }

    // 접촉
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (direction == "right")
        {
            direction = "left";
            transform.localScale = new Vector2(1, 1);   // 방향 변경
        }
        else
        {
            direction = "right";
            transform.localScale = new Vector2(-1, 1);  // 방향 변경
        }
    }
}
