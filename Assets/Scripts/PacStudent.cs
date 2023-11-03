using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PacStudentController : MonoBehaviour
{
    public float speed = 10f; // Movement speed
    private Vector2 dest = Vector2.zero; // Destination position
    public event Action<Vector2> OnMove; // Event to notify movement
    public AudioClip[] movementAudioClips; // �洢�ƶ�ʱ����Ч����
    public AudioClip[] eatingAudioClips;   // �洢�Թ�ʵʱ����Ч����
    private bool isMoving = false;
    private bool isEating = false;
    private AudioSource audioSource;





    // Start is called before the first frame update
    void Start()
    {
        dest = transform.position; // Initialize the destination position to the current position
        audioSource = GetComponent<AudioSource>(); // ��ȡAudioSource���

    }

    // Update is called once per frame
    void Update()
    {
        updateMove(); // Update movement direction
        Move(); // Move the game object
    }

    private void updateMove()
    {
        isMoving = false; // �����ƶ�״̬Ϊfalse

        if (Input.GetKeyDown(KeyCode.W))
        {
            dest = (Vector2)transform.position + Vector2.up;
            isMoving = true; // �����ƶ�״̬
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            dest = (Vector2)transform.position + Vector2.left;
            isMoving = true; // �����ƶ�״̬
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            dest = (Vector2)transform.position + Vector2.down;
            isMoving = true; // �����ƶ�״̬
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            dest = (Vector2)transform.position + Vector2.right;
            isMoving = true; // �����ƶ�״̬
        }

        isEating = CheckIfEatingPellet(); // ����Ƿ��ڳԹ�ʵ

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
                // ���ųԹ�ʵ����Ч
                PlayRandomAudioClip(eatingAudioClips);
            }
            else
            {
                // �����ƶ�����Ч
                PlayRandomAudioClip(movementAudioClips);
            }
        }
        else
        {
            // ��������ƶ���ֹͣ��Ч
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
        // ʹ������Ͷ��������Ƿ����ʵ��ײ
        Vector2 currentPosition = transform.position;
        Vector2 direction = dest - currentPosition;

        // �������ߵĳ���ΪPacStudent��Ŀ��λ�õľ���
        float rayLength = direction.magnitude;

        // ��������
        Ray2D ray = new Ray2D(currentPosition, direction.normalized);

        // ����������ײ�㣬ȷ��ֻ���ʵ����ײ
        int layerMask = LayerMask.GetMask("PelletLayer"); // �滻 "PelletLayer" Ϊ��Ĺ�ʵ������

        // ִ������Ͷ����
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, rayLength, layerMask);

        if (hit.collider != null)
        {
            // ��ײ���˹�ʵ
            return true;
        }

        // û����ײ����ʵ
        return false;
    }




}
