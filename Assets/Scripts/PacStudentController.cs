using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PacStudent : MonoBehaviour
{
    public float speed = 10f; // Movement speed
    private Vector2 dest = Vector2.zero; // Destination position
    public event Action<Vector2> OnMove; // Event to notify movement
    public AudioClip[] movementAudioClips; // 存储移动时的音效剪辑
    public AudioClip[] eatingAudioClips;   // 存储吃果实时的音效剪辑
    private bool isMoving = false;
    private bool isEating = false;
    private AudioSource audioSource;





    // Start is called before the first frame update
    void Start()
    {
        dest = transform.position; // Initialize the destination position to the current position
        audioSource = GetComponent<AudioSource>(); // 获取AudioSource组件

    }

    // Update is called once per frame
    void Update()
    {
        updateMove(); // Update movement direction
        Move(); // Move the game object
    }

    private void updateMove()
    {
        isMoving = false; // 重置移动状态为false

        if (Input.GetKeyDown(KeyCode.W))
        {
            dest = (Vector2)transform.position + Vector2.up;
            isMoving = true; // 设置移动状态
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            dest = (Vector2)transform.position + Vector2.left;
            isMoving = true; // 设置移动状态
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            dest = (Vector2)transform.position + Vector2.down;
            isMoving = true; // 设置移动状态
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            dest = (Vector2)transform.position + Vector2.right;
            isMoving = true; // 设置移动状态
        }

        isEating = CheckIfEatingPellet(); // 检查是否在吃果实

        Vector2 dir = dest - (Vector2)transform.position; // Calculate the movement direction
        GetComponent<Animator>().SetFloat("DirX", dir.x); // Set the animation parameter DirX
        GetComponent<Animator>().SetFloat("DirY", dir.y); // Set the animation parameter DirY
    }

    private void Move()
    {
        // Move towards the destination position using the MoveTowards method
        Vector2 temp = Vector2.MoveTowards(transform.position, dest, speed * Time.deltaTime);
        GetComponent<Rigidbody2D>().MovePosition(temp);


        // Notify subscribers about the movement
        OnMove?.Invoke(temp);


        if (isMoving)
        {
            if (isEating)
            {
                // 播放吃果实的音效
                PlayRandomAudioClip(eatingAudioClips);
            }
            else
            {
                // 播放移动的音效
                PlayRandomAudioClip(movementAudioClips);
            }
        }
        else
        {
            // 如果不再移动，停止音效
            audioSource.Stop();
        }

    }
    private void PlayRandomAudioClip(AudioClip[] clips)
    {
        if (clips.Length > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, clips.Length);
            audioSource.clip = clips[randomIndex];
            audioSource.Play();
        }
    }


    private bool CheckIfEatingPellet()
    {
        // 使用射线投射来检查是否与果实碰撞
        Vector2 currentPosition = transform.position;
        Vector2 direction = dest - currentPosition;

        // 设置射线的长度为PacStudent到目标位置的距离
        float rayLength = direction.magnitude;

        // 创建射线
        Ray2D ray = new Ray2D(currentPosition, direction.normalized);

        // 设置射线碰撞层，确保只与果实层碰撞
        int layerMask = LayerMask.GetMask("PelletLayer"); // 替换 "PelletLayer" 为你的果实层名称

        // 执行射线投射检测
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, rayLength, layerMask);

        if (hit.collider != null)
        {
            // 碰撞到了果实
            return true;
        }

        // 没有碰撞到果实
        return false;
    }




}
