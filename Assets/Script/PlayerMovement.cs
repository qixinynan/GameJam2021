using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

//using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public GameObject playerA;
    public GameObject playerB;
    public float speed;
    public static PlayerMovement instance;
    public CinemachineVirtualCamera virtualCamera;
    private Vector3 clickPos;
    private bool isMoving = false;
    private bool isControllA = true;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        PlayerControllUpdate();
        if (isControllA)
        {
            PlayerMove(playerA);
        }
        else
        {
             PlayerMove(playerB);
        }

    }

    void PlayerMove(GameObject player)
    {
        Rigidbody2D rig = player.GetComponent<Rigidbody2D>();
        float xMove = 0;
        float yMove = 0;
        
        if (Input.GetKey(KeyCode.W))
        {
            yMove = speed;
        }
        else if(Input.GetKey(KeyCode.S))
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
        if (yMove != 0)
        {
            rig.velocity = new Vector2(rig.velocity.x, yMove);
            isMoving = true;
        }
        if(xMove != 0)
        {
            rig.velocity = new Vector2(xMove,rig.velocity.y);
            isMoving = true;
        }
        if (xMove == 0 && yMove == 0)
        {
            rig.velocity = new Vector2(0,0);
        }
    }

    void PlayerControllUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ControllChange();
        }
    }

    void ControllChange()
    {
        isControllA = !isControllA;
        virtualCamera.Follow = isControllA ? playerA.gameObject.transform : playerB.gameObject.transform;
    }
}
