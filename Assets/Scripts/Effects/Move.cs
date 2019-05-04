using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Move : Card
{
    [Header("Specific")]
    public int range;

    public override void Play()
    {
        base.Play();
        selection.Select(range, Player.instance.currentCell.boardPosition);
    }

    public override void Trigger()
    {
        base.Trigger();
        var targetPos = (target as Cell).boardPosition;
        //Player.instance.transform.position = Board.Instance.CellAt(targetPos).transform.position;
        Player.instance.SetPosition(target as Cell);
    }
}
