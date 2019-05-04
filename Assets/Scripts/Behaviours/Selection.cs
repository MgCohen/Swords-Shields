using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selection : ScriptableObject
{

    public static Selection currentSelection;

    public int targetCount;
    public bool targetAll;

    public virtual void Select(int Range, Vector2 pos)
    {
        currentSelection = this;
    }
}
