using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public RBMove player;
    public GameObject crossHair;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<RBMove>();
    }

    void Update()
    {
        if (player.moveCamActive)
        {
            crossHair.SetActive(false);
        }
        else
        {
            crossHair.SetActive(true);
        }
    }

}
