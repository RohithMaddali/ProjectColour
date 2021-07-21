﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using Pyro;

public class GameManager : MonoBehaviour
{
    public bool gamepadInUse;

    public CinemachineFreeLook moveCam;
    public Slider camSlider;
    public Slider aimSlider;
    public float mouseSenX = 100f;
    public float mouseSenY = 1.5f;
    public bool isPicked = false;

    public float aimSen;
    public CharacterAim ca;

    public MenuScript mm;

    public int itemsPicked;

    private void Awake()
    {
        mm = FindObjectOfType<MenuScript>();
        DontDestroyOnLoad(this);
    }
    private void Start()
    {
        if(Gamepad.current == null)
        {
            Debug.Log("no gamepad connected");
            gamepadInUse = false;
            //use keyboard and mouse ui
        }
        else
        {
            Debug.Log(Gamepad.current.displayName + "Gamepad connected");
            gamepadInUse = true;
            //use gamepad ui
        }
    }
    public void Update()
    {
        if (itemsPicked == 6 && isPicked == true)
        {
            SceneManager.LoadScene(6);
            mm.feedback();
            isPicked = false;
        }

        InputSystem.onDeviceChange +=
            (device, change) =>
            {
                switch (change)
                {
                    case InputDeviceChange.Added:
                        //switch to gamepad ui
                        gamepadInUse = true;
                        Debug.Log("New device added: " + device);
                        break;

                    case InputDeviceChange.Removed:
                        //switch to KM ui
                        gamepadInUse = false;
                        Debug.Log("Device removed: " + device);
                        break;
                }
            };

    }

    public void ChangeMouseSensitivity()
    {
        mouseSenX = Mathf.Clamp(camSlider.value * 1f, 0.01f, 1f);
        mouseSenY = Mathf.Clamp(camSlider.value * 3f, 0.5f, 3f);
        if (moveCam != null)
        {
            moveCam.m_XAxis.m_MaxSpeed = mouseSenX;
            moveCam.m_YAxis.m_MaxSpeed = mouseSenY;
        }
        Debug.Log("MOUSEX SENSITIVITY IS " + mouseSenX);
        Debug.Log("MOUSEY SENSITIVITY IS " + mouseSenY);
    }

    public void ChangeAimSensitivity()
    {
        aimSen = Mathf.Clamp(aimSlider.value * 20f, 1, 20);
        if(ca != null)
        {
            ca.mouseSensitivity = aimSen;
        }
        Debug.Log("AIM SENSITIVITY IS " + aimSen);
    }
}
