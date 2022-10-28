using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Match3.DropSystem;

namespace Match3
{
    public static class AdjacentControl

    {
        public static bool IsAdjacent(Drop drop1, Drop drop2) // Adjacent Control Two Drop
        {
            return (drop1.X == drop2.X && (int)Mathf.Abs(drop1.Y - drop2.Y) == 1) || (drop1.Y == drop2.Y && (int)Mathf.Abs(drop1.X - drop2.X) == 1);
        }


    }
}