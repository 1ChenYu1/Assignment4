using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText; // 引用高分文本
    public TextMeshProUGUI timeText; // 引用时间文本


    private int highScore = 0; // 最高分数
    private float time = 0.0f; // 游戏时间

    private void Start()
    {
        // 在开始时，将文本元素更新为初始值
        highScoreText.text = "Previous high score: " + highScore.ToString();
        timeText.text = "Time: " + FormatTime(time);
    }

    // 辅助方法用于格式化时间
    private string FormatTime(float seconds)
    {
        int minutes = Mathf.FloorToInt(seconds / 60);
        int secondsInt = Mathf.FloorToInt(seconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, secondsInt);
    }

    // 更新高分数
    public void UpdateHighScore(int newScore)
    {
        if (newScore > highScore)
        {
            highScore = newScore;
            highScoreText.text = "Previous High Score: " + highScore.ToString();
        }
    }

    // 更新时间
    public void UpdateTime(float newTime)
    {
        time = newTime;
        timeText.text = "Time: " + FormatTime(time);
    }








}
