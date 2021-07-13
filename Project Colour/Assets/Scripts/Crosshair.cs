using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    public RBMove player;
    public GameObject crossHair;
    public PauseMenu pm;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<RBMove>();
        pm = FindObjectOfType<PauseMenu>();
    }

    void Update()
    {
        if (player.moveCamActive && !pm.GameIsPaused)
        {
            crossHair.SetActive(false);
        }
        else if (!player.moveCamActive && !pm.GameIsPaused)
        {
            crossHair.SetActive(true);
        }
        else if (pm.GameIsPaused)
        {
            crossHair.SetActive(false);
        }
    }

}
