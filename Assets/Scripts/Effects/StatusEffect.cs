using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusEffect
{
    public int duration;

    public GameObject attachedTo;

    public bool overTurn;

    public static void Apply(ITarget target, StatusEffect status, bool ignoreDoubles = false)
    {
        StatusEffect oldStatus = null;
        if (!(target as Entity).CheckStatus(out oldStatus, status) || ignoreDoubles)
        {
            if (target is Entity)
            {
                (target as Entity).team.turnBegin.Register(status.BeginTick);
                (target as Entity).team.turnEnd.Register(status.EndTick);
                (target as Entity).team.turnEnd.Register(status.CheckExpire);
                (target as Entity).status.Add(status);
            }
            else
            {
                //cell implementation
            }
            status.OnApply(target);
            status.attachedTo = (target as MonoBehaviour).gameObject;
        }
        else
        {
            oldStatus.Reapply(target, status);
        }
    }

    public static void Remove(ITarget target, StatusEffect status)
    {
        if (target is Entity)
        {
            (target as Entity).team.turnBegin.UnRegister(status.BeginTick);
            (target as Entity).team.turnEnd.UnRegister(status.EndTick);
            (target as Entity).team.turnEnd.UnRegister(status.CheckExpire);
            (target as Entity).status.Remove(status);
            status.OnRemove(target);
        }
        else
        {
            //cell implemetation
        }
    }

    public virtual void OnApply(ITarget target)
    {

    }

    public virtual void Reapply(ITarget target, StatusEffect status)
    {
        Apply(target, status, true);
    }

    public virtual void OnRemove(ITarget target)
    {

    }

    public virtual void Check()
    {

    }

    public virtual void BeginTick()
    {
        if (overTurn)
        {
            duration -= 1;
            if (duration == 0)
            {
                Remove(attachedTo.GetComponent<ITarget>(), this);
            }
        }
    }

    public virtual void EndTick()
    {
        if (!overTurn)
        {
            duration -= 1;
            if (duration == 0)
            {
                Remove(attachedTo.GetComponent<ITarget>(), this);
            }
        }
    }

    public virtual void CheckExpire()
    {
        if (duration == 0)
        {
            Remove(attachedTo.GetComponent<ITarget>(), this);
        }
    }
}

public interface ICombatEffect
{
    Damage CheckDamage(Damage dmg, Entity currentCheck);
}

