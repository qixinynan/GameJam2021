using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public static PlayerMovement instance;
    private Vector3 clickPos;
    private bool isMoving = false;
    
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
        if (Input.GetMouseButtonDown(0))
        {
            clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos.z = player.transform.position.z;
            player.GetComponent<Rigidbody2D>().velocity = Vector3.Normalize(clickPos - player.transform.position) * speed;
            isMoving = true;
        }

        if (Vector3.Distance(player.transform.position, clickPos) <= 0.1f && isMoving)
        {
            player.transform.position = clickPos;
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            isMoving = false;
        }
    }
}
