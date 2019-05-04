using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : Card
{
    [Header("Specific")]
    public int range;
    public int damage;

    public override void Play()
    {
        base.Play();
        selection.Select(range, Player.instance.currentCell.boardPosition);
    }

    public override void Trigger()
    {
        base.Trigger();
        DamageSystem.Damage(Player.instance, target as Character, damage);
    }
}
