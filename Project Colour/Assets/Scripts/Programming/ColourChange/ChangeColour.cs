using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AJ;
using UnityEngine.InputSystem;

namespace AJ
{
    public class ChangeColour : MonoBehaviour
    {
        //PUBLIC VARS
        [Tooltip("How far can we select colour from")]
        public float shootDistance;
        [Tooltip("Expects between 1-3 seconds")]
        public int shootDelay;
        [Tooltip("What should the raycast hit")]
        public LayerMask raycastHitMask;
        [Tooltip("Is the colour changing object one object or multiple")]
        public bool multiObjectShoot;
        //PRIVATE VARS
        private Color currentColor;
        private Color previousColor;
        private Renderer thisRenderer;
        private bool isCoroutineRunning; //Delay between shots
        Vector3 move;
        

        private void Start()
        {
            thisRenderer = GetComponent<Renderer>();
            isCoroutineRunning = false;
            currentColor = thisRenderer.material.color;
            
        }

        private void FixedUpdate()
        {
            move = gameObject.GetComponentInParent<LukesMovement>().moveDir;
            if (Mouse.current.leftButton.isPressed)
            {
                Ray ray = new Ray(transform.position, move);
                RaycastHit raycastToTarget;
            
                if (Physics.Raycast(ray, out raycastToTarget, shootDistance, raycastHitMask))
                {
                    if (isCoroutineRunning != true && raycastToTarget.transform.gameObject.layer == 31)
                    {
                        Debug.DrawLine(ray.origin, raycastToTarget.point, Color.green);
                        Renderer hitRenderer = raycastToTarget.transform.gameObject.GetComponent<Renderer>();
                        StartCoroutine(ChangeCurrentColour(hitRenderer));
                    }
                }
                else
                {
                    Debug.DrawLine(ray.origin, ray.origin + ray.direction * shootDistance, Color.red);
                }
            }
        }

        IEnumerator ChangeCurrentColour(Renderer hitRenderer)
        {
            isCoroutineRunning = true;
            if (multiObjectShoot != true)
            {
                previousColor = currentColor; //Store our current colour
                currentColor = hitRenderer.material.color; //Set current colour to the object hit's
                hitRenderer.material.color = previousColor; //Change the objects colour to our colour
                thisRenderer.material.color = currentColor; //Change our colour to the object's
            }
            else
            {
                previousColor = currentColor; //Store our current colour
                currentColor = hitRenderer.material.color; //Take the object hit's colour
                foreach (Renderer renderer in transform.GetComponentsInChildren<Renderer>())
                {
                    renderer.material.color = currentColor; //For every object in this gun, change it's colour
                }
                hitRenderer.material.color = previousColor; //Change the hit objects colour
                Debug.Log("CURRENTLY MULTI SHOOT IS WIP");
            }
            yield return new WaitForSeconds(shootDelay);
            isCoroutineRunning = false;
        }
    }
}
