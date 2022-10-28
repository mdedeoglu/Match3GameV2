using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3.DropSystem;


namespace Match3.ObjectPool
{
    public class ObjectPooler : MonoBehaviour
    {
        [SerializeField] public int dropEmptyPoolSize;
        [SerializeField] public int dropNormalPoolSize;
        [SerializeField] private GameController gameController;

        public List<GameObject> dropEmptyPool = new List<GameObject>();
        public List<GameObject> dropNormalPool = new List<GameObject>();
        public Transform activeDrops;
        public Transform inActiveDrops;


        public void GenerateDrops(DropGrid dropGrid)  // Generate and Pooling Drops
        {
            dropGrid.Drops = new Drop[dropGrid.Cols, dropGrid.Rows];

            for (int col = 0; col < dropGrid.Cols; col++)
            {
                for (int row = 0; row < dropGrid.Rows; row++)
                {
                    SpawnNewDrop(col, row, gameController.dropTypeList[0], dropGrid);
                }
            }

            for (int i = 0; i < dropNormalPoolSize; i++)
            {
                Vector3 pos = WorldPosition.GetWorldPosition(dropGrid, 100, 100);
                GameObject newDrop = (GameObject)Instantiate(gameController.dropTypeList[1].dropPrefab, pos, Quaternion.identity, inActiveDrops);
                newDrop.transform.parent = inActiveDrops;
                newDrop.SetActive(false);
                dropNormalPool.Add(newDrop);
            }
        }



        public Drop SpawnNewDrop(int x, int y, DropTypeSO type, DropGrid dropGrid) // Empty Drop Spawn
        {
            Vector2 pos = WorldPosition.GetWorldPosition(dropGrid, x, y);
            GameObject newDrop = (GameObject)Instantiate(gameController.dropTypeList[0].dropPrefab, pos, Quaternion.identity, activeDrops);
            newDrop.transform.parent = activeDrops;
            if (gameController.dropTypeList.Contains(type)) { dropEmptyPool.Add(newDrop); newDrop.transform.name = "Empty_" + x.ToString() + " - " + y.ToString(); }
            dropGrid.Drops[x, y] = newDrop.GetComponent<Drop>();
            dropGrid.Drops[x, y].Init(x, y, type);
            return dropGrid.Drops[x, y];
        }


        public Drop GetPoolDrop(int x, int y, DropTypeSO type, DropGrid dropGrid) //Get Drop From Pool
        {
            GameObject newDrop = null;
            for (int i = 0; i < dropEmptyPoolSize; i++)
            {
                if (!dropEmptyPool[i].activeInHierarchy)
                {
                    newDrop = dropEmptyPool[i].gameObject;
                    break;
                }
            }
            newDrop.SetActive(true);
            newDrop.transform.parent = activeDrops;
            dropGrid.Drops[x, y] = newDrop.GetComponent<Drop>();
            dropGrid.Drops[x, y].Init(x, y, type);
            return dropGrid.Drops[x, y];
        }

        public Drop GetEmptylDropFromPool(int x, int y, DropTypeSO type, DropGrid dropGrid) //Get Drop From Pool
        {
            GameObject newDrop = null;
            for (int i = 0; i < dropEmptyPoolSize; i++)
            {
                if (!dropEmptyPool[i].activeInHierarchy)
                {
                    newDrop = dropEmptyPool[i].gameObject;
                    break;
                }
            }
            newDrop.SetActive(true);
            newDrop.transform.parent = activeDrops;
            dropGrid.Drops[x, y] = newDrop.GetComponent<Drop>();
            dropGrid.Drops[x, y].Init(x, y, type);
            return dropGrid.Drops[x, y];
        }

    }
}