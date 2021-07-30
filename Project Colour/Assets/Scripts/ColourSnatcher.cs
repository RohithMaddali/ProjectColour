using AJ;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourSnatcher : MonoBehaviour
{
    public ChangeColour cc;
    private Color previousColor;
    [SerializeField] private Renderer snatcher;
    // Start is called before the first frame update
    void Start()
    {
        cc = FindObjectOfType<ChangeColour>();
        snatcher = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        previousColor = cc.thisRenderer.material.color;
        if(other.gameObject.tag == "Player")
        {
            if(snatcher.material.color != Color.green && snatcher.material.color != Color.blue && snatcher.material.color != Color.red)
            {
                cc.thisRenderer.material.color = Color.grey;
                cc.previousColor = Color.grey;
                cc.hasColour = false;
                snatcher.material.color = previousColor;
            }
        }
    }
    void OnTriggerStay(Collider other)
    {
        previousColor = cc.thisRenderer.material.color;
        if (other.gameObject.tag == "Player")
        {
            if (snatcher.material.color != Color.green && snatcher.material.color != Color.blue && snatcher.material.color != Color.red)
            {
                cc.thisRenderer.material.color = Color.grey;
                cc.hasColour = false;
                snatcher.material.color = previousColor;
            }
        }
    }
}
