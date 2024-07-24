using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;



public class PlayerContriller : MonoBehaviour
{
    [Header("이동")]
    private float axisH = 0.0f;   // 입력
    public float speed = 3.0f;   // 이동 속도

    [Header("점프")]
    public float jump = 9.0f;                   // 점프력
    public LayerMask groundLayer;      // 착지할 수 있는 레이어
    private bool goJump = false;            // 점프 개시 플래그
    private bool onGround = false;          // 지면에 서 있는 플래그

    private Rigidbody2D rb;     // Rigidbody2D형 변수
    private SpriteRenderer sr;

    [Header("애니메이션")]
    private Animator anim;  // Animator
    public string stopAnime = "PlayerStop";
    public string moveAnime = "PlayerMove";
    public string jumpAnime = "PlayerJump";
    public string goalAnime = "PlayerGoal";
    public string deadAnime = "PlayerDead";
    private string nowAnime = "";
    private string oldAnime = "";

    public static string gameState = "playing";     // 게임 상태    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        nowAnime = stopAnime;
        oldAnime = stopAnime;
        gameState = "playing";
    }

    private void Update()
    {
        if (gameState != "playing")
        {
            return;
        }

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

        // 캐릭터 점프하기
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if(gameState != "playing")
        {
            return;
        }

        // 착지 판정
        onGround = Physics2D.Linecast(transform.position, transform.position - (transform.up * 0.1f), groundLayer);

        if (onGround || axisH != 0)
        {
            // 지면 위 or 속도가 0 아님
            // 속도 갱신하기
            rb.velocity = new Vector2(speed * axisH, rb.velocity.y);
        }
        if (onGround && goJump)
        {
            // 지면 위에서 점프 키 눌림
            // 점프하기
            Vector2 jumpPw = new Vector2(0, jump);  // 점프를 위한 벡터 생성
            rb.AddForce(jumpPw, ForceMode2D.Impulse);  // 순간적인 힘 가하기
            goJump = false;  // 점프 플래그 끄기
        }

        if (onGround)
        {
            if (axisH == 0)
            {
                nowAnime = stopAnime;   // 정지
            }
            else
            {
                nowAnime = moveAnime;   // 이동
            }
        }
        else
        {
            // 공중
            nowAnime = jumpAnime;   // 점프
        }

        if (nowAnime != oldAnime)
        {
            oldAnime = nowAnime;
            anim.Play(nowAnime);    // 애니메이션 재생
        }


    }

    public void Jump()
    {
        goJump = true;  // 점프 플래그 키기
    }

    // 접촉 시작
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Goal"))    // gameObject.tag == "Goal" 도 가능 하지만 비추천
        {
            Goal();
        }
        else if (collision.CompareTag("Dead"))
        {
            GameOver();
        }
    }

    // 끝
    public void Goal()
    {
        anim.Play(goalAnime);
        gameState = "gameclear";
        GameStop();
    }

    // 게임 오버
    public void GameOver()
    {
       anim.Play(deadAnime);

        gameState = "gameover";
        GameStop();
        // ==================== 
        // 게임 오버 처리
        // ====================
        // 플레이어의 충돌 판정 비활성
        GetComponent<CapsuleCollider2D>().enabled = false;
        // 플레이어를 위로 튀어 오르게 하는 연출
        rb.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);
    }

    private void GameStop()
    {
        // Rigidbody2D 가져오기
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        // 속도를 0으로 하여 강제 정지
        rb.velocity = new Vector2(0, 0);
    }
}
