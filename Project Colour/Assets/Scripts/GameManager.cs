using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public CinemachineFreeLook moveCam;
    public Slider camSlider;
    public Slider aimSlider;
    public float mouseSenX = 100f;
    public float mouseSenY = 1.5f;
    public bool isPicked = false;

    public float aimSen;
    public CharacterAim ca;

    public int itemsPicked;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Update()
    {
        if (itemsPicked == 6 && isPicked == true)
        {
            SceneManager.LoadScene(6);
            isPicked = false;
        }
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
