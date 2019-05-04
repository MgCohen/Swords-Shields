using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour, ITarget
{
    public Team team;
    [SerializeField]
    public List<StatusEffect> status = new List<StatusEffect>();
    public SpriteRenderer sprite;
    public Cell mCurrentCell;
    public Cell currentCell
    {
        get
        {
            //if (!mCurrentCell)
            //{
            //    SetPosition(Board.Instance.CellAt(transform.position, true));
            //}
            return mCurrentCell;
        }
        set
        {
            mCurrentCell = value;
        }
    }

    public virtual void Start()
    {
        if (!currentCell) SetPosition(Board.Instance.CellAt(transform.position, true));
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetPosition(Cell posCell)
    {
        if (currentCell)
        {
            currentCell.contained = null;
        }
        Debug.Log(posCell);
        currentCell = posCell;
        posCell.contained = gameObject;
        var pos = sprite.transform.localPosition;
        Debug.Log(pos);
        pos.y = 0.556f + (0.37f * (posCell.size - 1));
        sprite.transform.localPosition = pos;
        sprite.sortingOrder = posCell.size;
        transform.position = posCell.transform.position;
    }

    public virtual void ReceivedDmg(int amount)
    {

    }

    public bool CheckStatus(out StatusEffect oldStatus, StatusEffect mstatus)
    {
        var type = mstatus.GetType();
        oldStatus = null;
        foreach(var stats in status)
        {
            if(type == stats.GetType())
            {
                oldStatus = stats;
                return true;
            }
        }

        return false;
    }

}
