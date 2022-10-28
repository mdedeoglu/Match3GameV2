using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3.DropSystem;


namespace Match3
{
    public class WorldPosition
    {
        public static Vector2 GetWorldPosition(DropGrid dropGrid, int x, int y)
        {
            float gridW = dropGrid.Cols * dropGrid.TileSize;
            float gridH = dropGrid.Rows * dropGrid.TileSize;
            return new Vector2(dropGrid.Grid.transform.position.x + x, dropGrid.Grid.transform.position.y + dropGrid.Rows - 1 - y);
        }

        public static Vector2 GetGridPosition(DropGrid dropGrid, float x, float y)
        {
            float gridW = dropGrid.Cols * dropGrid.TileSize;
            float gridH = dropGrid.Rows * dropGrid.TileSize;
            return new Vector2(x - dropGrid.Grid.transform.position.x, 0 - (y + 1f - dropGrid.Grid.transform.position.y - dropGrid.Rows));
        }

    }
}