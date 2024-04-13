using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//接口才能被继承
public interface IDamageable
{
    //挂上这个脚本就是攻击？
    public void OnHit(int damage, Vector2 knockback);
    //伤害

   
    

    
}
