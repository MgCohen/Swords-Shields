using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler
{

    public static Card SelectedCard;
    [Header("Description")]
    public string cardName;
    [TextArea]
    public string Description;
    public List<CardType> cardTypes = new List<CardType>();

    [Header("Targetting")]
    public Selection selection;
    public Targets targetType;

    [HideInInspector]
    public List<Cell> selectedTargets = new List<Cell>();
    protected List<ITarget> targets = new List<ITarget>();
    protected ITarget target
    {
        get
        {
            return targets[0];
        }

        set
        {
            targets[0] = value;
        }
    }

    [Header("Events")]
    public GameEvent OnPlay;
    public GameEvent OnDiscard;
    public GameEvent OnTrigger;
    public GameEvent OnDrop;

    private PopUpText pop;

    private Animator anim;

    public void OnPointerClick(PointerEventData eventData)
    {
        var clicks = eventData.clickCount;
        if (clicks >= 2)
        {
            pop = PopUpSystem.DescriptionPop(cardName, Description);
            if (SelectedCard != this)
            {
                Select();
            }
        }
        else
        {
            if (SelectedCard == this)
            {
                Deselect();
            }
            else
            {
                if(SelectedCard) SelectedCard.Deselect();
                Select();
            }
        }
    }

    public void Select()
    {
        //anim.SetBool("Selected", true);
        Board.Instance.CellReset();
        Play();
    }

    public void Deselect()
    {
        //anim.SetBool("Selected", false);
        if (pop) pop.Close();
        pop = null;
        SelectedCard = null;
        Board.Instance.CellReset();
        Selection.currentSelection = null;
        selectedTargets.Clear();
    }

    private void OnEnable()
    {
        OnPlay.RegisterOnCard(OnOtherCardPlay);
        OnDiscard.RegisterOnCard(OnCardDiscard);
        OnTrigger.RegisterOnCard(Discard);
        OnDrop.RegisterOnCard(OnCardLeftOver);
        //anim = GetComponent<Animator>();
    }

    private void OnDisable()
    {
        OnPlay.UnRegisterOnCard(OnOtherCardPlay);
        OnDiscard.UnRegisterOnCard(OnOtherCardPlay);
        OnTrigger.UnRegisterOnCard(Discard);
        OnDrop.UnRegisterOnCard(OnCardLeftOver);
    }

    public virtual void Play()
    {
        SelectedCard = this;
        OnPlay.RaiseCard(this);
    }

    public virtual void TargetSelected()
    {
        targets = TargetSystem.GetTargets(selectedTargets, targetType);
        Trigger();
    }

    public virtual void Trigger()
    {
        OnTrigger.RaiseCard(this);
    }

    public virtual void Discard(Card card)
    {
        if (card != this)
        {
            return;
        }
        SelectedCard = null;
        Selection.currentSelection = null;
        selectedTargets.Clear();
        DeckSystem.Discard(gameObject);
    }

    public virtual void OnOtherCardPlay(Card card)
    {
        if (card == this)
        {
            return;
        }
    }

    public virtual void OnCardDiscard(Card card)
    {
        if (card == this)
        {
            return;
        }
    }

    public virtual void OnCardLeftOver(Card card)
    {
        if(card != this)
        {
            return;
        }
    }

    public virtual void Cancel()
    {
        SelectedCard = null;
    }
}

