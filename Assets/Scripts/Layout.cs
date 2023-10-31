using UnityEngine;

public class Layout : MonoBehaviour
{
    public Texture2D standardPelletTexture;
    public Texture2D powerPelletTexture;

    public float tileSize = 1.0f;

    int[,] levelMap =
    {
        // (µØÍ¼Êý¾Ý)
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,5,5,5,5,5,5,5,5,5,5,5,5,0,0,0,0,0,0,0,0,5,5,5,5,5,5,5,5,5,5,5,5},
        {0,5,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,5},
        {0,5,5,5,6,5,5,5,5,6,5,5,5,0,0,0,0,0,0,0,0,5,5,5,6,5,5,5,5,6,5,5,5},
        {0,5,0,0,5,0,0,0,0,5,0,0,5,0,0,0,0,0,0,0,0,5,0,0,5,0,0,0,0,5,0,0,5},
        {0,5,0,0,5,0,0,0,0,5,0,0,5,0,0,0,0,0,0,0,0,5,0,0,5,0,0,0,0,5,0,0,5},
        {0,5,0,0,5,0,0,0,0,5,0,0,5,0,0,0,0,0,0,0,0,5,0,0,5,0,0,0,0,5,0,0,5},
        {0,5,5,5,5,5,0,0,0,5,5,5,5,0,0,0,0,0,0,0,0,5,5,5,5,5,0,0,0,5,5,5,5},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {0,5,5,5,5,5,5,5,5,5,5,5,5,0,0,0,0,0,0,0,0,5,5,5,5,5,5,5,5,5,5,5,5},
        {0,5,0,0,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,5,0,0,0,0,0,0,0,0,0,0,5},
        {0,5,5,6,5,5,5,5,5,6,5,5,5,0,0,0,0,0,0,0,0,5,5,5,6,5,5,5,5,6,5,5,5},
        {0,5,0,0,5,0,0,0,0,5,0,0,5,0,0,0,0,0,0,0,0,5,0,0,5,0,0,0,0,5,0,0,5},
        {0,5,0,0,5,0,0,0,0,5,0,0,5,0,0,0,0,0,0,0,0,5,0,0,5,0,0,0,0,5,0,0,5},
        {0,5,0,0,5,0,0,0,0,5,0,0,5,0,0,0,0,0,0,0,0,5,0,0,5,0,0,0,0,5,0,0,5},
        {0,5,5,5,5,5,0,0,0,5,5,5,5,0,0,0,0,0,0,0,0,5,5,5,5,5,0,0,0,5,5,5,5},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0}
      

    };

    void Start()
    {
        GenerateLevel();
    }

    void GenerateLevel()
    {
        for (int i = 0; i < levelMap.GetLength(0); i++)
        {
            for (int j = 0; j < levelMap.GetLength(1); j++)
            {
                int elementType = levelMap[i, j];

                if (elementType == 5 || elementType == 6) // Standard Pellet or Power Pellet
                {
                    Vector3 position = new Vector3(j * tileSize, -i * tileSize, 0f);

                    GameObject tileObject = new GameObject("Tile");
                    tileObject.transform.position = position;
                    tileObject.transform.localScale = new Vector3(tileSize, tileSize, 1f);

                    SpriteRenderer spriteRenderer = tileObject.AddComponent<SpriteRenderer>();
                    spriteRenderer.sprite = (elementType == 5) ? SpriteFromTexture(standardPelletTexture) : SpriteFromTexture(powerPelletTexture);
                }
            }
        }
    }

    Sprite SpriteFromTexture(Texture2D texture)
    {
        Rect rect = new Rect(0, 0, texture.width, texture.height);
        return Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f), 100f);
    }
}
