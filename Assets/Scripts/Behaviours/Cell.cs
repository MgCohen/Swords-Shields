using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Cell : MonoBehaviour, IPointerClickHandler, ITarget
{
    [Header("References")]
    public SpriteRenderer sprite;
    public PolygonCollider2D ClickZone;
    private Board board;
    public SortingGroup sort;
    public GameObject contained;


    [Header("Tile Status")]
    private bool selectable = false;
    public int size = 1;
    public int stepValue;
    public int stepCost;
    public Vector2Int boardPosition = new Vector2Int();
    public bool Checked = false;
    public float heigthOffset;
    private List<GameObject> cellTower = new List<GameObject>();
    public List<StatusEffect> status { get; set; }

    //PATHFINDING
    [HideInInspector]
    public Cell parentCell;
    [HideInInspector]
    public int stepNeeded;

    void Start()
    {
        Vector3 pos = sprite.transform.position;
        cellTower.Add(sprite.gameObject);
        SetSize();
    }

    private void OnEnable()
    {
        board = FindObjectOfType<Board>();
        board.cells.Add(this);
    }

    public void SetSize()
    {

        //sprite.size = new Vector2(1, size);
        if (size < cellTower.Count)
        {
            while (size < cellTower.Count)
            {
                var c = cellTower[cellTower.Count - 1];
                Destroy(c.gameObject);
                cellTower.RemoveAt(cellTower.Count - 1);
                size = cellTower.Count;
            }
        }
        else
        {
            while (size > cellTower.Count)
            {
                GameObject newImage = Instantiate(sprite.gameObject, transform);
                SpriteRenderer newSprite = newImage.GetComponent<SpriteRenderer>();
                newSprite.size = new Vector2(newSprite.size.x, 0.85f);
                //newCell.transform.parent = transform;
                cellTower.Add(newImage);
                size = cellTower.Count;
                newSprite.sortingOrder = cellTower.Count - 1;
                var pos = newImage.transform.localPosition;
                //pos.y += (size - 1) * heigthOffset;
                //newImage.transform.position = pos;
                pos.y = 0.52f + ((size - 2) * 0.37f);
                newImage.transform.localPosition = pos;
            }
        }
        ClickZone.offset = new Vector2(0, 0.37f * (size - 1));
    }

    public void OnPointerClick(PointerEventData data)
    {
        if (selectable && (Card.SelectedCard.selectedTargets.Count < Selection.currentSelection.targetCount))
        {
            bool valid = TargetSystem.IsValidTarget(this);
            if (valid)
            {
                Card.SelectedCard.selectedTargets.Add(this);
                if (Card.SelectedCard.selectedTargets.Count == Selection.currentSelection.targetCount)
                {
                    Board.Instance.CellReset();
                    Card.SelectedCard.TargetSelected();
                }
            }
        }
    }

    public bool SetStep(Vector2Int origin, int range, int step = 0)
    {
        if (stepValue <= step && Checked)
        {
            Debug.Log("not twice");
            return false;
        }
        Checked = true;
        stepValue = step;
        selectable = false;

        if (stepValue <= range)
        {
            sprite.color = Color.green;
            selectable = true;
            Debug.Log("in Range");
        }
        else if (stepValue <= range + 3)
        {
            Debug.Log("off Range");
            sprite.color = Color.red;
        }
        else
        {
            var dist = GetDistance(origin);
            if (dist > range + 3)
            {
                sprite.color = Color.white;
            }
            else
            {
                sprite.color = Color.red;
            }
        }
        return true;
    }

    public int GetDistance(Vector2Int pos)
    {
        var dist = Mathf.Abs(boardPosition.x - pos.x) + Mathf.Abs(boardPosition.y - pos.y);
        return dist;
    }

    public void ResetCell()
    {
        stepValue = -1;
        parentCell = null;
        stepNeeded = 0;
        Checked = false;
        SetSelectable(false);
    }

    public void SetSelectable(bool TrueOrFalse, bool noColor = true)
    {
        if (TrueOrFalse)
        {
            selectable = true;
            foreach(var msprite in GetComponentsInChildren<SpriteRenderer>())
            {
                msprite.color = Color.green;
            }
            //sprite.color = Color.green;
        }
        else
        {
            selectable = false;
            if (noColor)
            {
                foreach (var msprite in GetComponentsInChildren<SpriteRenderer>())
                {
                    msprite.color = Color.white;
                }
            }
            else
            {
                foreach (var msprite in GetComponentsInChildren<SpriteRenderer>())
                {
                    msprite.color = Color.red;
                }
            }
        }
    }

}
