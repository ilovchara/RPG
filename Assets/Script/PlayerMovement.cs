using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //��ȡ��ɫ������
    Rigidbody2D rb;
    Vector2 moveInput;
    public float moveSpeed;
    Animator animator;
    //�����ȡFlip
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        //��ȡ
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMove(InputValue value)
    {
        //�����ƶ����߼�
        moveInput = value.Get<Vector2>();
        if(moveInput == Vector2.zero)
        {
            //�����ƶ���������
            animator.SetBool("IsWalk", false);
        }
        else
        {
            animator.SetBool("IsWalk", true);
            if(moveInput.x>0)
            {
                spriteRenderer.flipX = false;
                //������������������ķ���
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
        //�ƶ� F = ma
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
