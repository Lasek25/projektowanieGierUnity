using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareMoveCube : MonoBehaviour
{
    public float speed;
    Rigidbody rb;
    char direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        direction = 'e';
        speed = 1;
    }

    void FixedUpdate()
    {
        Vector3 newPosition;
        if (direction.Equals('e'))
        {
            newPosition = new Vector3(1, 0, 0);
            if (transform.position.x >= 10)
            {
                direction = 'n';
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
        }
        else if (direction.Equals('n'))
        {
            newPosition = new Vector3(0, 0, 1);
            if (transform.position.z >= 10)
            {
                direction = 'w';
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }  
        }
        else if (direction.Equals('w'))
        {
            newPosition = new Vector3(-1, 0, 0);
            if (transform.position.x <= 0)
            {
                direction = 's';
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
        }
        else
        {
            newPosition = new Vector3(0, 0, -1);
            if (transform.position.z <= 0)
            {
                direction = 'e';
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }

        newPosition = newPosition.normalized * speed * Time.deltaTime;
        rb.MovePosition(transform.position + newPosition);
    }
}
