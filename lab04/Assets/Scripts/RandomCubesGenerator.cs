using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomCubesGenerator : MonoBehaviour
{
    List<Vector3> positions = new List<Vector3>();
    public float delay = 3.0f;
    int objectCounter = 0;
    // obiekt do generowania
    public GameObject block;
    public int numberOfObjects = 0;
    public List<Material> materialsList;

    void Start()
    {
        Transform tmpPosition = GetComponent<Transform>();
        int startPointX = (int)(tmpPosition.position.x - tmpPosition.localScale.x * 5 + 1);
        int quantityX = (int)(tmpPosition.localScale.x * 10 - 1);
        int startPointZ = (int)(tmpPosition.position.z - tmpPosition.localScale.z * 5 + 1);
        int quantityZ = (int)(tmpPosition.localScale.z * 10 - 1);

        // w momecie uruchomienia generuje 10 kostek w losowych miejscach
        List<int> pozycje_x = new List<int>(Enumerable.Range(startPointX, quantityX).OrderBy(x => Guid.NewGuid()).Take(numberOfObjects));
        List<int> pozycje_z = new List<int>(Enumerable.Range(startPointZ, quantityZ).OrderBy(x => Guid.NewGuid()).Take(numberOfObjects));

        for (int i = 0; i < numberOfObjects; i++)
        {
            this.positions.Add(new Vector3(pozycje_x[i], 5, pozycje_z[i]));
        }
        foreach (Vector3 elem in positions)
        {
            Debug.Log(elem);
        }
        // uruchamiamy coroutine
        StartCoroutine(GenerujObiekt());
    }

    void Update()
    {

    }

    IEnumerator GenerujObiekt()
    {
        Debug.Log("wywołano coroutine");
        foreach (Vector3 pos in positions)
        {
            int random = UnityEngine.Random.Range(0, materialsList.Count);
            GameObject clone = Instantiate(this.block, this.positions.ElementAt(this.objectCounter++), Quaternion.identity);
            clone.GetComponent<MeshRenderer>().material = materialsList[random];
            yield return new WaitForSeconds(this.delay);
        }
        // zatrzymujemy coroutine
        StopCoroutine(GenerujObiekt());
    }
}
