using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quontity;


public class GameFlowManager : MonoBehaviour
{
    private static GameFlowManager _i;
    public static GameFlowManager i
    {
        get
        {
            if (_i == null)
            {
                _i = FindObjectOfType<GameFlowManager>();
            }
            return _i;
        }
    }

    public enum GameState
    {
        MainMenu,
        NewGame,
        Continue,
    }

    public GameState state;
    public GameObject objectiveUI;

    private void Start()
    {
        state = GameState.MainMenu;
        objectiveUI.SetActive(false);
    }

    public void NewGame()
    {
        state = GameState.NewGame;
        ObjectiveManager.i.ClearObjective();
        PlayerPrefs.DeleteAll();
        objectiveUI.SetActive(true);
    }

    public void Continue()
    {
        state = GameState.Continue;
        //ObjectiveManager.i.ClearObjective();
        LoadObjectiveData();
        objectiveUI.SetActive(true);
    }

    public void Back()
    {
        state = GameState.MainMenu;
        objectiveUI.SetActive(false);
    }

    public void SaveObjectiveData()
    {
        PlayerPrefs.SetInt("Objective01", ObjectiveManager.i.GetObjectiveCurrentNumber(0));
        PlayerPrefs.SetInt("Objective02", ObjectiveManager.i.GetObjectiveCurrentNumber(1));
        PlayerPrefs.SetInt("Objective03", ObjectiveManager.i.GetObjectiveCurrentNumber(2));
        for (int i = 0; i < ObjectiveManager.i.objectives[0].actualObjectName.Count; i++)
        {
            PlayerPrefs.SetString("GreenPrism" + i, ObjectiveManager.i.objectives[0].actualObjectName[i]);
        }
        for (int i = 0; i < ObjectiveManager.i.objectives[1].actualObjectName.Count; i++)
        {
            PlayerPrefs.SetString("BluePrism" + i, ObjectiveManager.i.objectives[1].actualObjectName[i]);
        }
        for (int i = 0; i < ObjectiveManager.i.objectives[2].actualObjectName.Count; i++)
        {
            PlayerPrefs.SetString("RedPrism" + i, ObjectiveManager.i.objectives[2].actualObjectName[i]);
        }
    }

    public void LoadObjectiveData()
    {
        ObjectiveManager.i.SetObjectiveCurrentNumber(0, PlayerPrefs.GetInt("Objective01"));
        ObjectiveManager.i.SetObjectiveCurrentNumber(1, PlayerPrefs.GetInt("Objective02"));
        ObjectiveManager.i.SetObjectiveCurrentNumber(2, PlayerPrefs.GetInt("Objective03"));
        for (int i = 0; i < ObjectiveManager.i.objectives[0].currentItemNumber; i++)
        {
            ObjectiveManager.i.objectives[0].actualObjectName.Add(PlayerPrefs.GetString("GreenPrism" + i));
        }
        for (int i = 0; i < ObjectiveManager.i.objectives[1].currentItemNumber; i++)
        {
            ObjectiveManager.i.objectives[1].actualObjectName.Add(PlayerPrefs.GetString("BluePrism" + i));
        }
        for (int i = 0; i < ObjectiveManager.i.objectives[2].currentItemNumber; i++)
        {
            ObjectiveManager.i.objectives[2].actualObjectName.Add(PlayerPrefs.GetString("RedPrism" + i));
        }
    }
}

