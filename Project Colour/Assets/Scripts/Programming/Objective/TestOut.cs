using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Pyro;
namespace Quontity
{
    public class TestOut : MonoBehaviour
    {
        public PauseMenu pm;
        public GameObject mm;
        public GameObject tm;
        public MenuScript menu;
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
        }

        void ObjectiveMenu()
        {
            if (!pm.pauseMenuUI.activeInHierarchy && !mm.activeSelf && !tm.activeSelf)
            {
                if (trackingPanel.gameObject.activeInHierarchy)
                {
                    trackingPanel.gameObject.SetActive(false);
                }
                else
                {
                    trackingPanel.gameObject.SetActive(true);
                }
            }
        }

        void Update()
        {
        }

    }
}
