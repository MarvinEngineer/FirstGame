using System;

public class Board
{
    private Field[,] fs_quad;

    public Board()
    {
        fs_quad = new Field[3, 3];
        Reset();
    }
    public void Reset()
    {
        for (int i = 0; i < 3; i++)
            for (int j = 0; j < 3; j++)
            {
                fs_quad[i, j] = new Field();
                fs_quad[i, j].content = field.e;
            }
    }

    public field this[int i, int j]
    {
        get { return fs_quad[i, j].content; }
        set { fs_quad[i, j].content = value; }

    }

    public static Board operator +(Board obj1, Field obj2)
    {
        obj1[obj2.x, obj2.y] = obj2.content;
        return obj1;
    }

    public Field GetField(int i, int j)
    {
        if ((i < 3) && (j < 3)) return fs_quad[i, j];
        else throw new ArgumentOutOfRangeException();
    }
}
