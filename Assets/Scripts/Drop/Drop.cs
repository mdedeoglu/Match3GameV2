using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Match3.DropSystem
{
    public class Drop : MonoBehaviour
    {
        private int x;
        private int y;
        public int X
        {
            get { return x; }
            set
            {
                if (IsMovable()) { x = value; }
            }
        }
        public int Y
        {
            get { return y; }
            set
            {
                if (IsMovable()) { y = value; }
            }
        }



        private DropTypeSO type;
        public DropTypeSO Type
        {
            get { return type; }

        }

        private MoveableDrop moveableComponent;
        public MoveableDrop MoveableComponent
        {
            get { return moveableComponent; }
        }

        private ColorDrop colorComponent;
        public ColorDrop ColorComponent
        {
            get { return colorComponent; }
        }


        private ClearableDrop clearableComponent;
        public ClearableDrop ClearableComponent
        {
            get { return clearableComponent; }
        }


        private void Awake()
        {
            moveableComponent = GetComponent<MoveableDrop>();
            colorComponent = GetComponent<ColorDrop>();
            clearableComponent = GetComponent<ClearableDrop>();
        }

        public void Init(int _x, int _y, DropTypeSO _type)
        {
            x = _x;
            y = _y;
            type = _type;
        }

        public bool IsMovable()
        {
            return moveableComponent != null;
        }

        public bool IsColored()
        {
            return colorComponent != null;
        }

        public bool IsClearable()
        {
            return clearableComponent != null;
        }


    }
}

