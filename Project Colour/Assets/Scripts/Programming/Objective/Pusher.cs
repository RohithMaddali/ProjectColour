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
    public Renderer pusherMat;
    public bool moving;
    bool playOnce;
    W_Pusher_Audio pusherSound;
    // Start is called before the first frame update
    void Start()
    {
        destination = end;
        pusherSound = GetComponent<W_Pusher_Audio>();
        pusherMat = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (moving && pusherMat.material.color != Color.green)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
            if (!playOnce)
            {
                pusherSound.PusherSound();
                playOnce = true;
            }
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
        playOnce = false;
    }
}
