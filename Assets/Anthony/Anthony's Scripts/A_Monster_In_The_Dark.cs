using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Monster_In_The_Dark : MonoBehaviour
{
    [SerializeField] private float timer = 10f;
    private float chargespeed = 0f;
    //private Vector2 animatormovement;
    private float randomizedtimer, standintimer = 30f;
    //private Animator anim;
    //private Rigidbody2D rb;
    [SerializeField] private float inputspeed;
    [SerializeField] private GameObject player;
    [SerializeField] private Transform IdleSpawnPos;
    [SerializeField] private Transform RightSpawnPos;
    [SerializeField] private Transform LeftSpawnPos;
    private bool monsteractive = false, controltimer = true, torchlightflashed = false, monsterinlevel = false, monsterspawned = false;

    private SpriteRenderer spr;
    
    //============================================================================================
    void Start()
    {
        SetRandomTimer();
        spr = GetComponent<SpriteRenderer>();
        //anim = GetComponent<Animator>();
        //rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        timer = timer - 0.02f;
        if (timer <= 0f)
        {
            monsteractive = true;
        }
        //---------------Manages Timer according to whether the monster is active---------------//       
        if ( monsteractive == true && controltimer == true)
        {
            SetRandomTimer();
            controltimer = false;
        }
        //---------------Manages Monster Entering Level---------------//
        if (timer > 0f && monsteractive == true)
        {
            transform.position = IdleSpawnPos.position;
            chargespeed = 1f;
        }
        /*else if (timer == 0f && monsteractive == true)
        {
            transform.position = RightSpawnPos.position;           
        }*/
        else if (timer < 0f && monsteractive == true)
        {
            monsterinlevel = true;
            monsterspawned = true;
            TeleportMonster();
            monsterspawned = false;
            SetCooldown();
            NonActiveTimer();
        }
        //---------------Manages Monster Follow and being Flashed---------------//
        if (monsterinlevel == true && torchlightflashed == false)
        {
            chargespeed = inputspeed;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, Time.deltaTime * chargespeed);
            //animatormovement = rb.velocity;

            //anim.SetFloat("X", animatormovement.x);

        }
        else if (monsterinlevel == true && torchlightflashed == true)
        {
            transform.position = IdleSpawnPos.position;
            chargespeed = 0f;
            monsterinlevel = false;
            controltimer = true;
            torchlightflashed = false;
            monsteractive = false;
        }

        //---------------Filps Monster to Face Player---------------//
        if (monsterinlevel && player.transform.position.x < this.transform.position.x)
        {
            spr.flipX = true;
        }

        else if (monsterinlevel && player.transform.position.x > this.transform.position.x)
        {
            spr.flipX = false; 
        }
    }

    void NonActiveTimer()
    {
        timer = standintimer;
    }
    void SetCooldown()
    {
        monsteractive = false;        
    }
    void SetRandomTimer()
    {
        randomizedtimer = Random.Range(5f, 15f);
        timer = randomizedtimer;
    }
    void TeleportMonster()
    {
        int choice = Random.Range(1, 2);
        /*if(monsterspawned == true && choice == 1)
        {*/
            transform.position = RightSpawnPos.position;
        /*}
        else if (monsterspawned == true && choice == 2)
        {
            transform.position = LeftSpawnPos.position;
        }*/

        AudioManager.Instance.PlaySFX("MonsterScream");

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Flashlight")
        {
            torchlightflashed = true;
        }
    }
}
