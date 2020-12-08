using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    public GameObject parabolicShot;

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            Shoot(bullet);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Shoot(parabolicShot);
        }
        
    }

    void Shoot(GameObject bulletType)
    {
        Instantiate(bulletType, firePoint.position, firePoint.rotation);
    }
}
