using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using AJ;

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

        //These are for material swapping
        public GameObject orb;
        public Material greenMat;
        public Material RedMat;
        public Material blueMat;

        //for animation
        public Animator animator;

        //PRIVATE VARS
        private Color currentColor;
        private Color previousColor;
        private Renderer thisRenderer;
        private bool isCoroutineRunning; //Delay between shots
        Vector3 move;
        PlayerControls controls;
        public bool hasColour;

        public Camera cam;
        public RectTransform crosshair;
        RBMove player;

        private void Awake()
        {
            //get controls
            controls = new PlayerControls();
            //set controls for suck and shoot
            controls.Gameplay.Suck.performed += ctx => Suck();
            controls.Gameplay.Shoot.performed += ctx => Shoot();
            cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        }

        void OnEnable()
        {
            //yeah ni dunno lol call this if you need to turn off controls
            controls.Gameplay.Enable();
        }

        void OnDisable()
        {
            controls.Gameplay.Disable();
        }

        private void Start()
        {
            if (hasColour == true)
            {
                orb.SetActive(true);
            }
            else
            {
                orb.SetActive(false);
            }
            thisRenderer = orb.GetComponent<Renderer>();
            isCoroutineRunning = false;
            currentColor = thisRenderer.material.color;
            player = FindObjectOfType<RBMove>();
        }

        void Update()
        {
            if(currentColor == Color.green)
            {
                Debug.Log("Playing Green Voice Lines");
                AkSoundEngine.SetState("Colour", "Green");
            }
            if (currentColor == Color.blue)
            {
                Debug.Log("Playing blue Voice Lines");
                AkSoundEngine.SetState("Colour", "Blue");
            }
            if (currentColor == Color.red)
            {
                Debug.Log("Playing red Voice Lines");
                AkSoundEngine.SetState("Colour", "Red");
            }
        }

        /*private void FixedUpdate()
        {
            //Check to see if the object has that component, try not to make it dependent or it will break for testing.
            if (gameObject.GetComponentInParent<RBMove>() != null)
            {
                move = gameObject.GetComponentInParent<RBMove>().cam.forward;
            }
            else
            {
                Debug.Log("You don't have RBMove on the player");
                move = Vector3.forward;
            }

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
        }*/

        void Suck()
        {
            //shoot
            animator.SetTrigger("IsShooting");

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastToTarget;

            if (Physics.Raycast(ray, out raycastToTarget, shootDistance, raycastHitMask))
            {
                
                if (raycastToTarget.transform.gameObject.layer == 8 && !hasColour)
                {
                    
                    Debug.DrawLine(ray.origin, raycastToTarget.point, Color.green);
                    Renderer hitRenderer = raycastToTarget.transform.gameObject.GetComponent<Renderer>(); //get renderer of hit
                    if(hitRenderer.material.color == Color.red || hitRenderer.material.color == Color.blue || hitRenderer.material.color == Color.green) //chekc if object has special colour
                    {
                        orb.SetActive(true);
                        thisRenderer.material.color = hitRenderer.material.color; //make weapon objects colour
                        hitRenderer.material.color = Color.grey; //make object grey
                        hasColour = true; //no more suck!
                    }                    
                }
            }
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * shootDistance, Color.red);
            
        }

        void Shoot()
        {
            //shoot
            animator.SetTrigger("IsShooting");

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastToTarget;

            if (Physics.Raycast(ray, out raycastToTarget, shootDistance, raycastHitMask))
            {
                
                if (raycastToTarget.transform.gameObject.layer == 8 && raycastToTarget.transform.CompareTag("CanColour") && hasColour)
                {
                    Debug.DrawLine(ray.origin, raycastToTarget.point, Color.green);
                    Renderer hitRenderer = raycastToTarget.transform.gameObject.GetComponent<Renderer>();
                    if(hitRenderer.material.color != Color.red && hitRenderer.material.color != Color.blue && hitRenderer.material.color != Color.green) //check if object already has special colour so we can't lose it
                    {
                        orb.SetActive(false);
                        hitRenderer.material.color = thisRenderer.material.color; //make object stored colour
                        thisRenderer.material.color = Color.grey; //make stored color grey
                        hasColour = false; //allow suck again
                    }
                    
                }
            }
            Debug.DrawLine(ray.origin, ray.origin + ray.direction * shootDistance, Color.red);
            
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
