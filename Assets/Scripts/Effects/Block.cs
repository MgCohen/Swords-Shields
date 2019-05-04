using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : Card
{
    [Header("Specific")]
    public int blockedAttacks;
    public int turnDuration;

    public override void Play()
    {
        base.Play();
        selection.Select(1, Player.instance.currentCell.boardPosition);
    }

    public override void Trigger()
    {
        base.Trigger();
        var dir = Player.instance.currentCell.boardPosition - (target as Cell).boardPosition;
        StatusEffect.Apply(Player.instance, new BlockEffect(blockedAttacks, dir, turnDuration, true));
    }
    //choose a direction
    //block x attacks from that direction
}
