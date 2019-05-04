using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySystem : MonoBehaviour
{

    public TurnSystem turnSyst;

    public void Activate()
    {
        StartCoroutine(ActivationSequence());
    }

    IEnumerator ActivationSequence()
    {
        var enemies = FindObjectsOfType<Enemy>();
        enemies.OrderBy(a => a.priority);
        //sort enemies by priority
        foreach (var enm in enemies)
        {
            enm.Activate();
            while (!enm.done)
            {
                yield return null;
            }
        }
        turnSyst.StartPlayerTurn();
    }

    public static void LoseUnit()
    {
        var enemies = FindObjectsOfType<Enemy>();
        if(enemies.Length <= 1)
        {
            MainManager.Win();
        }
    }
}
