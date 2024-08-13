using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private GameObject player;                              // 플레이어
    private PlayerContriller playerContriller;        // PlayerContriller

    public GameObject mainImage;                        // 이미지를 담아두는 GameObject
    public Sprite gameOverSpr;                              // 게임오버 이미지
    public Sprite gameClearSpr;                             // 게임클리어 이미지
    public GameObject panel;                                 // 패널
    public GameObject restartButton;                   // 재시작 버튼
    public GameObject nextButton;                       // 다음 스테이지 버튼

    private Image titleImage;                                   // 이미지를 표시하는 Image 컴포넌트

    // +++ 시간제한 추가 +++
    public GameObject timeBar;                      // 시간 표시 이미지
    public GameObject timeText;                     // 시간 텍스트
    private TimeController timeCount;           // TimeController

    // +++ 점수 추가 +++
    public GameObject scoreText;                    // 점수 표시 텍스트
    public static int totalScore;                           // 점수 총합
    public int stageScore;                                   // 스테이지 점수

    // +++ 사운드 재생 추가 +++
    public AudioClip meGameOver;    //  게임 오버
    public AudioClip meGameClear;   // 게임 클리어
    private AudioSource soundPlayer;

    // ====================================================================================================

    private void Awake()
    {
        soundPlayer = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // 플레이어 찾기
        player = GameObject.FindGameObjectWithTag("Player");
        playerContriller = player.GetComponent<PlayerContriller>();

        // 이미지 숨기기
        Invoke(nameof(InactiveImage), 1.0f);

        // 버튼(패널)을 숨기기
        panel.SetActive(false);

        // +++ 시간제한 추가 +++
        timeCount = GetComponent<TimeController>();
        if (timeCount != null)
        {
            if (timeCount.gameTime == 0)
            {
                timeBar.SetActive(false);
            }
        }

        // +++ 점수 추가 +++
        UpdateScore();
    }

    private void Update()
    {
        if (PlayerContriller.gameState == "gameclear")
        {
            // 게임 클리어
            mainImage.SetActive(true);  // 이미지 표시
            panel.SetActive(true);          // 패널 표시
            // Restart 버튼 무효화
            Button bt = restartButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameClearSpr;
            PlayerContriller.gameState = "gameend";

            // +++ 시간제한 추가 +++
            if (timeCount != null)
            {
                timeCount.isTimeOver = true;

                // +++ 점수 추가 +++
                // 정수에 할당하여 소수점 이하를 버린다.
                int time = (int)timeCount.displayTime;
                totalScore += time * 10;
            }

            // +++ 점수 추가 +++
            totalScore += stageScore;
            stageScore = 0;
            UpdateScore();  // 점수 갱신
        }
        else if (PlayerContriller.gameState == "gameover")
        {
            // 게임 오버
            mainImage.SetActive(true);  // 이미지 표시
            panel.SetActive(true);          // 패널 표시
            // Next 버튼 무효화
            Button bt = nextButton.GetComponent<Button>();
            bt.interactable = false;
            mainImage.GetComponent<Image>().sprite = gameOverSpr;
            PlayerContriller.gameState = "gameend";


            // +++ 시간제한 추가 +++
            if (timeCount != null)
            {
                timeCount.isTimeOver = true;
            }
        }
        else if (PlayerContriller.gameState == "playing")
        {
            // 게임 중

            // +++ 시간제한 추가 +++
            if (timeCount != null)
            {
                if (timeCount.gameTime > 0)
                {
                    // 정수에 할당하여 소수점 이하를 버림
                    int time = (int)timeCount.displayTime;

                    // 시간  갱신
                    timeText.GetComponent<Text>().text = time.ToString();

                    // 타임 오버
                    if (time == 0)
                    {
                        playerContriller.GameOver();
                    }
                }
            }
            if (playerContriller.score != 0)
            {
                stageScore += playerContriller.score;
                playerContriller.score = 0;
                UpdateScore();
            }
        }

        if (soundPlayer != null)
        {
            // BGM 정지
            soundPlayer.Stop();
            soundPlayer.PlayOneShot(meGameOver);
        }

    }

    private void InactiveImage()
    {
        mainImage.SetActive(false);
    }

    // +++ 점수 추가 +++
    private void UpdateScore()
    {
        int score = stageScore + totalScore;
        scoreText.GetComponent<Text>().text = score.ToString();
    }
}
