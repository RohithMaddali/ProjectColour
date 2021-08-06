using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlTips : MonoBehaviour
{
    GameManager gm;

    public GameObject GPControls;
    public GameObject gpAim;
    public GameObject gpSuck;
    public GameObject gpShoot;
    public GameObject gpJump;
    public GameObject gpCam;
    public GameObject gpMove;


    public GameObject KMControls;
    public GameObject kmAim;
    public GameObject kmSuck;
    public GameObject kmShoot;
    public GameObject kmJump;
    public GameObject kmCam;
    public GameObject kmMove;

    public GameObject powerTips;

    private GameObject previousTip;

    PlayerControls controls;

    public bool jumped;
    public bool aimed;
    public bool sucked;
    public bool shot;
    public bool moved;
    public bool camMoved;
    public bool powersShown;
    public bool delay = true;

    public float displayTime;
    Vector2 move;
    Vector2 rotate;

    private void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.Weapon.performed += ctx => DisplayPowerTips();
        controls.Gameplay.CamChange.performed += ctx => DisplaySuckShootTip();
        controls.Gameplay.Jump.performed += ctx => DisplayAimTip();
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Camera.performed += ctx => rotate = ctx.ReadValue<Vector2>();
    }
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        if (gm.gamepadInUse)
        {
            GamepadInUse();
            StartCoroutine(DelayTipDisplay(gpCam));
        }
        else
        {
            KeyboardMouseInUse();
            StartCoroutine(DelayTipDisplay(kmCam));
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!camMoved && !moved && !delay && rotate.magnitude > 0f)
        {
            camMoved = true;
            DisplayMoveTip();
        }

        if (!moved && camMoved && !delay && move.magnitude > 0)
        {
            moved = true;
            DisplayJumpTip();
        }

    }
    IEnumerator DelayTipDisplay(GameObject tip)
    {
        yield return new WaitForEndOfFrame();
        StartCoroutine(DisplayTip(tip));
        delay = false;
        
    }
    IEnumerator DisplayTip(GameObject tip)
    {
        if(previousTip != false)
        {
            previousTip.SetActive(false);
        }
        tip.SetActive(true);
        previousTip = tip;
        yield return new WaitForSeconds(displayTime);
        tip.SetActive(false);
    }

    public void GamepadInUse()
    {
        KMControls.SetActive(false);
        GPControls.SetActive(true);
    }
    public void KeyboardMouseInUse()
    {
        KMControls.SetActive(true);
        GPControls.SetActive(false);
    }
    public void DisplayMoveTip()
    {
        Debug.Log("Display Move Tip");
        if (gm.gamepadInUse)
        {
            StartCoroutine(DisplayTip(gpMove));
        }
        else if (!gm.gamepadInUse)
        {
            StartCoroutine(DisplayTip(kmMove));
        }
    }
    public void DisplayAimTip()
    {
        Debug.Log("Display Aim Tip");
        if (gm.gamepadInUse && !aimed)
        {
            StartCoroutine(DisplayTip(gpAim));
        }
        else if (!gm.gamepadInUse && !aimed)
        {
            StartCoroutine(DisplayTip(kmAim));
        }
        jumped = true;
    }

    public void DisplaySuckShootTip()
    {
        
        if (!sucked && gm.gamepadInUse)
        {
            StartCoroutine(DisplayTip(gpSuck));
            sucked = true;
        }
        else if (!sucked && !gm.gamepadInUse)
        {
            StartCoroutine(DisplayTip(kmSuck));
            sucked = true;
        }
        aimed = true;
    }
    public void DisplayPowerTips()
    {
        if (shot && sucked && !powersShown)
        {
            StartCoroutine(DisplayTip(powerTips));
            powersShown = true;
        }
        if (sucked && !shot && gm.gamepadInUse)
        {
            StartCoroutine(DisplayTip(gpShoot));
            shot = true;
        }
        else if (sucked && !shot && !gm.gamepadInUse)
        {
            StartCoroutine(DisplayTip(kmShoot));
            shot = true;
        }
        
    }
    public void DisplayJumpTip()
    {
        Debug.Log("Display JUMP Tip");
        if (gm.gamepadInUse)
        {
            StartCoroutine(DisplayTip(gpJump));
        }
        else if (!gm.gamepadInUse)
        {
            StartCoroutine(DisplayTip(kmJump));
        }
    }
}
