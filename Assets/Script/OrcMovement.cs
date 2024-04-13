using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;

public class OrcMovement : MonoBehaviour
{
    //��ȡ��ɫ������
    Rigidbody2D rb;
    //�����ȡFlip
    SpriteRenderer spriteRenderer;
    //�������
    DetectionZone detectionZone;
    Animator animator;


    public float speed;
    public float knockbackForce;
    public int attactPower;
 
    private void Awake()
    {
        //��ȡ��ǰ��Ʒ�ı���
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        detectionZone = GetComponent<DetectionZone>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (detectionZone.detectedObjs != null)
        {
            //����λ�� ���λ�� - ����λ��
            Vector2 direction = (detectionZone.detectedObjs.transform.position - transform.position);
            if (direction.magnitude <= detectionZone.viewRadius)
            {
                //�� force ʸ���ķ�������ʩ������
                rb.AddForce(direction.normalized * speed);
                //��������
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
    //��ϣ���ص�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();

        if(damageable != null && collider.tag == "Player") { 
            //orcָ��player������
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
