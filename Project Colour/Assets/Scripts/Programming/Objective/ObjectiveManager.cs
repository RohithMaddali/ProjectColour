using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Quontity
{
    [System.Serializable]
    public class SingleObjective
    {
        public string objectiveName;
        public int maxItemNumber;
        private int currentItemNumber;
        public UnityEvent OnCompletedObjective;

        public void LoadValues(int value)
        {
            currentItemNumber = value;
        }
        public int GetCurrentItemValue()
        {
            return currentItemNumber;
        }
        public void AddCurrentItemNumber()
        {
            if (currentItemNumber < maxItemNumber)
            {
                currentItemNumber += 1;
            }
            if (currentItemNumber == maxItemNumber)
            {
                OnCompletedObjective?.Invoke();
            }
        }

        public string GetObjectiveName()
        {
            return objectiveName + "   " + currentItemNumber.ToString() + " / " + maxItemNumber.ToString();
        }
    }

    public class ObjectiveManager : MonoBehaviour
    {
        private static ObjectiveManager _i;
        public GameObject mm;
        public GameObject tm;
        public static ObjectiveManager i
        {
            get
            {
                if (_i == null)
                {
                    _i = FindObjectOfType<ObjectiveManager>();
                }
                return _i;
            }
        }

        //public GameObject objectiveDisplayPanel;
        public GameObject objectiveTrackingPanel;

        public List<SingleObjective> objectives = new List<SingleObjective>();
        private List<GameObject> objectiveInTrackingPanel = new List<GameObject>();

        public GameObject objectivePrefab;
        [SerializeField] private Scene currentScene;

        void Start()
        {
            currentScene = SceneManager.GetActiveScene();
            //objectiveDisplayPanel.SetActive(false);
            //objectiveTrackingPanel.SetActive(false);
        }

        public void TriggerDisplayPanel(int _objectiveIndex)
        {
            string sceneName = currentScene.name;
            //objectiveDisplayPanel.SetActive(true);
            //objectiveDisplayPanel.GetComponentInChildren<Text>().text = objectives[_objectiveIndex].objectiveName;
            if (!mm.activeSelf) 
            {
               GameObject o = Instantiate(objectivePrefab);
               o.transform.SetParent(objectiveTrackingPanel.transform, false);
               o.GetComponentInChildren<Text>().text = objectives[_objectiveIndex].GetObjectiveName();
               objectiveInTrackingPanel.Add(o);
            } 
        }

        public void AddItemToObjective(int _objectiveIndex)
        {
            objectives[_objectiveIndex].AddCurrentItemNumber();
            objectiveInTrackingPanel[_objectiveIndex].GetComponentInChildren<Text>().text = objectives[_objectiveIndex].GetObjectiveName();
        }

        public void AllObjectiveCompleted()
        {
            //objectiveDisplayPanel.SetActive(true);
            //objectiveDisplayPanel.GetComponentInChildren<Text>().text = "Completed!";
        }
    }
}
