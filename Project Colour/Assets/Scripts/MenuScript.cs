using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pyro
{
    public class MenuScript : MonoBehaviour
    {
        public GameObject confirm;

        public void Quit()
        {
            Application.Quit();
            Debug.Log("Application has quit.");
        }

        public void AskToQuit()
        {
            confirm.SetActive(true);
        }

        public void CancelQuit()
        {
            confirm.SetActive(false);
        }
    }
}
