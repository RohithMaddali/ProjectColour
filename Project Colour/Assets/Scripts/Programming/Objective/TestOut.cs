using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quontity
{
    public class TestOut : MonoBehaviour
    {
        //var for tracking panel 
        public GameObject trackingPanel;
        //keeps tracking panel hidden when played
        bool Paused = false;
        void Start()
        {
            //loads all objective onto the tracking panel
            ObjectiveManager.i.TriggerDisplayPanel(0);
            ObjectiveManager.i.TriggerDisplayPanel(1);
            ObjectiveManager.i.TriggerDisplayPanel(2);
            trackingPanel.gameObject.SetActive(false);
        }

        void Update()
        {
            //acts similiar to a pause menu, pressing TAB will pause the menu and display current objectives
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (Paused == true)
                {
                    Time.timeScale = 1.0f;
                    trackingPanel.gameObject.SetActive(false);
                    Paused = false;
                }
                else
                {
                    Time.timeScale = 0.0f;
                    trackingPanel.gameObject.SetActive(true);
                    Paused = true;

                }
            }
        }

    }
}
