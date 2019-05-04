using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSystem
{

    public static void Damage(Entity Source, Entity Target, int Amount)
    {
        Damage dmg = new Damage(Source, Target, Amount);
        foreach(var stat in Source.status)
        {
            if(stat is ICombatEffect)
            {
                dmg = (stat as ICombatEffect).CheckDamage(dmg, Source);
            }
        }
        foreach(ICombatEffect stat in Target.status)
        {
            if (stat is ICombatEffect)
            {
                dmg = (stat as ICombatEffect).CheckDamage(dmg, Target);
            }
        }
        if (dmg.negated)
        {
            //do something
        }
        else
        {
            Target.ReceivedDmg(dmg.amount);
            PopUpSystem.ShowLocalText(Target.transform.position, Amount.ToString(), Color.red, true);
        }
    }
}

public class Damage
{
    public Damage(Entity msource, Entity mtarget, int mamount, bool mnegated = false)
    {
        source = msource;
        target = mtarget;
        amount = mamount;
        negated = mnegated;
    }
    public Entity source;
    public Entity target;
    public int amount;
    public bool negated;
}
