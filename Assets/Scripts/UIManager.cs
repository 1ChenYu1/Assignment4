using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // 方法用于处理 Level 1 按钮的点击事件
    public void LoadLevel1()
    {
        // 使用 SceneManager.LoadScene 加载到 Level 1 场景
        SceneManager.LoadScene("Level1"); //
    }
}
