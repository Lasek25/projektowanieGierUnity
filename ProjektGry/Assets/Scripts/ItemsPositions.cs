using System;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class ItemsPositions : MonoBehaviour
{
    [SerializeField] private Tilemap tilemapGround;
    [SerializeField] private Tilemap tilemapObstacles;
    [SerializeField] private List<Item> itemsRandomList;
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private GameObject doorOpenPrefab;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        List<Vector3> doorPositions = new List<Vector3>(){
            new Vector3(-50.5f,7.5f),
            new Vector3(-50.5f, -11f),
            new Vector3(-43.5f, -7f),
            new Vector3(-41.5f, -32f),
            new Vector3(10.5f, -46f),
            new Vector3(25.5f, -42.5f),
            new Vector3(1.5f, -9f),
            new Vector3(-2.5f, 2f),
            new Vector3(-13.5f, 3f),
            new Vector3(27.5f, 0.5f),
        };

        List<Vector3> playerPositions = new List<Vector3>()
        {
            new Vector3(-57f, -14.5f),
            new Vector3(-34f, 5.5f),
            new Vector3(-3f, 12.5f),
            new Vector3(28f, 2.5f),
            new Vector3(2f, -13.5f),
            new Vector3(-3f, -37.5f),
            new Vector3(18f, -34.5f),
            new Vector3(-41f, -28.5f),
            new Vector3(-36f, -8.5f),
            new Vector3(-13f, 1.5f),
        };

        List<Vector3> doors = new List<Vector3>( randomPositions(doorPositions, 5));
        //Object doorOpenPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Door_Open.prefab", typeof(GameObject));
        generateItems(doors, doorOpenPrefab);
        //Object doorPrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Door.prefab", typeof(GameObject));
        generateItems(doors, doorPrefab);

        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = playerPositions[Random.Range(0, playerPositions.Count)];

        foreach (var item in itemsRandomList)
        {
            var tmp = 0;
            while (tmp < item.quantity)
            {
                var rndX = Random.Range(tilemapGround.cellBounds.xMin, tilemapGround.cellBounds.xMax + 1);
                var rndY = Random.Range(tilemapGround.cellBounds.yMin, tilemapGround.cellBounds.yMax + 1);
                Vector3Int rndPosition = new Vector3Int(rndX, rndY, 0);
                if (tilemapGround.HasTile(rndPosition) && !tilemapObstacles.HasTile(rndPosition) && CheckIfEmpty(rndPosition))
                {
                    GameObject newObject = Instantiate(item.prefab, rndPosition, Quaternion.identity);
                    newObject.name = item.prefab.name;
                    tmp++;
                }
            }
        }
    }

    public bool CheckIfEmpty(Vector3 newItemPosition)
    {
        String[] tags = {"Item", "Key2", "Flask", "Player", "Door"};
        foreach (var tagName in tags)
        {
            GameObject[] items = GameObject.FindGameObjectsWithTag(tagName);
            if (tagName.Equals("Player"))
            {
                var x = items[0].transform.position.x;
                var y = items[0].transform.position.y;
                Vector3 player1 = new Vector3(x, y - 0.5f);
                if (player1 == newItemPosition)
                    return false;
            }
            else
            {
                foreach (GameObject item in items)
                {
                    if (tagName.Equals("Door"))
                    {
                        var x = item.transform.position.x;
                        var y = item.transform.position.y;
                        Vector3 door1 = new Vector3(x - 0.5f, y);
                        Vector3 door2 = new Vector3(x + 0.5f, y);
                        if (door1 == newItemPosition || door2 == newItemPosition)
                            return false;
                    }
                    else if (item.transform.position == newItemPosition)
                        return false;
                }
            }
        }
        return true;
    }

    public List<Vector3> randomPositions(List<Vector3> availablePositions, int countPositions)
    {
        for (var i = availablePositions.Count; i > countPositions; i--)
        {
            availablePositions.RemoveAt(Random.Range(0, i));
        }
        return availablePositions;
    }

    public void generateItems(List<Vector3> positions, GameObject item)
    {
        foreach (var pos in positions)
        {
            GameObject newObject = Instantiate(item, pos, Quaternion.identity);
            newObject.name = item.name;
        }
    }
}
