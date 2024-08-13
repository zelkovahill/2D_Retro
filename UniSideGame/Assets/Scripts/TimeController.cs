using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public bool isCountDown = true;     // true  = 카운트다운으로 시간 측정
    public float gameTime = 0;          // 게임의 최대 시간
    public bool isTimeOver = false;    // true = 타이머 정지
    public float displayTime = 0;      // 표시용 시간

    private float times = 0;           // 현재 시간

    // ====================================================================================================

    private void Start()
    {
        if (isCountDown)
        {
            // 카운트다운
            displayTime = gameTime;
        }
    }

    private void Update()
    {
        if (isTimeOver == false)
        {
            times += Time.deltaTime;

            if (isCountDown)
            {
                // 카운트다운
                displayTime = gameTime - times;
                if (displayTime <= 0)
                {
                    displayTime = 0;
                    isTimeOver = true;
                }
            }
            else
            {
                // 카운트업
                displayTime = times;
                if (displayTime >= gameTime)
                {
                    displayTime = gameTime;
                    isTimeOver = true;
                }
            }
            // Debug.Log("Time : " + displayTime);
        }
    }

}
