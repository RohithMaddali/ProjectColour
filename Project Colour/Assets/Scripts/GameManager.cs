using System.Collections;
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

    public CinemachineVirtualCamera moveCam;
    public Slider camSlider;
    public Slider aimSlider;
    public float mouseSenX = 100f;
    public float mouseSenY = 1.5f;
    public bool isPicked = false;
    public bool displayTips = true;
    public bool continuing;

    public float aimSen;
    public CharacterAim ca;

    public MenuScript mm;

    public int itemsPicked;
    public int gPrism, rPrism, bPrism;
    public bool r, g, b;

    public GameObject red, green, blue;
    public Material Red, Green, Blue;
    public Material terrain;
    public ColourList[] colourLists;
    public static bool isVoicePlaying;
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
    public AK.Wwise.Event voiceOver;
    void CallBackFunction(object in_cookie, AkCallbackType callType, object in_info)
    {
        if (callType == AkCallbackType.AK_EndOfEvent)
        {
            isVoicePlaying = false;
        }
    }
    public void Update()
    {
        if (itemsPicked == 6 && isPicked == true)
        {
            //SceneManager.LoadScene(6);
            //mm.feedback();
            isPicked = false;
        }

        if(gPrism == 2 && g== false)
        {
            //green = GameObject.FindGameObjectWithTag("GreenPrism");
            g = true;
            green.GetComponent<Renderer>().material = Green;
            voiceOver.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, CallBackFunction);
            isVoicePlaying = true;
        }

        if (bPrism == 2 && b == false)
        {
            //blue = GameObject.FindGameObjectWithTag("BluePrism");
            b = true;
            blue.GetComponent<Renderer>().material = Blue;
            voiceOver.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, CallBackFunction);
            isVoicePlaying = true;
        }

        if (rPrism == 2 && r == false)
        {
            //red = GameObject.FindGameObjectWithTag("RedPrism");
            r = true;
            red.GetComponent<Renderer>().material = Red;
            voiceOver.Post(gameObject, (uint)AkCallbackType.AK_EndOfEvent, CallBackFunction);
            isVoicePlaying = true;
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

    public void TipDisplay()
    {
        displayTips = !displayTips;
    }

    public void ChangeMouseSensitivity()
    {
        mouseSenX = Mathf.Clamp(camSlider.value * 0.7f, 0.001f, 0.7f);
        mouseSenY = Mathf.Clamp(camSlider.value * 0.9f, 0.005f, 0.9f);
        if (moveCam != null)
        {
            moveCam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = mouseSenY;
            moveCam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = mouseSenX;
            /*moveCam.m_XAxis.m_MaxSpeed = mouseSenX;
            moveCam.m_YAxis.m_MaxSpeed = mouseSenY;*/
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

    public void Endgame()
    {
        terrain.SetFloat("Saturation", 1f);
        colourLists = FindObjectsOfType<ColourList>();
        foreach (ColourList colourList in colourLists)
        {
            foreach (GameObject colourableObject in colourList.colourableObjects)
            {
                //colourableObject.GetComponent<Renderer>().material.color = colourList.mycolour;
                colourableObject.GetComponent<Renderer>().material.SetFloat("Saturation", 1f);
            }
        }
        
    }
}
