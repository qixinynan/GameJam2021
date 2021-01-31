using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

//using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 2.5f;
    public static PlayerMovement instance;
    //private bool isMoving = false;
    //private bool isControllA = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (GameController.manager.disableInput ||  GameController.manager.isGameOver)
        {
            return;
        }
        //GameObject player = GameController.manager.isControllBoy ? GameController.manager.boy : GameController.manager.girl;
        PlayerMove(GameController.manager.player);
    }

    void PlayerMove(GameObject player)
    {
        if (player == null){
        
            //Debug.LogError("Player is Null");
            return;
        }
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        float xMove = 0;
        float yMove = 0;

        if (Input.GetKey(KeyCode.W))
        {
            yMove = speed;
            
        }
        else if (Input.GetKey(KeyCode.S))
        {
            yMove = -speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            xMove = -speed;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            xMove = speed;
        }
        if(xMove!=0 || yMove!=0) 
            PlayWalkingSound(player);
        else
        {
            player.GetComponent<AudioSource>().Stop();
        }
        
        
        rb.velocity = new Vector2(xMove, yMove);
        UpdateMove(player, xMove, yMove);
    }

    public void PlayWalkingSound(GameObject player)
    {
        AudioSource audioSource = player.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            //Debug.LogError("AudioSource is null");
        }
        else if (audioSource.clip == null)
        {
            //Debug.LogWarning("Sound is null now");
        }
        else
        {
            
            if (audioSource.isPlaying)
            {
                
            }
            else
            {
                audioSource.Play();
            }
            
        }
    }
    public void UpdateMove(GameObject player, float xMove, float yMove)
    {
        if (Mathf.Approximately(xMove, 0) && Mathf.Approximately(yMove, 0))
        {
            player.GetComponent<Animator>().SetInteger("dir", 0);
            return;
        }
        if (Mathf.Abs(yMove) > Mathf.Abs(xMove))
        {
            player.GetComponent<Animator>().SetInteger("dir", yMove > 0 ? 1 : 2);
        }
        else
        {
            player.GetComponent<Animator>().SetInteger("dir", 3);
            player.transform.localScale = new Vector3(Mathf.Abs(player.transform.localScale.x) * (xMove > 0 ? -1.0f: 1.0f),
                player.transform.localScale.y
                , player.transform.localScale.z);
        }
    }
}
