using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotGunController : MonoBehaviour
{

    public GameObject HitMarker;
    public float fireDelay = .5f;
    Transform BarrelEnd;
    RaycastHit hit;

    bool canShoot = true;

    void Start()
    {
        BarrelEnd = GameObject.Find("BarrelLoc").transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canShoot)
        {
            Physics.Raycast(BarrelEnd.position, BarrelEnd.forward, out hit, 100f);

            if(hit.collider != null)
            {
                Instantiate(HitMarker, hit.point, Quaternion.identity);
            }

            canShoot = false;
            Invoke(nameof(ResetShoot), fireDelay);
        }
    }

    void ResetShoot()
    {
        canShoot = true;
    }
}
