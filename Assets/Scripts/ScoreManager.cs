using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText; // ���ø߷��ı�
    public TextMeshProUGUI timeText; // ����ʱ���ı�


    private int highScore = 0; // ��߷���
    private float time = 0.0f; // ��Ϸʱ��

    private void Start()
    {
        // �ڿ�ʼʱ�����ı�Ԫ�ظ���Ϊ��ʼֵ
        highScoreText.text = "Previous high score: " + highScore.ToString();
        timeText.text = "Time: " + FormatTime(time);
    }

    // �����������ڸ�ʽ��ʱ��
    private string FormatTime(float seconds)
    {
        int minutes = Mathf.FloorToInt(seconds / 60);
        int secondsInt = Mathf.FloorToInt(seconds % 60);
        return string.Format("{0:00}:{1:00}", minutes, secondsInt);
    }

    // ���¸߷���
    public void UpdateHighScore(int newScore)
    {
        if (newScore > highScore)
        {
            highScore = newScore;
            highScoreText.text = "Previous High Score: " + highScore.ToString();
        }
    }

    // ����ʱ��
    public void UpdateTime(float newTime)
    {
        time = newTime;
        timeText.text = "Time: " + FormatTime(time);
    }








}
