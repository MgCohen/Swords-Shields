using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : ScriptableObject
{
    public virtual bool Check(Enemy source)
    {
        return true;
    }

    public EnemyTarget targetting;

    public List<EnemyAction> actions;
}
