using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalElevator : MonoBehaviour
{
    [SerializeField]
    private float elevatorSpeed = 2f;
    private bool isRunning = false;
    [SerializeField]
    private float distance = 18f;
    private bool isRunningFromStart = true;
    private bool isRunningFromEnd = false;
    private float endPosition;
    private float startPosition;

    void Start()
    {
        startPosition = transform.position.z;
        endPosition = transform.position.z + distance;
    }

    void Update()
    {
        if (isRunningFromStart && transform.position.z >= endPosition)
        {
            isRunningFromEnd = true;
            isRunningFromStart = false;
            elevatorSpeed = -elevatorSpeed;
        }
        else if (isRunningFromEnd && transform.position.z <= startPosition)
        {
            isRunningFromEnd = false;
            isRunningFromStart = true;
            elevatorSpeed = Mathf.Abs(elevatorSpeed);
        }

        if (isRunning)
        {
            Vector3 move = transform.forward * elevatorSpeed * Time.deltaTime;
            transform.Translate(move);
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