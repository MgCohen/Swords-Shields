using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Targetting Player", menuName = "Enemy/target/Player")]
public class EnemyTargetPlayer : EnemyTarget
{
    public override Entity GetTarget(Enemy source)
    {
        return Player.instance;
    }
}
