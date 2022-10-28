using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.SpawnerButton
{

    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrefab;
        public List<Button> buttonList;
        public List<Button> GenerateSpawner(int cols, int rows)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject button1 = (GameObject)Instantiate(buttonPrefab, transform);
                float posX1 = col;
                float posY1 = rows + 1;
                button1.transform.localPosition = new Vector2(posX1, posY1);
                buttonList.Add(button1.GetComponent<Button>());
            }
            return buttonList;
        }
    }
}