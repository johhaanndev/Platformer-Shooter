using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSparks : MonoBehaviour
{
    // particle system that follows player game object
    public Transform player;
    void LateUpdate()
    {
        gameObject.transform.position = player.position;
    }
}
