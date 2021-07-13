using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    public float speed;
    public float pauseTime;
    public Transform destination;
    public Transform start;
    public Transform end;
    public bool moving;
    // Start is called before the first frame update
    void Start()
    {
        destination = end;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
            if(transform.position == destination.position)
            {
                StartCoroutine(Pause());
            }
        }
    }

    IEnumerator Pause()
    {
        moving = false;
        Debug.Log("SWITCH TURN AROUND NOW HIT IT");
        yield return new WaitForSeconds(pauseTime);
        if (destination == start)
        {
            destination = end;
        }
        else if (destination == end)
        {
            destination = start;
        }

        Debug.Log("Moving towards " + destination);
        moving = true;
    }
}
