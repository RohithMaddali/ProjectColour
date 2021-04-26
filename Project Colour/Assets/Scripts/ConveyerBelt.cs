using System.Collections;
using System.Collections.Generic;
using AJ;
using UnityEngine;

namespace Jordan
{
    public class ConveyerBelt : MonoBehaviour
    {
        public GameObject belt;
        public Transform endpoint;
        bool isOn = false;
        public float timer;
        public int speed;
        private void OnTriggerStay(Collider col)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isOn = true;
                Debug.Log("Belt is on");
                StartCoroutine("BeltToggle");
            }

            if (isOn == true)
            {
                col.transform.position = Vector3.MoveTowards(col.transform.position, endpoint.transform.position, speed * Time.deltaTime);
            }
        }

        IEnumerator BeltToggle()
        {
            yield return new WaitForSeconds(timer);
            isOn = false;
            Debug.Log("Belt is off");
        }
    }

}
