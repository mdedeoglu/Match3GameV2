using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3.DropSystem;
using Match3.ObjectPool;
using Match3.SpawnerButton;


namespace Match3
{
    public class FillBoard
    {
        public static bool FillStep(DropGrid dropGrid, List<DropTypeSO> dropTypeList, float fillTime, ObjectPooler objectPooler, List<ColorTypeSO> colorTypeList, List<Button> buttonList)// Fill Grid 
        {
            bool movedDrop = false;
            for (int y = dropGrid.Rows - 2; y >= 0; y--)
            {
                for (int loopX = 0; loopX < dropGrid.Cols; loopX++)
                {
                    int x = loopX;
                    Drop drop = dropGrid.Drops[x, y];
                    if (drop.IsMovable())
                    {
                        Drop dropBelow = dropGrid.Drops[x, y + 1];
                        if (dropBelow.Type == dropTypeList[0])
                        {
                            dropBelow.gameObject.SetActive(false);
                            dropBelow.transform.parent = objectPooler.inActiveDrops;
                            drop.MoveableComponent.Move(dropGrid, x, y + 1, fillTime);
                            dropGrid.Drops[x, y + 1] = drop;
                            objectPooler.GetPoolDrop(x, y, dropTypeList[0], dropGrid);
                            movedDrop = true;
                        }
                    }
                }
            }
            for (int x = 0; x < dropGrid.Cols; x++)
            {
                Drop dropBelow = dropGrid.Drops[x, 0];
                if (dropBelow.Type == dropTypeList[0])
                {
                    if (buttonList[x].Pressed == true)
                    {
                        dropBelow.gameObject.SetActive(false);
                        dropBelow.transform.parent = objectPooler.inActiveDrops;
                        GameObject newDrop = null;


                        for (int i = 0; i < objectPooler.dropNormalPoolSize; i++)
                        {
                            if (!objectPooler.dropNormalPool[i].activeInHierarchy)
                            {
                                newDrop = objectPooler.dropNormalPool[i].gameObject;
                                break;
                            }
                        }
                        Vector3 pos = WorldPosition.GetWorldPosition(dropGrid, x, -1);
                        newDrop.transform.position = pos;
                        newDrop.transform.localScale = new Vector3(1, 1, 1);
                        newDrop.SetActive(true);
                        newDrop.transform.parent = objectPooler.activeDrops;
                        dropGrid.Drops[x, 0] = newDrop.GetComponent<Drop>();
                        dropGrid.Drops[x, 0].Init(x, -1, dropTypeList[1]);
                        dropGrid.Drops[x, 0].ClearableComponent.Refresh();
                        dropGrid.Drops[x, 0].MoveableComponent.Move(dropGrid, x, 0, fillTime);
                        dropGrid.Drops[x, 0].ColorComponent.SetColor(colorTypeList[UnityEngine.Random.Range(0, colorTypeList.Count)]);
                        movedDrop = true;
                    }
                }
            }
            return movedDrop;
        }
    }
}