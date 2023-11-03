using System.Collections;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    private Tweener tweener;

    [SerializeField]
    private GameObject cherryPrefab; // 奖励樱桃的预制体

    public Vector2 spawnIntervalRange = new Vector2(8f, 12f); // 奖励樱桃生成间隔范围

    private void Start()
    {
        tweener = GetComponent<Tweener>();
        StartCoroutine(SpawnCherryCoroutine());
    }

    private IEnumerator SpawnCherryCoroutine()
    {
        yield return new WaitForSeconds(3f); // 等待游戏开始的倒计时

        while (true)
        {
            yield return new WaitForSeconds(Random.Range(spawnIntervalRange.x, spawnIntervalRange.y));

            StartCoroutine(GenerateCherryAndTween());
        }
    }

    private IEnumerator GenerateCherryAndTween()
    {
        Vector3 spawnPosition = GenerateRandomPointOutOfBounds();
        GameObject newCherry = Instantiate(cherryPrefab, spawnPosition, Quaternion.identity);

        // 奖励樱桃的最终目标位置，这里假设游戏摄像机在(0, 0)位置
        Vector3 finalPosition = new Vector3(-spawnPosition.x, -spawnPosition.y, 0);

        // 使用 Tween 让奖励樱桃从生成位置移动到中心位置
        tweener.AddTween(newCherry.transform, spawnPosition, Vector3.zero, 5f);
        yield return new WaitForSeconds(5f);

        // 如果奖励樱桃仍然存在（未被吃掉），则将其移出屏幕并销毁
        if (newCherry != null)
        {
            tweener.AddTween(newCherry.transform, newCherry.transform.position, finalPosition, 5f);
            yield return new WaitForSeconds(5f);

            Destroy(newCherry);
        }
    }

    private Vector3 GenerateRandomPointOutOfBounds()
    {
        Vector3 randomPosition = Vector3.zero;

        // 随机选择一个屏幕边界
        int side = Random.Range(0, 4);

        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        switch (side)
        {
            case 0: // 左边界
                randomPosition = new Vector3(-cameraWidth - 1f, Random.Range(-cameraHeight, cameraHeight), 0);
                break;

            case 1: // 右边界
                randomPosition = new Vector3(cameraWidth + 1f, Random.Range(-cameraHeight, cameraHeight), 0);
                break;

            case 2: // 上边界
                randomPosition = new Vector3(Random.Range(-cameraWidth, cameraWidth), cameraHeight + 1f, 0);
                break;

            case 3: // 下边界
                randomPosition = new Vector3(Random.Range(-cameraWidth, cameraWidth), -cameraHeight - 1f, 0);
                break;
        }

        return randomPosition;
    }
}
