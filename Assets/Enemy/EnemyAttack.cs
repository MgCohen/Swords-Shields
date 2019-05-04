using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Attack", menuName = "Enemy/Action/Attack")]
public class EnemyAttack : EnemyAction
{

    public int range;
    public bool IgnoreWalls;

    public override void Do(Enemy source)
    {
        base.Do(source);
        if(Board.Instance.GetDistance(source.currentCell, source.target.currentCell) <= range)
        {
            //implement targetting
            source.anim.SetTrigger("Act");
            DamageSystem.Damage(source, source.target, source.Atk);
        }
        else
        {
            source.SetReady(true);
        }
    }
}
