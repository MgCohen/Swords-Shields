using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Line Selection", menuName = "Selection/Line")]
public class LineSelection : Selection
{

    public bool ignoreHeigth;
    public bool ignoreWalls;
    public bool ignoreCenter;
    public bool ignoreContent;
    public bool passContent;

    private List<Vector2> directions = new List<Vector2>() { Vector2.down, Vector2.left, Vector2.up, Vector2.right };

    public override void Select(int Range, Vector2 origin)
    {
        base.Select(Range, origin);
        List<Cell> targets = new List<Cell>();
        Board.Instance.CellReset();
        int j = 0;
        if (ignoreCenter)
        {
            j = 1;
        }
        var originSize = Board.Instance.CellAt(origin).size;
        foreach(var dir in directions)
        {
            for (int i = j; i <= Range; i++)
            {
                Cell targetCell = Board.Instance.CellAt(origin + (dir * i));
                if (!targetCell)
                {
                    break;
                }
                if (!ignoreContent)
                {
                    if(targetCell.contained != null)
                    {
                        if (passContent)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (!ignoreHeigth)
                {
                    var size = targetCell.size;
                    if (size != originSize)
                    {
                        if (ignoreWalls)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (targetAll)
                {
                    targets.Add(targetCell);
                }
                else
                {
                    targetCell.SetSelectable(true);
                }
            }
        }

        if(targets.Count > 0)
        {
            foreach(var target in targets)
            {
                Card.SelectedCard.selectedTargets.Add(target);
            }
        }
    }
}
