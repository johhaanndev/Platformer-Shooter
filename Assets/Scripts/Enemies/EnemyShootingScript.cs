using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootingScript : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;
    public ParticleSystem particles;

    void Shoot()
    {
        particles.Play();
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}
