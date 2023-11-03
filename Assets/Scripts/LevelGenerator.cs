using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    public Tilemap map;
    public GameObject[] tileObjects;
    private GameObject newTile;
    private GameObject gamemap;

    public static int[,] levelMap =
    {
        {1,2,2,2,2,2,2,2,2,2,2,2,2,7},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,4},
        {2,6,4,0,0,4,5,4,0,0,0,4,5,4},
        {2,5,3,4,4,3,5,3,4,4,4,3,5,3},
        {2,5,5,5,5,5,5,5,5,5,5,5,5,5},
        {2,5,3,4,4,3,5,3,3,5,3,4,4,4},
        {2,5,3,4,4,3,5,4,4,5,3,4,4,3},
        {2,5,5,5,5,5,5,4,4,5,5,5,5,4},
        {1,2,2,2,2,1,5,4,3,4,4,3,0,4},
        {0,0,0,0,0,2,5,4,3,4,4,3,0,3},
        {0,0,0,0,0,2,5,4,4,0,0,0,0,0},
        {0,0,0,0,0,2,5,4,4,0,3,4,4,0},
        {2,2,2,2,2,1,5,3,3,0,4,0,0,0},
        {0,0,0,0,0,0,5,0,0,0,4,0,0,0},
    };

    // Start is called before the first frame update
    void Start()
    {
        // 创建游戏地图容器
        gamemap = new GameObject("Map");

        // 查找 Tilemap 游戏对象
        var tilemap = GameObject.Find("Tilemap");

        // 获取 Tilemap 组件
        var tilemapComponent = tilemap.GetComponent<Tilemap>();

        // 清除手动布局的 Tilemap
        tilemapComponent.ClearAllTiles();

        // 生成地图布局（第二象限）
        GenerateLevelLayout();

        // 旋转地图容器
        gamemap.transform.Rotate(new Vector3(0, 0, -90f));

        // 设置地图容器的位置
        gamemap.transform.position = new Vector3(-9f, 5f);

        // 生成第一个象限（右上）
        GenerateQuadrant(
            new Vector3(18f, 5f),  // 位置
            new Vector3(-1, 1),        // 缩放
            Quaternion.Euler(0, 0, 90) // 旋转
        );

        // 生成第三个象限（左下）
        GenerateQuadrant(
            new Vector3(-9f, -24), // 位置
            new Vector3(1, -1),         // 缩放
            Quaternion.Euler(0, 0, 90)  // 旋转
        );

        // 生成第四个象限（右下）
        GenerateQuadrant(
            new Vector3(18f, -24f),  // 位置
            new Vector3(-1, -1),        // 缩放
            Quaternion.Euler(0, 0, -90) // 旋转
        );
    }





    void GenerateLevelLayout()
    {
        for (int x = 0; x < levelMap.GetLength(0); x++)
        {
            for (int y = 0; y < levelMap.GetLength(1); y++) 
            {
                var spriteValue = levelMap[x, y];

                if (spriteValue != 0) 
                {
                    newTile = Instantiate(tileObjects[spriteValue], new Vector3(x, y), Quaternion.identity, gamemap.transform);
                    newTile.transform.rotation = SetRotation(x, y, spriteValue);
                }
            }
        }
    }




    void GenerateQuadrant(Vector3 position, Vector3 scale, Quaternion rotation)
    {
        GameObject quadrant = Instantiate(gamemap, position, rotation, map.transform.parent.transform);
        quadrant.transform.localScale = scale;
    }

    Quaternion SetRotation(int x, int y, int spriteValue)
    {
        if (spriteValue == 1 || spriteValue == 3)
        {
            return CornerRotation(x, y);
        }
        else if (spriteValue == 2 || spriteValue == 4)
        {
            return WallRotation(x, y);
        }
        else if (spriteValue == 7)
        {
            return Quaternion.Euler(0, 0, 90);
        }
        else
        {
            return Quaternion.identity;
        }
    }


    Quaternion CornerRotation(int x, int y) // Get rotation of corner from coordinate
    {
        var leftHit = Physics2D.Raycast(new Vector3(x, y), -newTile.transform.right, 1);
        var downHit = Physics2D.Raycast(new Vector3(x, y), -newTile.transform.up, 1);
        var leftExists = leftHit.collider != null && leftHit.collider.gameObject.CompareTag("Wall");
        var downExists = downHit.collider != null && downHit.collider.gameObject.CompareTag("Wall");
       int caseValue = 0;  // 初始化一个 case 值

    if (leftExists) 
    {
        caseValue += 1;
    }
    if (downExists) 
    {
        caseValue += 2;
    }

    // 使用 switch 处理 case 值
    switch (caseValue)
    {
        case 0:  // 既没有左侧接触也没有下方接触
            return Quaternion.Euler(0, 0, 90);

        case 1:  // 只有左侧接触
            return Quaternion.Euler(0, 0, 180);

        case 2:  // 只有下方接触
            return Quaternion.identity;

        case 3:  // 左侧和下方都接触
            if (levelMap[x - 1, y] == 3)
            {
                return Quaternion.Euler(0, 0, leftHit.collider.transform.rotation.eulerAngles.z - 90f);
            }
            else if (levelMap[x + 1, y] == 4 && levelMap[x, y - 1] == 4)
            {
                return Quaternion.identity;
            }
            else if (leftHit.collider.transform.rotation.eulerAngles.z == downHit.collider.transform.rotation.eulerAngles.z)
            {
                return Quaternion.Euler(0, 0, 180);
            }
            else
            {
                return Quaternion.Euler(0, 0, -90);
            }

        default:
            return Quaternion.identity;
    }
    }

    Quaternion WallRotation(int x, int y)
    {
        var leftHit = Physics2D.Raycast(new Vector3(x, y), -newTile.transform.right, 1);
        var leftExists = leftHit.collider != null && leftHit.collider.gameObject.CompareTag("Wall");

        int switchValue = 0;  // 初始化一个 switch 值

        if (leftExists)
        {
            switchValue = 1;
        }

        // 使用 switch 处理 switch 值
        switch (switchValue)
        {
            case 0:  // 如果没有左侧接触
                return Quaternion.identity;

            case 1:  // 如果左侧接触
                if (leftHit.collider.gameObject.GetComponent<SpriteRenderer>().sprite == newTile.GetComponent<SpriteRenderer>().sprite)
                {
                    return leftHit.collider.transform.rotation * Quaternion.identity;  // Continue the wall
                }
                else
                {
                    return Quaternion.Euler(0, 0, 90);  // If left not wall, use vertical wall
                }

            default:
                return Quaternion.identity;
        }
    }


}