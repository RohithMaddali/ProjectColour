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
        public int currentItemNumber;
        public List<string> actualObjectName;

        public void AddActualIndex(string _name)
        {
            if (!actualObjectName.Contains(_name))
            {
                actualObjectName.Add(_name);
            }
        }

        public void AddCurrentItemNumber()
        {
            if (currentItemNumber < maxItemNumber)
            {
                currentItemNumber += 1;
            }

        }

        public string GetObjectiveName()
        {
            return objectiveName + "\t" + currentItemNumber.ToString() + " / " + maxItemNumber.ToString();
        }
    }
    public class ObjectiveManager : MonoBehaviour
    {
        private static ObjectiveManager _i;
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

        public GameObject objectiveTrackingPanel;

        public List<SingleObjective> objectives = new List<SingleObjective>();
        private List<GameObject> objectiveInTrakingPanel = new List<GameObject>();

        public GameObject objectivePrefab;

        void Start()
        {
            InitObjectives();
        }
        private void InitObjectives()
        {
            for (int i = 0; i < objectives.Count; i++)
            {
                objectiveTrackingPanel.transform.GetChild(i).GetComponentInChildren<Text>().text = objectives[i].GetObjectiveName();
                objectiveInTrakingPanel.Add(objectiveTrackingPanel.transform.GetChild(i).gameObject);
            }
        }

        public void AddItemToObjective(int _objectiveIndex, string _name)
        {
            objectives[_objectiveIndex].AddCurrentItemNumber();
            objectives[_objectiveIndex].AddActualIndex(_name);
            objectiveInTrakingPanel[_objectiveIndex].GetComponentInChildren<Text>().text = objectives[_objectiveIndex].GetObjectiveName();

        }

        public int GetObjectiveCurrentNumber(int _objectiveIndex)
        {
            return objectives[_objectiveIndex].currentItemNumber;
        }


        public void SetObjectiveCurrentNumber(int _objectiveIndex, int _newNumber)
        {
            objectives[_objectiveIndex].currentItemNumber = _newNumber;
            objectiveInTrakingPanel[_objectiveIndex].GetComponentInChildren<Text>().text = objectives[_objectiveIndex].GetObjectiveName();
        }
        public void ClearObjective()
        {
            for (int i = 0; i < objectives.Count; i++)
            {
                objectives[i].currentItemNumber = 0;
                objectiveInTrakingPanel[i].GetComponentInChildren<Text>().text = objectives[i].GetObjectiveName();
                objectives[i].actualObjectName.Clear();
            }
        }

    }
}
