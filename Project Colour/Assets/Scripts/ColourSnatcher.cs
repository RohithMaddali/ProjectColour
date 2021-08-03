using AJ;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourSnatcher : MonoBehaviour
{
    public ChangeColour cc;
    private Color previousColor;
    [SerializeField] private Renderer snatcher;
    public bool canSnatchColour;
    // Start is called before the first frame update
    void Start()
    {
        cc = FindObjectOfType<ChangeColour>();
        snatcher = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canSnatchColour)
        {
            previousColor = cc.thisRenderer.material.color;
            if (snatcher.material.color != Color.green && snatcher.material.color != Color.blue && snatcher.material.color != Color.red)
            {
                cc.thisRenderer.material.color = Color.grey;
                cc.previousColor = Color.grey;
                cc.hasColour = false;
                snatcher.material.color = previousColor;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            canSnatchColour = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canSnatchColour = false;
        }
    }
}
