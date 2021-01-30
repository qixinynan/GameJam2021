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
        if (GameController.manager.player == null || GameController.manager.isGameOver)
        {
            rb.velocity = Vector2.zero;
            UpdateAnim(rb.velocity.x, rb.velocity.y);
            return;
        }
        Vector3 vec = GameController.manager.player.transform.position - transform.position;
        vec.z = 0;
        rb.velocity = Vector3.Normalize(vec) * speed;
        // transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        UpdateAnim(rb.velocity.x, rb.velocity.y);
    }
    
    public void UpdateAnim(float xMove, float yMove)
    {
        if (Mathf.Approximately(xMove, 0) && Mathf.Approximately(yMove, 0))
        {
            GetComponent<Animator>().SetInteger("dir", 0);
            return;
        }
        if (Mathf.Abs(yMove) > Mathf.Abs(xMove))
        {
            GetComponent<Animator>().SetInteger("dir", yMove > 0 ? 1 : 2);
        }
        else
        {
            GetComponent<Animator>().SetInteger("dir", 3);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (xMove > 0 ? 1.0f: -1.0f),
                transform.localScale.y
                , transform.localScale.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(Util.TagCollection.player))
        {
            GameController.manager.player.GetComponent<Animator>().SetInteger("dir", -1);
            GameController.manager.player.GetComponent<Animator>().SetTrigger("sleep");
            GameController.manager.player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            GameController.manager.isGameOver = true;
            Debug.LogError("GameOver");
        }
    }
}
