using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Data/Deck")]
public class Deck : ScriptableObject
{

    public string deckName;
    public List<GameObject> deckCards = new List<GameObject>();

}
