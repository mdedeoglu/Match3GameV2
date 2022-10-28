using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Match3
{
    public class CameraController : MonoBehaviour
    {

        public void SetupCamera(int cols, int rows)
        {
            if (cols >= rows)
            {
                Camera.main.orthographicSize = (cols / 2f) / Screen.width * Screen.height;
            }
            else
            {
                Camera.main.orthographicSize = (rows / 1.2f);
            }
        }


    }
}