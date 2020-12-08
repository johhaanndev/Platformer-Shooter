using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDieParticles : MonoBehaviour
{
    private PlayerController playerController;
    public Transform player;
    public ParticleSystem particles;


    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    void LateUpdate()
    {
        // when player dies, particles appear at the player current position
        if (playerController.getDead())
        {
            particles.Play();
        }
        gameObject.transform.position = player.position;
    }
}
