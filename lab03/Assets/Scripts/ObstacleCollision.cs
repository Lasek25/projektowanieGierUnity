using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtacleCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            print("Uderzyłeś w przeszkodę!");
        }
    }
}
