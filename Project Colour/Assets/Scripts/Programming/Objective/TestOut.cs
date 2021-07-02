using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOut : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) {
            ObjectiveManager.i.TriggerDisplayPanel(0);
        }
        if (Input.GetKeyDown(KeyCode.G)) {
            ObjectiveManager.i.AddItemToObjective(0);
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            ObjectiveManager.i.AddItemToObjective(1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ObjectiveManager.i.AddItemToObjective(2);
        }
    }
}
