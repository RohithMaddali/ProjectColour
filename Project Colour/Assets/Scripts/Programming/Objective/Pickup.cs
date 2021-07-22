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
            if (ObjectiveManager.i.storedItem.Contains(transform.tag + index.ToString()))
            {
                Destroy(this.gameObject);
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
                    ObjectiveManager.i.AddItemToObjective(0);
                    ObjectiveManager.i.AddToStore(this);
                    Instantiate(sfx, prism.transform.position, Quaternion.identity);
                    gm.itemsPicked += 1;
                    Destroy(prism);
                }
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                if (CompareTag("BluePrism"))
                {
                    ObjectiveManager.i.AddItemToObjective(1);
                    ObjectiveManager.i.AddToStore(this);
                    Instantiate(sfx, prism.transform.position, Quaternion.identity);
                    gm.itemsPicked += 1;
                    Destroy(prism); 
                }
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                if (CompareTag("RedPrism"))
                {
                    ObjectiveManager.i.AddItemToObjective(2);
                    ObjectiveManager.i.AddToStore(this);
                    Instantiate(sfx, prism.transform.position, Quaternion.identity);
                    gm.itemsPicked += 1;
                    Destroy(prism);
                }
            }
        }
    }
}
