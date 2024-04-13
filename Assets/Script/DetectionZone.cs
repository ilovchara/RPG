using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{

    public Collider2D detectedObjs;
    public float viewRadius; //半径
    public LayerMask playerLayerMask; //扫描的layer


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //位置 半径 目标 - 这个方法返回的是 Collider2D 与圆重叠的碰撞器。
        Collider2D collider =  Physics2D.OverlapCircle(transform.position, viewRadius, playerLayerMask);

        if(collider != null)
        {
            detectedObjs = collider;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, viewRadius);
    }
}
