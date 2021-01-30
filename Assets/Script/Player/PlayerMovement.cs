﻿using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

//using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
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
        if (GameController.manager.disableInput)
        {
            return;
        }
        //GameObject player = GameController.manager.isControllBoy ? GameController.manager.boy : GameController.manager.girl;
        PlayerMove(GameController.manager.player);
    }

    void PlayerMove(GameObject player)
    {
        if (player == null)
        {
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

        /*if (!Mathf.Approximately(yMove,0))
        {
            rb.velocity = new Vector2(rb.velocity.x, yMove);
            //isMoving = true;
        }  else
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }

        if (!Mathf.Approximately(xMove,0))
        {
            rb.velocity = new Vector2(xMove, rb.velocity.y);
            //isMoving = true;
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }*/

        rb.velocity = new Vector2(xMove, yMove);
        /*if (Mathf.Approximately(xMove, 0) && Mathf.Approximately(yMove, 0))
        {
            rb.velocity = new Vector2(0, 0);
        }*/

        UpdateAnim(player, xMove, yMove);
    }

    public void UpdateAnim(GameObject player, float xMove, float yMove)
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
