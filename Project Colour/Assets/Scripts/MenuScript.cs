using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pyro
{
    public class MenuScript : MonoBehaviour
    {
        public GameObject confirm;
        public GameObject settingsMenu;

        public void Quit()
        {
            Application.Quit();
            Debug.Log("Application has quit.");
        }

        public void OpenSubmenu(GameObject delta)
        {
            delta.SetActive(true);
        }

        public void CloseSubmenu(GameObject delta)
        {
            delta.SetActive(false);
        }

        public void LoadScene()
        {
            SceneManager.LoadScene("AdditiveSceneTest", LoadSceneMode.Single);
        }
    }
}
