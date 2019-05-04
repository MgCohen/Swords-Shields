using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Board : MonoBehaviour
{
    public static Board Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public List<Cell> cells = new List<Cell>();

    public Cell[,] board;

    public Vector2 boardlenght = new Vector2();

    private void OnEnable()
    {
        int maxX = 0;
        int maxY = 0;
        foreach (var cell in cells)
        {
            var pos = MatrixSolver.SolveFor(cell.transform.position);
            if (pos.x > maxX)
            {
                maxX = Mathf.RoundToInt(pos.x);
            }
            if (pos.y > maxY)
            {
                maxY = Mathf.RoundToInt(pos.y);
            }
            cell.boardPosition = pos;
        }
        board = new Cell[maxX + 1, maxY + 1];
        boardlenght = new Vector2(maxX, maxY);
        foreach (var cell in cells)
        {
            board[(int)cell.boardPosition.x, (int)cell.boardPosition.y] = cell;
        }
    }

    public List<Cell> GetConnectedCells(Cell cell)
    {
        List<Cell> connections = new List<Cell>();

        Vector2Int pos = new Vector2Int((int)cell.boardPosition.x, (int)cell.boardPosition.y);
        if (pos.x - 1 >= 0)
        {
            if (board[pos.x - 1, pos.y] != null)
            {
                connections.Add(board[pos.x - 1, pos.y]);
            }
        }
        if (pos.x + 1 <= boardlenght.x)
        {
            if (board[pos.x + 1, pos.y] != null)
            {
                connections.Add(board[pos.x + 1, pos.y]);
            }
        }
        if (pos.y - 1 >= 0)
        {
            if (board[pos.x, pos.y - 1] != null)
            {
                connections.Add(board[pos.x, pos.y - 1]);
            }
        }
        if (pos.y + 1 <= boardlenght.y)
        {
            if (board[pos.x, pos.y + 1] != null)
            {
                connections.Add(board[pos.x, pos.y + 1]);
            }
        }
        return connections;
    }

    public void MarkCells(Cell Origin, int range)
    {
        StartCoroutine(MarkCO(Origin, range));
    }

    public void CellReset()
    {
        StopAllCoroutines();
        foreach (var cell in board)
        {
            if (cell)
                cell.ResetCell();
        }
    }

    IEnumerator MarkCO(Cell origin, int range)
    {

        List<Cell> pendingCells = new List<Cell>();
        pendingCells.Add(origin);
        origin.SetStep(origin.boardPosition, range);
        while (pendingCells.Count > 0)
        {
            List<Cell> tempCells = new List<Cell>();
            foreach (var cell in pendingCells)
            {
                var adjCells = GetConnectedCells(cell);
                foreach (var tCell in adjCells)
                {
                    var reach = tCell.SetStep(origin.boardPosition, range, cell.stepValue + tCell.stepCost);
                    if (reach)
                    {
                        tempCells.Add(tCell);
                    }
                }
            }
            pendingCells.Clear();
            foreach (var cell in tempCells)
            {
                pendingCells.Add(cell);
            }
            //yield return new WaitForSeconds(0.05f);
        }
        yield return null;
    }

    public Cell CellAt(Vector2 pos, bool worldPos = false)
    {
        if (!worldPos)
        {
            if (pos.x < 0 || pos.y < 0 || pos.x > boardlenght.x || pos.y > boardlenght.y)
            {
                return null;
            }
            return board[(int)pos.x, (int)pos.y];
        }
        else
        {
            var boardPos = MatrixSolver.SolveFor(pos);
            return CellAt(boardPos);
        }
    }

    public int GetDistance(Cell origin, Cell target)
    {
        var A = origin.boardPosition;
        var B = target.boardPosition;
        A = new Vector2Int(Mathf.Abs(A.x - B.x), Mathf.Abs(A.y - B.y));
        return A.x + A.y;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            MarkCells(cells[0], 3);
        }
    }
}
