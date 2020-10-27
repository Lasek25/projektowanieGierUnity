using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCubes : MonoBehaviour
{
    public GameObject cubePrefab;
    
    void Start()
    {
        for(var i = 0; i < 10; i++)
        {
            Vector3 position = new Vector3(Random.Range(-4.5f, 4.5f), 0.5f, Random.Range(-4.5f, 4.5f));
            Instantiate(cubePrefab, position, Quaternion.identity);
        }
    }

}
