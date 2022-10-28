using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Match3
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private GameObject tilePrefab;
        public void GenerateGrid(int cols, int rows, float tileSize)
        {
            for (int col = 0; col < cols; col++)
            {
                for (int row = 0; row < rows; row++)
                {
                    GameObject tile = (GameObject)Instantiate(tilePrefab, transform);
                    float posX = col * tileSize;
                    float posY = row * tileSize;
                    tile.transform.localPosition = new Vector2(posX, posY);
                }
            }
            float gridW = cols * tileSize;
            float gridH = rows * tileSize;
            transform.position = new Vector2(-gridW / 2 + (tileSize / 2), -gridH / 2 + (tileSize / 2));
        }


    }
}
