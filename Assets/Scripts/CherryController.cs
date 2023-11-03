using System.Collections;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    private Tweener tweener;

    [SerializeField]
    private GameObject cherryPrefab; // ����ӣ�ҵ�Ԥ����

    public Vector2 spawnIntervalRange = new Vector2(8f, 12f); // ����ӣ�����ɼ����Χ

    private void Start()
    {
        tweener = GetComponent<Tweener>();
        StartCoroutine(SpawnCherryCoroutine());
    }

    private IEnumerator SpawnCherryCoroutine()
    {
        yield return new WaitForSeconds(3f); // �ȴ���Ϸ��ʼ�ĵ���ʱ

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

        // ����ӣ�ҵ�����Ŀ��λ�ã����������Ϸ�������(0, 0)λ��
        Vector3 finalPosition = new Vector3(-spawnPosition.x, -spawnPosition.y, 0);

        // ʹ�� Tween �ý���ӣ�Ҵ�����λ���ƶ�������λ��
        tweener.AddTween(newCherry.transform, spawnPosition, Vector3.zero, 5f);
        yield return new WaitForSeconds(5f);

        // �������ӣ����Ȼ���ڣ�δ���Ե����������Ƴ���Ļ������
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

        // ���ѡ��һ����Ļ�߽�
        int side = Random.Range(0, 4);

        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = cameraHeight * Camera.main.aspect;

        switch (side)
        {
            case 0: // ��߽�
                randomPosition = new Vector3(-cameraWidth - 1f, Random.Range(-cameraHeight, cameraHeight), 0);
                break;

            case 1: // �ұ߽�
                randomPosition = new Vector3(cameraWidth + 1f, Random.Range(-cameraHeight, cameraHeight), 0);
                break;

            case 2: // �ϱ߽�
                randomPosition = new Vector3(Random.Range(-cameraWidth, cameraWidth), cameraHeight + 1f, 0);
                break;

            case 3: // �±߽�
                randomPosition = new Vector3(Random.Range(-cameraWidth, cameraWidth), -cameraHeight - 1f, 0);
                break;
        }

        return randomPosition;
    }
}
