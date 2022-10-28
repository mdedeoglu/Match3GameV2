using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Match3.DropSystem
{
    public class DropGrid
    {
        private int rows;
        private int cols;
        private float tileSize;
        private Drop[,] drops;
        private Grid grid;

        public int Cols
        {
            get { return cols; }
        }
        public int Rows
        {
            get { return rows; }
        }

        public float TileSize
        {
            get { return tileSize; }
        }
        public Drop[,] Drops
        {
            get { return drops; }
            set { drops = value; }
        }
        public Grid Grid
        {
            get { return grid; }
        }
        public DropGrid(Drop[,] _drops, int _cols, int _rows, float _tileSize, Grid _grid)
        {
            cols = _cols;
            rows = _rows;
            drops = _drops;
            grid = _grid;
            tileSize = _tileSize;
        }
    }
}