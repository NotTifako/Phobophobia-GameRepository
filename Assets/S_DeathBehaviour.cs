using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class S_DeathBehaviour : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject spawnPoint;
    public int playerLevel;

    [SerializeField] int deathLevel;

    private void FixedUpdate()
    {
        if (playerLevel == 2 && deathLevel !=2)
        {
            transform.position = spawnPoint.transform.position;
        }

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Level1")
        {
            deathLevel = 1;
        }
        else if (collision.tag == "Level2")
        {
            deathLevel = 2;
        }
    }
}
