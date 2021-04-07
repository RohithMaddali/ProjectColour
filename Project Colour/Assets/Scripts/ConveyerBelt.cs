using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyerBelt : MonoBehaviour
{
    public GameObject belt;
    public Transform endpoint;
    public int speed;
    private void OnTriggerStay(Collider other)
    {
        other.transform.position = Vector3.MoveTowards(other.transform.position, endpoint.transform.position, speed * Time.deltaTime);
    }
}
