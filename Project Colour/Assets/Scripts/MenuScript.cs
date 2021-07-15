using System.Collections;
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

        public GameObject mainMenuFirst;
        public GameObject newGameFirst;
        public GameObject settingsMenuFirst;
        public GameObject controlsMenu;
        public GameObject quitFirst;
        
        public void Quit()
        {
            Application.Quit();
            Debug.Log("Application has quit.");
        }

        public void OpenSubmenu(GameObject delta)
        {
            Debug.Log(delta.gameObject);
            delta.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            if (delta.gameObject.CompareTag("newGame"))
                Debug.Log("Setting first selection to yes");
            EventSystem.current.SetSelectedGameObject(newGameFirst);
            if (delta.gameObject.tag == "settings")
                EventSystem.current.SetSelectedGameObject(settingsMenuFirst);
            if (delta.gameObject.tag == "controls")
                EventSystem.current.SetSelectedGameObject(controlsMenu);
            if (delta.gameObject.tag == "quit")
                EventSystem.current.SetSelectedGameObject(quitFirst);
        }

        public void CloseSubmenu(GameObject delta)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(mainMenuFirst);
            delta.SetActive(false);
        }

        public void LoadScene()
        {
            SceneManager.LoadScene("AdditiveSceneTest", LoadSceneMode.Single);
        }
    }
}
