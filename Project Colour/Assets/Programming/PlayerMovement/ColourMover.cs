using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AJ
{
    public class ColourMover : MonoBehaviour
    {
        //public GameObject lastHit;
        public Vector3 collision = Vector3.zero;

        
        //public LayerMask layer;
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            //This is the next step to complete once I get that uncoloured box fixed correctly.
            if (Input.GetKeyDown("E"))
            {
                
            }
            
            
            //var ray = new Ray(transform.position, transform.forward);
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit, 100))
            //{
            //lastHit = GameObject.FindWithTag("Target");
            //collision = hit.point;
            //}
        
            var transform = this.transform;
            
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 100.0f))
            {
                Color color = hit.transform.gameObject.GetComponent<Renderer>().material.color;
                collision = hit.point;
                print("Object colour: " + ToString());
            }
            else if (!Physics.Raycast(transform.position, transform.forward, out hit))
            {
                print("DID NOT HIT A COLOURED OBJECT!! ");
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(collision, 0.2f);
        }
    }
}

