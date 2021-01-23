using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public float maxHP;
    public bool dead = false;
    public float speed;

    public float jumpForce;
    public float fallForce;


    public float HP {
        get {
            return _hp;
        }
        set {
            _hp = Mathf.Clamp(value, 0, maxHP);
            if (_hp <= 0) {
                Die();
            }
        }
    }
    private float _hp;

    private int attackCount = 0;
    //private AnimatorStateInfo currentState;
    //private Animator anim;
    private Rigidbody2D rb;

    private float moveInput;
    public bool isGrounded;
    private bool isJump = false;
    private bool isDash = false;

    public bool jumpPressed = false;

    public bool isHurt = false;
    public int dir = 1;


    void Start() {
        HP = maxHP;
        //anim = GetComponent<Animator>();
        //currentState = anim.GetCurrentAnimatorStateInfo(0);
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    
    void FixedUpdate() {
        if (GameController.manager.disableInput)
        {
            return;
        }

        UpdateGroundState();
        Move();
        Jump();
    }

    void Update() {
        if (GameController.manager.disableInput)
        {
            return;
        }
    }


  
    private void UpdateGroundState()
    {
        isGrounded = true;
    }

    private void Move() {
        if (isHurt || GetComponent<Player>().attackCount > 0)
            return;

        moveInput = Input.GetAxisRaw("Horizontal");

        if (moveInput > 0) {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
            dir = 1;
        } else if (moveInput < 0) {
            transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            dir = -1;
        }

        if (!Mathf.Approximately(moveInput, 0)) {
            //anim.SetBool("Walk", true);


        } else {
            //anim.SetBool("Walk", false);
        }

        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

    }

    private void Jump() {
        //anim.SetBool("IsGround", isGrounded);
        if (isGrounded) {
            if (jumpPressed) {
                //anim.SetTrigger("Jump");
                isJump = true;
                rb.velocity = Vector2.up * jumpForce;
            }
        } else if (rb.velocity.y < 0 || !jumpPressed) {
            rb.velocity += Vector2.down * fallForce;
        }
    }
    
    void Die() {
        dead = true;
        //anim.SetBool("Dead", true);
    }
    

    void DestroySelf() {
        Destroy(gameObject);
    }
   
}
