using System.Collections.Generic;
using UnityEngine;

public class Foods : MonoBehaviour
{
    public List<GameObject> foodslist = new List<GameObject>();
    Vector3 spawnPosition;

    public GameObject SpawnFoods()
    {
        // Setting the spawn area parameters.
        float minX = -7.5f;
        float maxX = 7.5f;
        float minY = -3.7f;
        float maxY = 3.7f;

        int randomX = (int)Random.Range(minX, maxX);
        int randomY = (int)Random.Range(minY, maxY);

        spawnPosition = new Vector3(randomX, randomY, 0);

        GameObject[] blocks = GameObject.FindGameObjectsWithTag("block");

        // Preventing foods from spawning ontop of blocks 
        // by returning null if there is a block already on the spawnPosition
        // and then recalling this SpawnFoods method.
        for (int i = 0; i < blocks.Length; i++)
        {
            if (blocks[i].transform.position == spawnPosition)
            {
                SpawnFoods();
                return null;
            }
        }
        return Instantiate(foodslist[Random.Range(0, 8)], spawnPosition, Quaternion.identity);
    }
    // Makes sure there is a food spawned a the start of the game.
    private void Awake()
    {
        SpawnFoods();
    }

}
