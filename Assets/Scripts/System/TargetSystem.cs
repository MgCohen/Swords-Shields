using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum Targets
{
    Cardtargets = -1,
    Cell = 0,
    Enemy = 1,
    Player = 2,
    Character = 3,
    Construct = 4,
    Any = 5
}

public class TargetSystem
{
    public static List<ITarget> GetTargets(List<Cell> cells, Targets specificTarget = Targets.Cardtargets)
    {
        if (specificTarget == Targets.Cardtargets)
        {
            specificTarget = Card.SelectedCard.targetType;
        }
        List<ITarget> targets = new List<ITarget>();
        if (specificTarget == Targets.Cell)
        {
            foreach (var cell in cells)
            {
                targets.Add(cell);
                return targets;
            }
        }
        else if (specificTarget == Targets.Enemy)
        {
            foreach (var cell in cells)
            {
                var enm = cell.contained.GetComponent<Enemy>();
                if (enm)
                {
                    targets.Add(enm);
                }
            }
        }
        else if (specificTarget == Targets.Player)
        {
            foreach (var cell in cells)
            {
                var pl = cell.contained.GetComponent<Player>();
                if (pl)
                {
                    targets.Add(pl);
                }
            }
        }
        else if (specificTarget == Targets.Character)
        {
            foreach (var cell in cells)
            {
                var ch = cell.contained.GetComponent<Character>();
                if (ch)
                {
                    targets.Add(ch);
                }
            }
        }
        else if (specificTarget == Targets.Construct)
        {
            foreach (var cell in cells)
            {
                var ct = cell.contained.GetComponent<Construct>();
                if (ct)
                {
                    targets.Add(ct);
                }
            }
        }
        else if (specificTarget == Targets.Any)
        {
            foreach (var cell in cells)
            {
                var an = cell.contained.GetComponent<ITarget>();
                if (an != null)
                {
                    targets.Add(an);
                }
            }
        }
        return targets;
    }

    public static bool IsValidTarget(ITarget target, Targets specificTarget = Targets.Cardtargets)
    {
        if (specificTarget == Targets.Cardtargets)
        {
            specificTarget = Card.SelectedCard.targetType;
        }
        GameObject targetObj = (target as Cell).contained;
        if (target is Cell && specificTarget == Targets.Cell)
        {
            return true;
        }
        if (!targetObj && specificTarget != Targets.Cell)
        {
            return false;
        }
        if (targetObj.GetComponent<Enemy>() && specificTarget == Targets.Enemy)
        {
            return true;
        }
        if (targetObj.GetComponent<Player>() && specificTarget == Targets.Player)
        {
            return true;
        }
        if (targetObj.GetComponent<Character>() && specificTarget == Targets.Character)
        {
            return true;
        }
        if (targetObj.GetComponent<Construct>() && specificTarget == Targets.Construct)
        {
            return true;
        }
        if (targetObj && specificTarget == Targets.Any)
        {
            return true;
        }
        return false;
    }

    public static bool IsValidTarget(out GameObject body, ITarget target, Targets specificTarget = Targets.Cardtargets)
    {
        body = null;
        if (specificTarget == Targets.Cardtargets)
        {
            specificTarget = Card.SelectedCard.targetType;
        }
        GameObject targetObj = (target as Cell).contained;
        if (target is Cell && specificTarget == Targets.Cell)
        {
            body = (target as Cell).gameObject;
            return true;
        }
        if (!targetObj && specificTarget != Targets.Cell)
        {
            return false;
        }
        if (targetObj.GetComponent<Enemy>() && specificTarget == Targets.Enemy)
        {
            body = (target as Enemy).gameObject;
            return true;
        }
        if (targetObj.GetComponent<Player>() && specificTarget == Targets.Player)
        {
            body = (target as Player).gameObject;
            return true;
        }
        if (targetObj.GetComponent<Character>() && specificTarget == Targets.Character)
        {
            body = (target as Character).gameObject;
            return true;
        }
        if (targetObj.GetComponent<Construct>() && specificTarget == Targets.Construct)
        {
            body = (target as Construct).gameObject;
            return true;
        }
        if (targetObj && specificTarget == Targets.Any)
        {
            return true;
        }
        return false;
    }

    public static List<Cell> SetPath(Cell source, Cell Target, bool ignoreHeigth = true, bool wallBlock = false)
    {
        Board.Instance.CellReset();
        List<Cell> openList = new List<Cell>();
        List<Cell> closedList = new List<Cell>();
        Cell activeCell;
        openList.Add(source);
        source.stepNeeded = Board.Instance.GetDistance(source, Target);
        List<Cell> path = new List<Cell>();
        while (openList.Count > 0)
        {
            activeCell = GetPriorityCell(openList);
            openList.Remove(activeCell);
            closedList.Add(activeCell);
            if(activeCell == Target)
            {
                path.Add(activeCell);
                while(activeCell.parentCell != null)
                {
                    activeCell = activeCell.parentCell;
                    path.Add(activeCell);
                }
                break;
            }
            var cells = Board.Instance.GetConnectedCells(activeCell);
            foreach (var cell in cells)
            {
                if ((wallBlock && cell.size > activeCell.size) || closedList.Contains(cell))
                {
                    continue;
                }

                if (!openList.Contains(cell))
                {
                    openList.Add(cell);
                    cell.parentCell = activeCell;
                    cell.stepValue = activeCell.stepValue + 1;
                    if (!ignoreHeigth) cell.stepValue += (Mathf.Abs(cell.size - activeCell.size)) - 1;
                    cell.stepNeeded = Board.Instance.GetDistance(cell, Target);
                }
                else
                {
                    var newStep = activeCell.stepValue + 1;
                    if (!ignoreHeigth) newStep += (Mathf.Abs(cell.size - activeCell.size)) - 1;
                    if (newStep < cell.stepValue)
                    {
                        cell.parentCell = activeCell;
                        cell.stepValue = newStep;
                    }
                }
            }
        }
        path.Reverse();
        return path;
    }

    public static Cell GetPriorityCell(List<Cell> cells)
    {
        cells.OrderBy(x => x.stepValue + x.stepNeeded);
        return cells[0];
    }
}

public interface ITarget
{
}
