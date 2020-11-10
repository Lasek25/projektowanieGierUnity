using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour
{
    private float moveSpeed = 2f;
    private float distance = 1.75f;
    private float endPosition;
    private float startPosition;
    private bool isOpening = false;
    private bool isClosing = false;
    private GameObject door;

    void Start()
    {
        door = GameObject.FindGameObjectWithTag("Door");
        startPosition = door.transform.position.x;
        endPosition = door.transform.position.x + distance;
    }

    void Update()
    {
        if (isOpening && door.transform.position.x <= endPosition || isClosing && door.transform.position.x >= startPosition)
        {
            Vector3 move = door.transform.right * moveSpeed * Time.deltaTime;
            door.transform.Translate(move);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player otworzył drzwi.");
            isOpening = true;
            isClosing = false;
            moveSpeed = Mathf.Abs(moveSpeed);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player zamknął drzwi.");
            isOpening = false;
            isClosing = true;
            moveSpeed = -moveSpeed;
        }
    }
}
