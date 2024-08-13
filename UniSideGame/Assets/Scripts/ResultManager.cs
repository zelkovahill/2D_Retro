using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
    public GameObject scoreText;                    // 점수 표시 텍스트

    private void Start()
    {
        scoreText.GetComponent<Text>().text = GameManager.totalScore.ToString();
    }
}
