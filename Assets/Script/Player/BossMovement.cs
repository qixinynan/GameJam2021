using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float speed = 1;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector3 vec = GameController.manager.player.transform.position - transform.position;
        vec.z = 0;
        rb.velocity = Vector3.Normalize(vec) * speed;
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
    }
}
