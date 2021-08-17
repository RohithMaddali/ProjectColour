﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTowards : MonoBehaviour
{
    public GameObject myTarget;
    public float speed = 5.0f;
    public Color color;

    public void FlyTowardsTarget(GameObject target)
    {
        if (target == null)
            target = myTarget;

        Vector3 myPos = transform.position;
        Vector3 tarPos = target.transform.position;

        float step = Time.deltaTime;

        if (myPos != tarPos)
        {
            myPos = Vector3.MoveTowards(myPos, tarPos, step);
        }
    }

    void Update()
    {
        TrailRenderer trailRenderer = GetComponent<TrailRenderer>();
        ParticleSystem particleSystem = GetComponentInChildren<ParticleSystem>();

        trailRenderer.endColor = color;
        trailRenderer.startColor = color;
        particleSystem.startColor = color;

        GameObject target = myTarget;

        Vector3 myPos = this.transform.position;
        Vector3 tarPos = target.transform.position;

        float step = speed * Time.deltaTime;

        //if (transform != tarPos)
        //{
            transform.position = Vector3.MoveTowards(transform.position, tarPos, step);
        //}
    }
}
