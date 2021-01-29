using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPos : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.CompareTag(Util.TagCollection.player))
        {
            PlayerMovement.instance.playerA.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
