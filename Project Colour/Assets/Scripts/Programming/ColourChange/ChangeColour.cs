using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AJ;

namespace AJ
{
    public class ChangeColour : MonoBehaviour
    {
        private Color previousColor;
        public Color currentColor;

        
        public void ChangeTo(Color newColor)
        {
            foreach (Renderer renderer in transform.GetComponentsInChildren<Renderer>())
            {
                Material mat = renderer.material;
                previousColor           = mat.color;
                renderer.material.color = newColor;
                currentColor            = newColor;
            }
            //ChangeTo(newColor);
        }

        /*public bool IsSameColour(Material a, Material b)
        {
            // TODO
            return true;
        }*/
    }
}
