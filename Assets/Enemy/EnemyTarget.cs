using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTarget : ScriptableObject
{
    public virtual Entity GetTarget(Enemy source)
    {
        return null;
    }
}
