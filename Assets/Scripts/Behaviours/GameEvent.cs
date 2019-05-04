using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Event", menuName = "Events")]
public class GameEvent : ScriptableObject
{
    //Standard Events
    private UnityEvent standard = new UnityEvent();
    private CardEvent cardEvent = new CardEvent();

    public void Register(UnityAction act)
    {
        standard.AddListener(act);
    }

    public void RegisterOnCard(UnityAction<Card> act)
    {
        cardEvent.AddListener(act);
    }

    public void UnRegister(UnityAction act)
    {
        standard.RemoveListener(act);
    }

    public void UnRegisterOnCard(UnityAction<Card> act)
    {
        cardEvent.RemoveListener(act);
    }

    public void Raise()
    {
        standard.Invoke();
    }

    public void RaiseCard(Card card)
    {
        cardEvent.Invoke(card);
    }

    //Gathering

    public int watchers;

    public void watch(UnityAction act)
    {
        standard.AddListener(act);
    }

    public void Open()
    {
        watchers += 1;
    }

    public void Close()
    {
        watchers -= 1;
        if(watchers < 0)
        {
            watchers = 0;
            Debug.Log("overclosed event");
        }
        if(watchers == 0)
        {
            standard.Invoke();
        }
    }

    public bool IsClosed()
    {
        if(watchers <= 0)
        {
            return true;
        }
        return false;
    }
}

[System.Serializable]
public class CardEvent: UnityEvent<Card> { }

[System.Serializable]
public class DamageEvent : UnityEvent<Damage> { }

