using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : PhasePower
{
    public override void Trigger()
    {
        base.Trigger();
        if(DeckSystem.instance.hand.Count > 0)
        {
            Player.instance.Hp += 1;
            PopUpSystem.ShowLocalText(Player.instance.transform.position, "1", Color.green, true);
        }
    }
}
