using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{

    public int Def;

    public int Atk;

    public int move;

    public int priority;

    public Entity target;

    public List<EnemyBehaviour> Behaviours = new List<EnemyBehaviour>();

    private bool ready = true;

    public bool done = false;

    public Animator anim;

    public void Activate()
    {
        done = false;
        EnemyBehaviour selected = null;
        foreach(var behaviour in Behaviours)
        {
            if (behaviour.Check(this))
            {
                selected = behaviour;
                break;
            }
        }
        StartCoroutine(Activation(selected));
    }

    public void SetReady(bool state)
    {
        ready = state;
    }

    IEnumerator Activation(EnemyBehaviour behav)
    {
        foreach(var act in behav.actions)
        {
            act.Do(this);
            while (!ready)
            {
               yield return null;
            }
        }
        done = true;
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
        EnemySystem.LoseUnit();
    }
}
