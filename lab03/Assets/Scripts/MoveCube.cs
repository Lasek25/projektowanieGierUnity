using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    char direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        direction = 'r';
        speed = 1;
    }

    void FixedUpdate()
    {
        Transform tmpPosition = GetComponent<Transform>();
        Vector3 newPosition;
        if (direction.Equals('r'))
        {
            newPosition = new Vector3(1, 0, 0);
            if (tmpPosition.position.x >= 10)
                direction = 'l';
        }
        else
        {
            newPosition = new Vector3(-1, 0, 0);
            if (tmpPosition.position.x <= -10)
                direction = 'r';
        }

        newPosition = newPosition.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + newPosition);
    }
}
