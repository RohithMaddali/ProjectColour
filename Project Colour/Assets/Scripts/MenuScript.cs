using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pyro
{
    public class MenuScript : MonoBehaviour
    {
        public void Quit()
        {
            Application.Quit();
            Debug.Log ("Application has quit.");
        }
    }
}
