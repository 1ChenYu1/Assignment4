using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // �������ڴ��� Level 1 ��ť�ĵ���¼�
    public void LoadLevel1()
    {
        // ʹ�� SceneManager.LoadScene ���ص� Level 1 ����
        SceneManager.LoadScene("Level1"); //
    }
}
