﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Quontity
{
    public class Pickup : MonoBehaviour
    {
        //var for prism
        public GameObject prism;
        public GameObject sfx;
        public GameManager gm;
        void Awake()
        {
            gm = FindObjectOfType<GameManager>();
        }

        //when the player collides with object, it will check for tag, then add item to objective and destroy it
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if(gm.itemsPicked == 5)
                {
                    gm.isPicked = true;
                }

                if (CompareTag("GreenPrism"))
                {
                    ObjectiveManager.i.AddItemToObjective(0);
                    Instantiate(sfx, prism.transform.position, Quaternion.identity);
                    gm.itemsPicked += 1;
                    Destroy(prism);
                    Debug.Log("GreenGone");
                }
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                if (CompareTag("BluePrism"))
                {
                    ObjectiveManager.i.AddItemToObjective(1);
                    Instantiate(sfx, prism.transform.position, Quaternion.identity);
                    gm.itemsPicked += 1;
                    Destroy(prism);
                    Debug.Log("BlueGone");    
                }
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                if (CompareTag("RedPrism"))
                {
                    ObjectiveManager.i.AddItemToObjective(2);
                    Instantiate(sfx, prism.transform.position, Quaternion.identity);
                    gm.itemsPicked += 1;
                    Destroy(prism);
                    Debug.Log("RedGone");  
                }
            }
        }
    }
}
