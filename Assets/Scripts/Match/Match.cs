using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3.ObjectPool;
using Match3.DropSystem;

namespace Match3.MatchSystem
{
    public class Match
    {
        private static List<Drop> matchingDrops;
        private static List<Drop> horizontalDrops;
        private static List<Drop> verticalDrops;

        // Control All Matches
        public static List<Drop> GetMatch(Drop drop, int newX, int newY, DropGrid dropGrid)
        {
            if (drop.IsColored())
            {
                matchingDrops = new List<Drop>();
                horizontalDrops = new List<Drop>();
                verticalDrops = new List<Drop>();

                if (HorizontalMatch(drop, dropGrid, newX, newY) != null)
                {
                    return HorizontalMatch(drop, dropGrid, newX, newY);
                }
                if (VerticalMatch(drop, dropGrid, newX, newY) != null)
                {
                    return VerticalMatch(drop, dropGrid, newX, newY);
                }
            }
            return null;
        }

        // Find Horizontal Match 
        private static List<Drop> HorizontalMatch(Drop drop, DropGrid dropGrid, int newX, int newY) // Horizontal Match Control
        {
            horizontalDrops.Clear();
            verticalDrops.Clear();
            ColorTypeSO color = drop.ColorComponent.Color;
            horizontalDrops.Add(drop);
            for (int dir = 0; dir <= 1; dir++)
            {
                for (int xOffset = 1; xOffset < dropGrid.Cols; xOffset++)
                {
                    int x;
                    if (dir == 0) { x = newX - xOffset; }
                    else { x = newX + xOffset; }
                    if (x < 0 || x >= dropGrid.Cols) { break; }
                    if (dropGrid.Drops[x, newY].IsColored() && dropGrid.Drops[x, newY].ColorComponent.Color == color)
                    {
                        horizontalDrops.Add(dropGrid.Drops[x, newY]);
                    }
                    else { break; }
                }
            }
            if (horizontalDrops.Count >= 3)
            {
                for (int i = 0; i < horizontalDrops.Count; i++)
                {
                    matchingDrops.Add(horizontalDrops[i]);
                }
            }

            if (horizontalDrops.Count >= 3)
            {
                for (int i = 0; i < horizontalDrops.Count; i++)
                {
                    matchingDrops.Add(horizontalDrops[i]);
                }
            }


            if (horizontalDrops.Count >= 3)
            {
                for (int i = 0; i < horizontalDrops.Count; i++)
                {
                    for (int dir = 0; dir <= 1; dir++)
                    {
                        for (int yOffset = 1; yOffset < dropGrid.Rows; yOffset++)
                        {
                            int y;
                            if (dir == 0) { y = newY - yOffset; }
                            else { y = newY + yOffset; }
                            if (y < 0 || y >= dropGrid.Rows) { break; }
                            if (dropGrid.Drops[horizontalDrops[i].X, y].IsColored() && dropGrid.Drops[horizontalDrops[i].X, y].ColorComponent.Color == color)
                            {
                                verticalDrops.Add(dropGrid.Drops[horizontalDrops[i].X, y]);
                            }
                            else { break; }
                        }
                    }
                    if (verticalDrops.Count < 2)
                    {
                        verticalDrops.Clear();
                    }
                    else
                    {
                        for (int j = 0; j < verticalDrops.Count; j++)
                        {
                            matchingDrops.Add(verticalDrops[j]);
                        }
                        break;
                    }
                }
            }
            if (matchingDrops.Count >= 3)
            {
                return matchingDrops;
            }
            return null;
        }

        // Find Vertical Match 
        private static List<Drop> VerticalMatch(Drop drop, DropGrid dropGrid, int newX, int newY)
        {
            horizontalDrops.Clear();
            verticalDrops.Clear();
            ColorTypeSO color = drop.ColorComponent.Color;
            verticalDrops.Add(drop);
            for (int dir = 0; dir <= 1; dir++)
            {
                for (int yOffset = 1; yOffset < dropGrid.Rows; yOffset++)
                {
                    int y;
                    if (dir == 0) { y = newY - yOffset; }
                    else { y = newY + yOffset; }

                    if (y < 0 || y >= dropGrid.Rows) { break; }

                    if (dropGrid.Drops[newX, y].IsColored() && dropGrid.Drops[newX, y].ColorComponent.Color == color)
                    {
                        verticalDrops.Add(dropGrid.Drops[newX, y]);
                    }
                    else { break; }
                }
            }
            if (verticalDrops.Count >= 3)
            {
                for (int i = 0; i < verticalDrops.Count; i++)
                {
                    matchingDrops.Add(verticalDrops[i]);
                }
            }

            if (verticalDrops.Count >= 3)
            {
                for (int i = 0; i < verticalDrops.Count; i++)
                {
                    for (int dir = 0; dir <= 1; dir++)
                    {
                        for (int xOffset = 1; xOffset < dropGrid.Cols; xOffset++)
                        {
                            int x;
                            if (dir == 0) { x = newX - xOffset; }
                            else { x = newX + xOffset; }
                            if (x < 0 || x >= dropGrid.Cols) { break; }
                            if (dropGrid.Drops[x, verticalDrops[i].Y].IsColored() && dropGrid.Drops[x, verticalDrops[i].Y].ColorComponent.Color == color)
                            {
                                horizontalDrops.Add(dropGrid.Drops[x, verticalDrops[i].Y]);
                            }
                            else { break; }
                        }
                    }
                    if (horizontalDrops.Count < 2) { horizontalDrops.Clear(); }
                    else
                    {
                        for (int j = 0; j < horizontalDrops.Count; j++)
                        {
                            matchingDrops.Add(horizontalDrops[j]);
                        }
                        break;
                    }
                }
            }
            if (matchingDrops.Count >= 3)
            {
                return matchingDrops;
            }
            return null;
        }


        // Clear All Matches in Grid
        public static bool ClearAllValidMatches(DropGrid dropGrid, ObjectPooler objectPooler, float fillTime, DropTypeSO emptyType)
        {
            bool needsRefill = false;
            for (int y = 0; y < dropGrid.Rows; y++)
            {
                for (int x = 0; x < dropGrid.Cols; x++)
                {
                    if (dropGrid.Drops[x, y].IsClearable())
                    {
                        List<Drop> match = Match.GetMatch(dropGrid.Drops[x, y], x, y, dropGrid);
                        if (match != null)
                        {
                            for (int i = 0; i < match.Count; i++)
                            {
                                if (ClearDrop(dropGrid, match[i].X, match[i].Y, objectPooler, fillTime, emptyType))
                                {
                                    needsRefill = true;
                                }
                            }
                        }
                    }

                }
            }
            return needsRefill;
        }


        // Release drop to pool and locate empty pool drop
        public static bool ClearDrop(DropGrid dropGrid, int x, int y, ObjectPooler objectPooler, float fillTime, DropTypeSO emptyType)
        {
            if (dropGrid.Drops[x, y].IsClearable() && !dropGrid.Drops[x, y].ClearableComponent.IsBeingCleared)
            {
                dropGrid.Drops[x, y].ClearableComponent.Clear(fillTime);
                objectPooler.GetPoolDrop(x, y, emptyType, dropGrid);
                return true;
            }
            return false;
        }
    }
}