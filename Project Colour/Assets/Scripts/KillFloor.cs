using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFloor : MonoBehaviour
{
    public Transform respawnPoint;
    public Rigidbody playerRB;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            playerRB.velocity = Vector3.zero;
            player.transform.position = respawnPoint.position;
        }
    }
}
