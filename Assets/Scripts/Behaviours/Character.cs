using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Entity
{

    public int Hp;
    public int Energy;
    public GameEvent OnDamage;


    public override void Start()
    {
        base.Start();
        OnDamage = Instantiate(OnDamage);
    }

    public virtual void Die()
    {
    }

    public override void ReceivedDmg(int amount)
    {
        OnDamage.Raise();
        Hp -= amount;
        if(Hp <= 0)
        {
            Die();
        }
    }

}
