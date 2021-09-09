using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    // Create road object
    public GameObject roadPrefab;
    // Width between blocks
    public float offset = 0.707f;
    public Vector3 lastPos;

    private int roadCount = 0;

    // every half second, new road part will be created
    public void StartBuilding()
    {
        InvokeRepeating("CreateNewRoadPart", 1f, 0.5f);
    }

    // Create new roads every time
    public void CreateNewRoadPart()
    {
        Debug.Log("Create new road part");

        Vector3 spawnPos = Vector3.zero;

        // Randomly create position of new block
        float chance = Random.Range(0, 100);
        if (chance < 50)
        {
            spawnPos = new Vector3(lastPos.x + offset, lastPos.y, lastPos.z + offset);
        }
        else
            spawnPos = new Vector3(lastPos.x - offset, lastPos.y, lastPos.z + offset);

        // Instantiate new block and rotate it in the correct position
        GameObject g = Instantiate(roadPrefab, spawnPos, Quaternion.Euler(0, 45, 0));

        lastPos = g.transform.position;

        roadCount++;
        // Activate crystals every 5 roads
        if (roadCount % 5 == 0)
        {
            g.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    
    // Function to create a new block every time user hit "space" to turn
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            CreateNewRoadPart();
    }
}
