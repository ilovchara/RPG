using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    //控制剑的方向
    Vector3 position;
    //攻击力
    private int attackPower;
    //力度
    public int knockbackForce;


    // Start is called before the first frame update
    void Start()
    {
        //位置基准是以父物体的
        position = transform.localPosition;
    }

    void IsFacingRight(bool isFacingRight)
    {
        if (isFacingRight)
        {
            transform.localPosition = position;
        }
        else
        {
            //单单翻转x
            transform.localPosition = new Vector3(-position.x,position.y,position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if(damageable != null )
        {
            Vector3 _position = transform.parent.position;
            Vector2 direction = collider.transform.position - _position;

            attackPower = Random.Range(1, 10);
            bool isCritical = Random.Range(0, 100) < 30;
            if(isCritical)
            {
                attackPower *= 2;
            }
            damageable.OnHit(attackPower, direction.normalized * knockbackForce);
            //
            DamagePopup.Create(collider.transform.position, attackPower,isCritical);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
