using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;

public class OrcMovement : MonoBehaviour
{
    //获取角色的物理
    Rigidbody2D rb;
    //这里获取Flip
    SpriteRenderer spriteRenderer;
    //攻击组件
    DetectionZone detectionZone;
    Animator animator;


    public float speed;
    public float knockbackForce;
    public int attactPower;
 
    private void Awake()
    {
        //获取当前物品的变量
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        detectionZone = GetComponent<DetectionZone>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (detectionZone.detectedObjs != null)
        {
            //计算位置 玩家位置 - 怪物位置
            Vector2 direction = (detectionZone.detectedObjs.transform.position - transform.position);
            if (direction.magnitude <= detectionZone.viewRadius)
            {
                //沿 force 矢量的方向连续施加力。
                rb.AddForce(direction.normalized * speed);
                //矫正方向
                if(direction.x > 0)
                {
                    spriteRenderer.flipX = false;
                }
                if(direction.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
                OnWalk();
            }
            else
            {
                OnWalkStop();
            }
        }
        
    }
    //不希望重叠
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();

        if(damageable != null && collider.tag == "Player") { 
            //orc指向player的向量
            Vector2 direction = collider.transform.position - transform.position;
            Vector2 force = direction.normalized * knockbackForce;

            damageable.OnHit(attactPower, force);
        }
    }


    public void OnWalk()
    {
        animator.SetBool("isWalk", true);
    }

    public void OnWalkStop()
    {
        animator.SetBool("isWalk", false);
    }

    void OnDamage()
    {
        animator.SetTrigger("isDamage");
    }

    void OnDie()
    {
        animator.SetTrigger("isDead");
    }

}
