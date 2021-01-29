using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
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
            /*RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log(hit.transform.name);
                if (hit.transform.CompareTag(Util.TagCollection.stopPos))
                {
                    Debug.Log("click : " + hit.transform.name);
                    return;
                }
            }*/
            

            clickPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            clickPos.z = GameController.manager.player.transform.position.z;
            GameController.manager.player.GetComponent<NavMeshAgent2D>().destination = clickPos;
            //player.GetComponent<Rigidbody2D>().velocity = Vector3.Normalize(clickPos - player.transform.position) * speed;
            //isMoving = true;
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);

        // if (Vector3.Distance(player.transform.position, clickPos) <= 0.1f && isMoving)
        // {
        //     player.transform.position = clickPos;
        //     player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        //     isMoving = false;
        // }
    }
}
