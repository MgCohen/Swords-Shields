using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New team", menuName ="Data/Team")]
public class Team : ScriptableObject
{
        public GameEvent turnBegin;
        public GameEvent turnEnd;
}
