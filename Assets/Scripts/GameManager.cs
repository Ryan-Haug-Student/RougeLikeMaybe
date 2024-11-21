
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // BUY MR LYKE COOKIE IN A COUPLE WEEKS

    public int dungeonSize;

    int nextRoom;
    int nextPos;
    int lastRoom;

    Vector3 spawnLoc = new Vector3(0, -1, 0);

    [Header("Room Types")]
    public GameObject Room1;
    public GameObject Room2;
    public GameObject Room3;



    void Start()
    {
        dungeonSize = Random.Range(5, 20);
        nextRoom = Random.Range(1, 4);
        nextPos = Random.Range(1, 4);

        MakeDungeon();
    }

    void MakeDungeon()
    {
        bool movedRoom = false;

        for(int i = 0; i < dungeonSize; i++)
        {
                if (nextPos == 1 && lastRoom != 2)
                {
                    spawnLoc += new Vector3(-24, 0, 0);
                    lastRoom = 1;
                    movedRoom = true;

                    //Debug.Log("moved left");
                }
                else if (nextPos == 2)
                {
                    spawnLoc += new Vector3(0, 0, 24);
                    lastRoom = 0;
                    movedRoom = true;

                    //Debug.Log("moved forward");
                }
                else if (lastRoom != 1)
                {
                    spawnLoc += new Vector3(24, 0, 0);
                    lastRoom = 2;
                    movedRoom = true;

                    //Debug.Log("moved right");
                }
                else
                {
                    i--;
                }

                if (movedRoom)
                {
                    if (nextRoom == 1)
                    {
                        Instantiate(Room1, spawnLoc, Quaternion.identity);
                    }
                    else if (nextRoom == 2)
                    {
                        Instantiate(Room2, spawnLoc, Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(Room3, spawnLoc, Quaternion.identity);
                    }
                }

                movedRoom = false;
                nextRoom = UnityEngine.Random.Range(1, 4);
                nextPos = UnityEngine.Random.Range(1, 4);
        }

    }
}
