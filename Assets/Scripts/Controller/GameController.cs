using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3.DropSystem;
using Match3.SpawnerButton;
using Match3.ObjectPool;
using Match3.InputSystem;
using Match3.MatchSystem;


namespace Match3 {
    public class GameController : MonoBehaviour
    {
        [SerializeField] public List<DropTypeSO> dropTypeList;
        [SerializeField] public List<ColorTypeSO> colorTypeList;

        [SerializeField]
        [Range(3, 10)] public int rows = 8;
        [SerializeField]
        [Range(3, 10)] public int cols = 8;

        private float moveTime = 0.2f;
        private float tileSize = 1;
        private float fillTime = 0f;
        private List<Button> buttonList;

        [SerializeField] private Grid grid;
        [SerializeField] private Spawner spawner;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private InputController inputController;
        [SerializeField] private ObjectPooler objectPooler;

        private Drop[,] drops;
        public DropGrid dropGrid;
        private Drop pressedDrop;



        private void Awake() //Input Events 
        {
            inputController.OnPressDrop += PressDrop;
            inputController.OnSwipeLeft += LeftSwipe;
            inputController.OnSwipeRight += RightSwipe;
            inputController.OnSwipeUp += UpSwipe;
            inputController.OnSwipeDown += DownSwipe;
        }


        void Start()
        {
            grid.GenerateGrid(cols, rows, tileSize); // Generate Grid
            buttonList = spawner.GenerateSpawner(cols, rows); // Generate Spawner Button
            cameraController.SetupCamera(cols, rows);  // Camera Setup
            dropGrid = new DropGrid(drops, cols, rows, tileSize, grid); // DropGrid Generate        
            objectPooler.GenerateDrops(dropGrid); // Object Pool Generate
            StartFill();
        }


        public void StartFill()// Start Fill Grid with Normal Drops
        {
            bool needsRefill = true;
            while (needsRefill)
            {
                while (FillBoard.FillStep(dropGrid, dropTypeList, fillTime, objectPooler, colorTypeList, buttonList))
                    needsRefill = Match.ClearAllValidMatches(dropGrid, objectPooler, fillTime, dropTypeList[0]);
            }
        }


        public IEnumerator Fill() // Fill Grid with Normal Drops
        {
            bool needsRefill = true;
            while (needsRefill)
            {
                yield return new WaitForSeconds(fillTime * 2f);
                while (FillBoard.FillStep(dropGrid, dropTypeList, fillTime, objectPooler, colorTypeList, buttonList))
                {
                    yield return new WaitForSeconds(fillTime);
                }
                needsRefill = Match.ClearAllValidMatches(dropGrid, objectPooler, fillTime, dropTypeList[0]);
            }
        }



        #region Inputcontroller Events
        //Press Drop 
        private void PressDrop()
        {
            Vector2 firstPressPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 pos1 = WorldPosition.GetGridPosition(dropGrid, firstPressPos.x, firstPressPos.y);
            int dropX = (int)Math.Round(pos1.x);
            int dropY = (int)Math.Round(pos1.y);
            if (dropX >= 0 && dropX < cols && dropY >= 0 && dropY < rows)
            {
                pressedDrop = dropGrid.Drops[dropX, dropY];
            }
            else
            {
                pressedDrop = null;
            }
        }

        private void LeftSwipe()
        {
            if (pressedDrop != null && pressedDrop.X > 0)
            {
                Drop enteredDrop = dropGrid.Drops[pressedDrop.X - 1, pressedDrop.Y];
                ReleaseDrop(dropGrid, pressedDrop, enteredDrop);
            }
        }
        private void RightSwipe()
        {
            if (pressedDrop != null && pressedDrop.X < cols - 1)
            {
                Drop enteredDrop = dropGrid.Drops[pressedDrop.X + 1, pressedDrop.Y];
                ReleaseDrop(dropGrid, pressedDrop, enteredDrop);
            }
        }
        private void UpSwipe()
        {
            if (pressedDrop != null && pressedDrop.Y > 0)
            {
                Drop enteredDrop = dropGrid.Drops[pressedDrop.X, pressedDrop.Y - 1];
                ReleaseDrop(dropGrid, pressedDrop, enteredDrop);
            }
        }
        private void DownSwipe()
        {
            if (pressedDrop != null && pressedDrop.Y < rows - 1)
            {
                Drop enteredDrop = dropGrid.Drops[pressedDrop.X, pressedDrop.Y + 1];
                ReleaseDrop(dropGrid, pressedDrop, enteredDrop);
            }
        }

        //Release Drop 
        public void ReleaseDrop(DropGrid dropGrid, Drop pressedDrop, Drop enteredDrop)
        {
            fillTime = moveTime;
            if (AdjacentControl.IsAdjacent(pressedDrop, enteredDrop))
            {
                if (SwapDrop.SwapDrops(pressedDrop, enteredDrop, fillTime, dropGrid))
                {
                    pressedDrop = null; enteredDrop = null;
                    LeanTween.delayedCall(moveTime * 1.5f, () =>
                    {
                        Match.ClearAllValidMatches(dropGrid, objectPooler, fillTime, dropTypeList[0]);
                        LeanTween.delayedCall(moveTime * 1.5f, () => { StartCoroutine(Fill()); });
                    });
                }
            }
            pressedDrop = null;
        }

        #endregion

    }

}