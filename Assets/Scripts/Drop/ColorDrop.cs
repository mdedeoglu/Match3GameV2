using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Match3.DropSystem
{
    public class ColorDrop : MonoBehaviour
    {

        public ColorTypeSO Color;
        private SpriteRenderer sprite;


        void Awake()
        {
            sprite = transform.Find("drop").GetComponent<SpriteRenderer>();
        }

        public void SetColor(ColorTypeSO newColor)
        {
            Color = newColor;
            sprite.sprite = newColor.colorSprite;

        }

    }
}