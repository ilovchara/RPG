using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private Vector3 moveVector;
    public float disappearTimer;
    public float disappearSpeed;
    private Color textColor;
    private const float DISAPPEAR_TIMER_MAX = 1f;

    private static int sortingOrder;


    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
        textColor = textMesh.color;
    }
    //´´½¨Æ®×Ö
    public static DamagePopup Create(Vector3 position,int damageAmount,bool isCritical)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.Instance.DamagePopop,position,Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount,isCritical);
        return damagePopup;
    }
    //Æ®×ÖÉèÖÃ
    private void Setup(int damageAmount,bool iscriticalHit)
    {
        textMesh.SetText(damageAmount.ToString());
        if (!iscriticalHit)
        {
            textMesh.fontSize = 5;
        }
        else
        {
            textMesh.fontSize = 7;
            textColor = Color.red;            
        }
        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIMER_MAX;

        //Êý×Ö×ª×Ö·û
        moveVector = new Vector3(0.7f,1)*10;
        sortingOrder++;
        textMesh.sortingOrder = sortingOrder; 

    }

    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector*7 * Time.deltaTime;

        if(disappearTimer > DISAPPEAR_TIMER_MAX*0.5f)
        {
            float increaseScaleAmount = 1f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float drcreaseScaleAmount = 1f;
            transform.localScale -= Vector3.one * drcreaseScaleAmount * Time.deltaTime;
        }

       
        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0)
        {
            //½µµÍÍ¸Ã÷¶È
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
            
        }
    }

}
