﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

namespace Pyro
{
    public class MenuScript : MonoBehaviour
    {
        public GameObject confirm;
        public GameObject settingsMenu;
        public GameObject MainMenu;
        public GameObject PauseMenu;
        public GameObject Title;
        public GameObject feedbackButton;
        public GameObject mainMenuPrompt;
        public GameObject mainMenu;

        public SettingsMenu sm;

        public GameObject mainMenuFirst;
        public GameObject newGameFirst;
        public GameObject settingsMenuFirst;
        public GameObject controlsMenu;
        public GameObject quitFirst;
        public GameManager gm;

        public GameObject tips;

        public Scene feed;

        private void Start()
        {
            sm = settingsMenu.GetComponent<SettingsMenu>();
        }

        public void TitleButton()
        {
            //StartCoroutine(loadTransitionAnim());
            Title.SetActive(false);
            SceneManager.LoadScene(7);
            MainMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(mainMenuFirst);
        }

        public void Quit()
        {
            Application.Quit();
            Debug.Log("Application has quit.");
        }

        public void OpenSubmenu(GameObject delta)
        {
            mainMenu.SetActive(false);
            Debug.Log(delta.gameObject);
            delta.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            if (delta.gameObject.CompareTag("newGame"))
                Debug.Log("Setting first selection to yes");
            EventSystem.current.SetSelectedGameObject(newGameFirst);
            if (delta.gameObject.tag == "settings")
            {
                EventSystem.current.SetSelectedGameObject(settingsMenuFirst);
                sm.lastMenu = sm.mainMenu;
            }
            if (delta.gameObject.tag == "controls")
                EventSystem.current.SetSelectedGameObject(controlsMenu);
            if (delta.gameObject.tag == "quit")
                EventSystem.current.SetSelectedGameObject(quitFirst);
        }

        public void CloseSubmenu(GameObject delta)
        {
            mainMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(mainMenuFirst);
            delta.SetActive(false);
        }

        public void LoadScene()
        {
            //new game function
            gm.isPicked = false;
            gm.itemsPicked = 0;
            gm.r = false;
            gm.b = false;
            gm.g = false;
            gm.red = null;
            gm.green = null;
            gm.blue = null;
            gm.gPrism = 0;
            gm.rPrism = 0;
            gm.bPrism = 0;
            gm.terrain.SetFloat("Saturation", 0.25f);
            SceneManager.LoadScene("CutScene", LoadSceneMode.Single);
            MainMenu.SetActive(false);
            PauseMenu.SetActive(true);

        }
        public void Continue()
        {
            gm.displayTips = false;
            gm.toggle.isOn = false;
            gm.continuing = true;
            SceneManager.LoadScene("AdditiveSceneTest");
            MainMenu.SetActive(false);
            PauseMenu.SetActive(true);
        }

        public void feedback()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            MainMenu.SetActive(false);
            PauseMenu.SetActive(false);
            feedbackButton.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(feedbackButton);
        }

        public void backFromFeedback()
        {
            SceneManager.LoadScene(7);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mainMenuPrompt.SetActive(false);
            feedbackButton.SetActive(false);
            MainMenu.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(mainMenuFirst);
        }
    }
}
