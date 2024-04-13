using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //获取角色的物理
    Rigidbody2D rb;
    Vector2 moveInput;
    public float moveSpeed;
    Animator animator;
    //这里获取Flip
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        //获取
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMove(InputValue value)
    {
        //控制移动的逻辑
        moveInput = value.Get<Vector2>();
        if(moveInput == Vector2.zero)
        {
            //控制移动动画播放
            animator.SetBool("IsWalk", false);
        }
        else
        {
            animator.SetBool("IsWalk", true);
            if(moveInput.x>0)
            {
                spriteRenderer.flipX = false;
                //调用这个物体的子物体的方法
                gameObject.BroadcastMessage("IsFacingRight", true);

            }
            if(moveInput.x<0)
            {
                spriteRenderer.flipX= true;
                gameObject.BroadcastMessage("IsFacingRight", false);
            }
        }

    }


    private void FixedUpdate()
    {
        //移动 F = ma
        rb.AddForce(moveInput * moveSpeed);
    }

    void OnFire()
    {
        animator.SetTrigger("swordAttack");
    }

    void OnDamage()
    {
        animator.SetTrigger("isDamage");
    }

    void OnDie()
    {
        animator.SetTrigger("isDead");
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }

}
