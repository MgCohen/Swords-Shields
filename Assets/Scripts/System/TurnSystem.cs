using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{


    public GameEvent StartPlayer;
    public GameEvent EndPlayer;
    public GameEvent EndEnemy;
    public EnemySystem enmSyst;


    public void EndTurn()
    {
        //end turn effects
        DeckSystem.instance.but.interactable = false;
        if (Player.instance.power)
        {
            Player.instance.power.Trigger();
        }
        DeckSystem.DropHand();
        StartCoroutine(WaitCO(0.25f));
    }

    public void StartPlayerTurn()
    {
        StartPlayer.Raise();
        //refrsh stats
        DeckSystem.Setup();
    }

    public void StartEnemyTurn()
    {
        enmSyst.Activate();
    }

    IEnumerator WaitCO(float time)
    {
        yield return new WaitForSeconds(time);
        StartEnemyTurn();
    }
}
