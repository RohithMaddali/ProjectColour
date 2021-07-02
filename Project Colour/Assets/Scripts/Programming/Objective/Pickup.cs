using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quontity
{
    public class Pickup : MonoBehaviour
    {
        //var for prism
        public GameObject prism;

        //when the player collides with object, it will check for tag, then add item to objective and destroy it
        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                if (CompareTag("GreenPrism"))
                {
                    ObjectiveManager.i.AddItemToObjective(0);
                    Destroy(prism);
                    Debug.Log("GreenGone");
                }
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                if (CompareTag("BluePrism"))
                {
                    ObjectiveManager.i.AddItemToObjective(1);
                    Destroy(prism);
                    Debug.Log("BlueGone");
                }
            }

            if (collision.gameObject.CompareTag("Player"))
            {
                if (CompareTag("RedPrism"))
                {
                    ObjectiveManager.i.AddItemToObjective(2);
                    Destroy(prism);
                    Debug.Log("RedGone");
                }
            }

        }
    }
}
