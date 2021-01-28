using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouch : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("交互钟表");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("取消交互钟表");
    }
}
