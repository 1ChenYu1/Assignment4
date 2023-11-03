using System.Collections;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    private Transform target;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private float startTime;
    private float duration;
    private bool isTweening;

    public void AddTween(Transform targetTransform, Vector3 start, Vector3 end, float tweenDuration)
    {
        if (isTweening)
        {
            // 如果当前正在进行 Tween，可以选择中断或忽略新的 Tween 请求
            return;
        }

        target = targetTransform;
        startPosition = start;
        endPosition = end;
        startTime = Time.time;
        duration = tweenDuration;
        isTweening = true;
    }

    void Update()
    {
        if (isTweening)
        {
            float elapsed = Time.time - startTime;
            float t = Mathf.Clamp01(elapsed / duration);
            target.position = Vector3.Lerp(startPosition, endPosition, t);

            if (t >= 1.0f)
            {
                isTweening = false;
            }
        }
    }
}
