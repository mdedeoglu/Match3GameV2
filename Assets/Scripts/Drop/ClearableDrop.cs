using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.DropSystem
{
    public class ClearableDrop : MonoBehaviour
    {
        private bool isBeingCleared = false;
        public bool IsBeingCleared
        {
            get { return isBeingCleared; }
            set { isBeingCleared = value; }
        }

        public void Refresh()
        {
            isBeingCleared = false;
        }

        public void Clear(float time)
        {
            isBeingCleared = true;
            if (time != 0)
            {
                LeanTween.scale(gameObject, new Vector3(0, 0, 0), time);
                LeanTween.delayedCall(time, () =>
                {
                    gameObject.SetActive(false);
                });
            }
            else
            {
                gameObject.SetActive(false);
            }

        }

    }
}
