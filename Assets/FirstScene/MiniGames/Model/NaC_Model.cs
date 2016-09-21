using UnityEngine;
using System;

public enum field { e, X, O }

#region EventArgs
public class FieldEventArgs : EventArgs
{
    public Field Field { get; set; }
    public FieldEventArgs(Field field)
    {
        Field = field;
    }
}

public class BoardEventArgs : EventArgs
{
    public Board Board { get; set; }
    public BoardEventArgs(Board board)
    {
        Board = board;
    }
}
#endregion EventArgs

public class NaC_Model : INaC_Model
{
    public event EventHandler<BoardEventArgs> BoardUpdated;

    private NaC_Game game;

    public NaC_Model()
    {
        game = new NaC_Game();
    }

    public Field GetField(int i, int j)
    {
        return game.board.GetField(i,j);
    }

    public void UpdateGame(Field _field)
    {
        game.Click(_field);
        BoardUpdated(this, new BoardEventArgs(game.board));
    }

    public void Reset()
    {
        game = new NaC_Game();
    }
}

public class NaC_Game
{
    public Board board { get; set; }

    private NaC_AI ai;

    private int currentStep;

    public NaC_Game()
    {
        board = new Board();
        currentStep = 1;
        ai = new NaC_AI();
    }

    public void Click(Field clicked)
    {
        UpdateBoard(clicked);

        if (Check(clicked.x, clicked.y, field.X))
        {
            Win();
            return;
        }
        else
        {
            currentStep += 1;
            if (currentStep == 10) { Tie(); return; }
            Field _f = ai.play(currentStep / 2);
            if (_f != null) board += _f;            
            if (Check(_f.x, _f.y, field.O)) { Lose(); }
            currentStep++;
            return;
        }

    }

    public void UpdateBoard(Field field)
    {
        board += field;
        ai.UpdateBoard(board);
    }

    private void Win()
    {
        Debug.Log("win");
    }

    private void Lose()
    {
        Debug.Log("Lose");
    }

    private void Tie()
    {
        Debug.Log("Tie");
    }    

    #region Check

    private bool Check(int _i, int _j, field _F)
    {
        if (CheckDiagonal(_i, _j)) return true;
        if (CheckColumn(_j)) return true;
        if (CheckRow(_i)) return true;
        return false;
    }

    private bool CheckDiagonal(int _i, int _j)
    {
        if (board[1, 1] != 0)
        {
            if ((board[1, 1] == board[2, 2]) && (board[1, 1] == board[0, 0])) return true;
            if ((board[0, 2] == board[1, 1]) && (board[2, 0] == board[0, 2])) return true;
        }
        return false;
    }
    
    private bool CheckRow(int _i)
    {
        field tmp = board[_i, 0];
        for (int j = 1; j < 3; j++)
        {
            if (!CheckTwo(tmp, board[_i, j])) return false;
        }
        return true;
    }

    private bool CheckColumn(int _j)
    {
        field tmp = board[0, _j];
        for (int i = 1; i < 3; i++)
        {
            if (!CheckTwo(tmp, board[i, _j])) return false;
        }
        return true;

    }

    private bool CheckTwo(field _a, field _b)
    {
        if ((_a != field.e) && (_b != field.e))
        {
            if (_a == _b) return true;
            else return false;
        }
        else return false;
    }

    #endregion Check

}

