using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camController : MonoBehaviour
{
    public GameObject cam;

    void Update()
    {
        cam.transform.position = transform.position;
        cam.transform.rotation = transform.rotation;
    }
}
