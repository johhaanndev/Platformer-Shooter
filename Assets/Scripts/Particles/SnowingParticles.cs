using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowingParticles : MonoBehaviour
{
    // snowing effect followers player position. 
    // This way we minimize the particle quantity over the game
    public Transform player;
    void LateUpdate()
    {
        gameObject.transform.position = new Vector3(player.position.x, 8.5f, player.position.z);
    }
}
