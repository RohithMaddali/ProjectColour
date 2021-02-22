using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AJ;

namespace AJ
{
    public class SlimeEnum : MonoBehaviour
    {
        public Color color;
        //public bool changeInRunTime; 
        public enum ColorType
        {
            Red,
            Green,
            Blue
        }

        public ColorType currentColorType;

        void ChangeColor()
        {
            GetComponent<Renderer>().sharedMaterial.color = color;
        }


        void Update()
        {
            switch(currentColorType)
            {
                case ColorType.Green:
                    color = Color.green;
                    Debug.Log("Green");
                    break;
                case ColorType.Red:
                    color = Color.red;
                    Debug.Log("Red");
                    break;
                case ColorType.Blue:
                    color = Color.blue;
                    Debug.Log("Orange");
                    break;                
            }
            ChangeColor();
            /*if (changeInRunTime)
            {
                ChangeColor();
            }*/

        }
        /*
        public static implicit operator Color(SlimeEnum v)
        {
            throw new NotImplementedException();
        }*/
    }
}