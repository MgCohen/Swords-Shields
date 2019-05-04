using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : ScriptableObject
{

    public virtual void Do(Enemy source)
    {
        source.SetReady(false);
    }
}
