using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public CinemachineFreeLook moveCam;
    public Slider mouseSlider;
    public float mouseSenX = 100f;
    public float mouseSenY = 1.5f;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    public void ChangeMouseSensitivity()
    {
        mouseSenX = Mathf.Clamp(mouseSlider.value * 150f, 40, 150);
        mouseSenY = Mathf.Clamp(mouseSlider.value * 3f, .3f, 3);
        if (moveCam != null)
        {
            moveCam.m_XAxis.m_MaxSpeed = mouseSenX;
            moveCam.m_YAxis.m_MaxSpeed = mouseSenY;
        }
        Debug.Log("MOUSEX SENSITIVITY IS " + mouseSenX);
        Debug.Log("MOUSEY SENSITIVITY IS " + mouseSenY);
    }
}
