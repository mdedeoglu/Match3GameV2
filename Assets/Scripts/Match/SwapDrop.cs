using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3.DropSystem;

namespace Match3.MatchSystem
{
    public class SwapDrop
    {
        public static bool SwapDrops(Drop drop1, Drop drop2, float moveTime, DropGrid dropGrid)  // Swap Drops 
        {
            if (drop1.IsMovable() && drop2.IsMovable())
            {
                dropGrid.Drops[drop1.X, drop1.Y] = drop2;
                dropGrid.Drops[drop2.X, drop2.Y] = drop1;
                if (Match.GetMatch(drop1, drop2.X, drop2.Y, dropGrid) != null || Match.GetMatch(drop2, drop1.X, drop1.Y, dropGrid) != null)
                {
                    int drop1X = drop1.X;
                    int drop1Y = drop1.Y;
                    drop1.MoveableComponent.Move(dropGrid, drop2.X, drop2.Y, moveTime);
                    LeanTween.scale(drop1.gameObject, new Vector3(1.75f, 1.75f, 1.75f), moveTime / 1.5f).setLoopPingPong(1);
                    drop2.MoveableComponent.Move(dropGrid, drop1X, drop1Y, moveTime);
                    LeanTween.scale(drop2.gameObject, new Vector3(1.75f, 1.75f, 1.75f), moveTime / 1.5f).setLoopPingPong(1);
                    return true;
                }
                else
                {
                    dropGrid.Drops[drop1.X, drop1.Y] = drop1;
                    dropGrid.Drops[drop2.X, drop2.Y] = drop2;

                    int drop1X = drop1.X;
                    int drop1Y = drop1.Y;
                    drop1.MoveableComponent.Move(dropGrid, drop2.X, drop2.Y, moveTime);
                    drop2.MoveableComponent.Move(dropGrid, drop1X, drop1Y, moveTime);
                    LeanTween.delayedCall(moveTime * 1.1f, () =>
                    {
                        drop1X = drop1.X;
                        drop1Y = drop1.Y;
                        drop1.MoveableComponent.Move(dropGrid, drop2.X, drop2.Y, moveTime);
                        drop2.MoveableComponent.Move(dropGrid, drop1X, drop1Y, moveTime);
                    });
                    return false;
                }
            }
            return false;
        }

    }
}