using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDeath : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject deathmenu;
    [SerializeField] private GameObject gameplayUI;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player.SetActive(false);
            gameplayUI.SetActive(false);
            deathmenu.SetActive(true);
        }
    }
}
