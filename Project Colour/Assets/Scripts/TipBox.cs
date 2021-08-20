using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipBox : MonoBehaviour
{
    ControlTips tips;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        tips = FindObjectOfType<ControlTips>();
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gm.displayTips)
        {
            tips.DisplayColourStealTip();
        }
    }
}
