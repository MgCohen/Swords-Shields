using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckSystem : MonoBehaviour
{
    public static Deck currentDeck;

    public static DeckSystem instance;

    public Deck myDeck;

    public Pile deck;
    public Pile discard;
    public Pile hand;

    public GameEvent ready;
    public GameEvent onDiscard;

    //temp
    public Button but;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            return;
        }

        //FIND FIX
        currentDeck = myDeck;
        //FIND FIX

        deck = new Pile(GameObject.FindGameObjectWithTag("Deck").transform);
        discard = new Pile(GameObject.FindGameObjectWithTag("Discard").transform);
        hand = new Pile(GameObject.FindGameObjectWithTag("Hand").transform);
        foreach (var Card in currentDeck.deckCards)
        {
            GameObject newCard = Instantiate(Card);
            deck.Add(newCard, false);
            Card.transform.position = deck.pilePos.position;
        }
        instance.deck.Shuffle();
        Setup();
    }

    public static void Setup()
    {
        instance.StartCoroutine(DrawDelay(3));
        //instance.ready.Close();
    }

    public static bool Draw(int amount = 1)
    {
        instance.but.interactable = false;
        var obj = instance.deck.GetNext();
        if (!obj)
        {
            instance.deck.TransferAll(instance.discard, false, true);
            Reshuffle(instance.deck);
            obj = instance.deck.GetNext();
            if (!obj)
            {
                return false;
            }
        }
        instance.hand.Transfer(obj, instance.deck, true, false);
        instance.StartCoroutine(DrawDelay(amount - 1));
        return true;
    }

    public static void Discard(GameObject card)
    {
        Card mCard = card.GetComponent<Card>();
        instance.onDiscard.RaiseCard(mCard);
        instance.discard.Transfer(card, instance.hand, false, true);
        //temporary
        //if (instance.hand.Count <= 0)
        //{
        //    Draw(3);
        //}
    }

    public static void DropHand()
    {
        foreach (var card in instance.hand.list)
        {
            var Card = card.GetComponent<Card>();
            Card.OnDrop.RaiseCard(Card);
        }
        instance.discard.TransferAll(instance.hand, false, true);
    }

    public static void Reshuffle(Pile targetPile, bool full = false)
    {
        if (!full)
        {
            targetPile.Shuffle();
        }
        else
        {
            instance.deck.TransferAll(instance.hand, false, true);
            instance.deck.TransferAll(instance.discard, false, true);
            instance.deck.Shuffle();
        }
    }

    public static IEnumerator DrawDelay(int value)
    {
        yield return new WaitForSeconds(0.25f);
        if (value > 0)
        {
            Draw(value);
        }
        else
        {
            instance.but.interactable = true;
        }
    }
}
