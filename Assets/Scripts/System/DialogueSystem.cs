using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public void stringFormat()
    {
        string test = "var {0}";
        string.Format(test, 0);
    }
}
