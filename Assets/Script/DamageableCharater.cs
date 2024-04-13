using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableCharater : MonoBehaviour, IDamageable
{
    Rigidbody2D rb;
    Collider2D physicsCollider;
    public int health;

    public int Health
    {
        get { return health; }
        set { health = value;
            if (health <= 0)
            {
                //��С���޷��ƶ�
                gameObject.BroadcastMessage("OnDie");
                Targetable = false;
            }
            else
            {
                gameObject.BroadcastMessage("OnDamage");
            }    
        
        }
    }

    bool targetable;
    public bool Targetable
    {
        get
        {
            return targetable;
        }
        set
        {
            targetable = value;
            if(!targetable)
            {
                rb.simulated = false;
            }
        }
    }



    public void OnHit(int damage,Vector2 knockback)
    {
        //�����۳�
        Health -= damage;
        rb.AddForce(knockback);
    }


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        physicsCollider = GetComponent<Collider2D>();
    }

    public void OnObjectDestroyed()
    {
        Destroy(gameObject);
    }



}
