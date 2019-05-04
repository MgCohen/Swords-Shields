using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public static Player instance;

    public GameObject LoseScreen;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    public PhasePower power;

    public override void Die()
    {
        base.Die();
        gameObject.SetActive(false);
        MainManager.Lose();
    }

    //deck reference
}
