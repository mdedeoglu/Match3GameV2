using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Match3.SpawnerButton;

namespace Match3.InputSystem
{
    public class InputController : MonoBehaviour
    {
        private Vector2 firstPressPos;
        private Vector2 secondPressPos;
        private Vector2 currentSwipe;
        private RaycastHit2D hit;

        public event Action OnPressDrop = delegate { };
        public event Action OnSwipeLeft = delegate { };
        public event Action OnSwipeRight = delegate { };
        public event Action OnSwipeUp = delegate { };
        public event Action OnSwipeDown = delegate { };



        void Update()
        {
            Swipe();
        }


        public void Swipe()  //Swipe Check 
        {
            if (Input.GetMouseButtonDown(0))
            {
                firstPressPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                OnPressDrop();
            }
            if (Input.GetMouseButtonUp(0))
            {
                secondPressPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();

                hit = Physics2D.Raycast(firstPressPos, Vector2.zero);
                if (hit.collider != null && hit.collider.CompareTag("Button"))
                {
                    hit.transform.GetComponent<Button>().Press();
                }
                else
                {
                    if (currentSwipe.y > 0.5f && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        OnSwipeUp();
                    }

                    else if (currentSwipe.y < 0.5f && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                    {
                        OnSwipeDown();
                    }

                    else if (currentSwipe.x < 0.5f && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        OnSwipeLeft();
                    }

                    else if (currentSwipe.x > 0.5f && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                    {
                        OnSwipeRight();
                    }
                }
            }

        }


    }
}