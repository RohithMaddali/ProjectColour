using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AJ
{
    public class ColourMover : MonoBehaviour
    {
        //public GameObject lastHit;
        private Color myColor;
        public Color currentColor;
        public Color lastColor;
        RaycastHit hit;
        public Vector3 collision = Vector3.zero;
        public GameObject weapon;
        //public LayerMask layer;

        
        void Update()
        {
            var transform = this.transform;

            if (Physics.Raycast(transform.position, transform.forward, out hit, 10.0f))
            {
                if (hit.transform.GetComponent<ColouredObject>())
                {
                    currentColor = hit.transform.gameObject.GetComponent<Renderer>().material.color;
                    collision = hit.point;
                    print("Object colour: " + ToString());
                }
            }
            else if (!Physics.Raycast(transform.position, transform.forward, out hit, 10.0f))
            {
                print("DID NOT HIT A COLOURED OBJECT!! ");
            }
            ColourSwap();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(collision, 0.2f);
        }
        
        void ColourSwap()
        {
            //This is the next step to complete once I get that uncoloured box fixed correctly.
            
             //remove a colour from an object, store it on the weapon, place colour on another object
             if (Input.GetMouseButton(0))
             {
                 if (!(hit.transform is null))
                 {
                     currentColor = weapon.transform.GetComponent<Renderer>().material.color;
                     lastColor = hit.transform.GetComponent<Renderer>().material.color;
                     hit.transform.GetComponent<Renderer>().material.color = currentColor;
                     weapon.transform.GetComponent<Renderer>().material.color = lastColor;
                 }
             }
            
        }
    }
}

//var ray = new Ray(transform.position, transform.forward);
//RaycastHit hit;
//if (Physics.Raycast(ray, out hit, 100))
//{
//lastHit = GameObject.FindWithTag("Target");
//collision = hit.point;
//}


// weapon.transform.GetComponent<Renderer>().material.color = currentColor;
//hit.transform.GetComponent<Renderer>().material.color = Color.gray;
                    
                    
//changes the shade of the coloured object to a lighter shade (if you change the + to - it darkens the shade)
//hit.transform.GetComponent<Renderer>().material.color +=
//weapon.transform.GetComponent<Renderer>().material.color;
