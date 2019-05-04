using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pile
{

    public Pile(Transform target)
    {
        pilePos = target;
    }

    public Transform pilePos;
    public List<GameObject> list = new List<GameObject>();

    public int Count { get { return list.Count; } }

    public void Add(GameObject obj, bool active)
    {
        list.Add(obj);
        obj.SetActive(active);
        obj.GetComponent<CardMovement>().GoTo(pilePos, active);
    }

    public void Transfer(GameObject obj, Pile oldPile, bool active, bool setPosition)
    {
        oldPile.list.Remove(obj);
        Add(obj, active);
        if (setPosition)
        {
            obj.transform.position = pilePos.position;
        }
    }

    public void TransferAll(Pile oldPile, bool active, bool setPosition)
    {
        foreach (var obj in oldPile.list)
        {
            Add(obj, active);
            if (setPosition)
            {
                obj.transform.position = pilePos.position;
            }
        }
        oldPile.list.Clear();
    }

    public void Shuffle()
    {
        list.Shuffle();
    }

    public GameObject GetNext()
    {
        if (list.Count > 0)
        {
            return list[0];
        }
        return null;
    }
}
