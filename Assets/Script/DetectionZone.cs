using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionZone : MonoBehaviour
{

    public Collider2D detectedObjs;
    public float viewRadius; //�뾶
    public LayerMask playerLayerMask; //ɨ���layer


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //λ�� �뾶 Ŀ�� - ����������ص��� Collider2D ��Բ�ص�����ײ����
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
