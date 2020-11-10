using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    private bool isRunning = false;
    private bool isRunningFromStart = true;
    private bool isRunningFromEnd = false;
    private int i;
    [SerializeField]
    private float elevatorSpeed = 2f;
    [SerializeField]
    private List<Vector3> places = new List<Vector3>();
    private List<int> pos;
    private Vector3 direction;

    void Start()
    {
        places.Insert(0, transform.position);
        pos = new List<int>(){0, 1, 2};
        i = 0;
    }

    void FixedUpdate()
    {
        foreach (var p in pos.ToArray())
        {
            if (Math.Abs(transform.position[p] - places[i][p]) < 0.001)
            {
                pos.Remove(p);
            }

            if (isRunning)
            {
                if (p == 0)
                    direction = transform.right;
                else if (p == 1)
                    direction = transform.up;
                else
                    direction = transform.forward;

                var elevatorSpeedLocal = elevatorSpeed;
                if (transform.position[p] >= places[i][p])
                    elevatorSpeedLocal = -elevatorSpeed;

                Vector3 move = direction * elevatorSpeedLocal * Time.deltaTime;
                transform.Translate(move);
            }
        }

        if (pos.Count == 0)
        {
            pos = new List<int>() { 0, 1, 2 };
            if (isRunningFromStart)
                i++;
            else
                i--;    
            if (i == places.Count && isRunningFromStart)
            {
                isRunningFromEnd = true;
                isRunningFromStart = false;
                i = places.Count - 1;
            }
            else if (i == 0 && isRunningFromEnd)
            {
                isRunningFromEnd = false;
                isRunningFromStart = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player wszedł na windę.");
            other.transform.SetParent(transform);
            isRunning = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player zszedł z windy.");
            isRunning = false;
            other.transform.SetParent(null);
        }
    }
}
