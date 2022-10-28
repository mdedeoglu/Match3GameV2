using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Match3.DropSystem
{
    public class MoveableDrop : MonoBehaviour
    {
        private Drop drop;
        void Awake()
        {
            drop = GetComponent<Drop>();
        }

        public void Move(DropGrid dropGrid, int newX, int newY, float time)
        {
            drop.X = newX;
            drop.Y = newY;
            Vector2 pos = WorldPosition.GetWorldPosition(dropGrid, newX, newY);

            if (time != 0f)
            {
                LeanTween.move(drop.gameObject, pos, time);
            }
            else
            {
                drop.transform.position = pos;
            }

        }


    }
}