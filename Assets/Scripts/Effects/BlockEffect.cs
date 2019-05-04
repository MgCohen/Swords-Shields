using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockEffect : StatusEffect, ICombatEffect
{
    public BlockEffect(int attacks, Vector2Int dir, int mduration, bool over)
    {
        NumberOfAttacks = attacks;
        direction = dir;
        duration = mduration;
        overTurn = over;
    }

    public int NumberOfAttacks;
    public Vector2Int direction;

    public Damage CheckDamage(Damage dmg, Entity currentChar)
    {
        if (currentChar == dmg.target && NumberOfAttacks > 0)
        {
            var dir = dmg.target.currentCell.boardPosition - dmg.source.currentCell.boardPosition;
            dir = SetDirection(dir);
            direction = SetDirection(direction);
            if (dir == direction)
            {
                Debug.Log("negated");
                dmg.negated = true;
                NumberOfAttacks -= 1;
            }
        }
        return dmg;
    }

    public override void OnApply(ITarget target)
    {
        base.OnApply(target);
        //spawn shield icon
    }

    public override void OnRemove(ITarget target)
    {
        base.OnRemove(target);
        //remove shield icon
    }

    public override void Reapply(ITarget target, StatusEffect status)
    {

        if((status as BlockEffect).direction == direction)
        {
            NumberOfAttacks += (status as BlockEffect).NumberOfAttacks;
            duration += status.duration;
        }
        else
        {
            base.Reapply(target, status);
        }
    }

    public override void CheckExpire()
    {
        base.CheckExpire();
        if(NumberOfAttacks <= 0)
        {
            Remove(attachedTo.GetComponent<ITarget>(), this);
        }

    }

    public Vector2Int SetDirection(Vector2Int dir)
    {
        if (Mathf.Abs(dir.y) > Mathf.Abs(dir.x))
        {
            if (dir.y > 0)
            {
                dir = new Vector2Int(0, 1);
            }
            else
            {
                dir = new Vector2Int(0, -1);
            }
        }
        else if (Mathf.Abs(dir.y) < Mathf.Abs(dir.x))
        {
            if (dir.x > 0)
            {
                dir = new Vector2Int(1, 0);
            }
            else
            {
                dir = new Vector2Int(-1, 0);
            }
        }
        else
        {
            dir = new Vector2Int(0, 0);
        }
        return dir;
    }
}
