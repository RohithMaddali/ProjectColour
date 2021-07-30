using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFloor : MonoBehaviour
{
    public Transform respawnPoint;
    public Rigidbody playerRB;
    public GameObject player;
    [SerializeField] float respawnDelay = 0.2f;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            AkSoundEngine.PostEvent("ev_water_splash", gameObject);
            StartCoroutine(Respawn());
        }
    }
    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnDelay);
        playerRB.velocity = Vector3.zero;
        player.transform.position = respawnPoint.position;
    }
}
