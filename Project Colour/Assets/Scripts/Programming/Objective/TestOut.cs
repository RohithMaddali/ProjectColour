using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Quontity
{
    public class TestOut : MonoBehaviour
    {
        //var for tracking panel 
        public GameObject trackingPanel;
        //keeps tracking panel hidden when played
        bool Paused = false;
        PlayerControls controls;

        private void Awake()
        {
            controls = new PlayerControls();

            controls.Gameplay.Objectives.performed += ctx => ObjectiveMenu();
        }
        void OnEnable()
        {
            controls.Gameplay.Enable();
        }

        void OnDisable()
        {
            controls.Gameplay.Disable();
        }
        void Start()
        {
            //loads all objective onto the tracking panel
            ObjectiveManager.i.TriggerDisplayPanel(0);
            ObjectiveManager.i.TriggerDisplayPanel(1);
            ObjectiveManager.i.TriggerDisplayPanel(2);
            trackingPanel.gameObject.SetActive(false);
        }

        void ObjectiveMenu()
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

        void Update()
        {
           
        }

    }
}
