using System.Collections;
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
        public int index;
        void Awake()
        {
            gm = FindObjectOfType<GameManager>();
        }
        void Start()
        {
            Debug.Log("fucking shit");
            for (int i = 0; i < ObjectiveManager.i.objectives.Count; i++)
            {
                for (int j = 0; j < ObjectiveManager.i.objectives[i].actualObjectName.Count; j++)
                {
                    if (ObjectiveManager.i.objectives[i].actualObjectName[j] == transform.gameObject.tag + index.ToString())
                    {
                        Destroy(this.gameObject);
                    }
                }
            }
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
                    ObjectiveManager.i.AddItemToObjective(0, transform.tag + index.ToString());
                    gm.gPrism += 1;
                    AkSoundEngine.PostEvent("ev_gem_collect", gameObject);
                    Instantiate(sfx, prism.transform.position, Quaternion.identity);
                    gm.itemsPicked += 1;
                    gameObject.SetActive(false);
                    //Destroy(prism);
                }
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                if (CompareTag("BluePrism"))
                {
                    ObjectiveManager.i.AddItemToObjective(1, transform.tag + index.ToString());
                    gm.bPrism += 1;
                    AkSoundEngine.PostEvent("ev_gem_collect", gameObject);
                    Instantiate(sfx, prism.transform.position, Quaternion.identity);
                    gm.itemsPicked += 1;
                    gameObject.SetActive(false);
                    //Destroy(prism); 
                }
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                if (CompareTag("RedPrism"))
                {
                    ObjectiveManager.i.AddItemToObjective(2, transform.tag + index.ToString());
                    gm.rPrism += 1;
                    AkSoundEngine.PostEvent("ev_gem_collect", gameObject);
                    Instantiate(sfx, prism.transform.position, Quaternion.identity);
                    gm.itemsPicked += 1;
                    gameObject.SetActive(false);
                    //Destroy(prism);
                }
            }
        }
    }
}
