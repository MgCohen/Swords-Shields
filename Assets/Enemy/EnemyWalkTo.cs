using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Walk", menuName = "Enemy/Action/Walk")]
public class EnemyWalkTo : EnemyAction
{
    public override void Do(Enemy source)
    {
        base.Do(source);
        source.anim.SetTrigger("Act");
        var target = source.target;
        var dist = source.move;
        var path = TargetSystem.SetPath(source.currentCell, target.currentCell, true, true);
        dist = Mathf.Clamp(dist, 0, path.Count - 2);
        while (path[dist].contained && dist > 0)
        {
            dist -= 1;
        }
        source.SetPosition(path[dist]);
    }

}
