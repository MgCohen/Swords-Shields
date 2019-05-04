using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Chase", menuName = "Enemy/Behaviour/Chase")]
public class EnemyChase : EnemyBehaviour
{
    public override bool Check(Enemy source)
    {
        var target = targetting.GetTarget(source);
        if (target)
        {
            source.target = target;
            return true;
        }
        else
        {
            return false;
        }
    }
}
