using System;

public class Field
{
    public int x { get; set; }
    public int y { get; set; }
    public field content { get; set; }

    public Field()
    {
        x = 0;
        y = 0;
        content = field.e;
    }
    public Field(int i, int j, field f)
    {
        x = i;
        y = j;
        content = f;
    }
    public Field(int i, field f)
    {
        switch (i)
        {
            case 0: { x = 1; y = 1; break; }
            case 1: { x = 0; y = 0; break; }
            case 2: { x = 0; y = 1; break; }
            case 3: { x = 0; y = 2; break; }
            case 4: { x = 1; y = 2; break; }
            case 5: { x = 2; y = 2; break; }
            case 6: { x = 2; y = 1; break; }
            case 7: { x = 2; y = 0; break; }
            case 8: { x = 1; y = 0; break; }
            default: throw new ArgumentOutOfRangeException();
        }
        content = f;
    }
    public int GetLineInt()
    {
        switch (x)
        {
            case 0:
                {
                    if (y == 0) return 1;
                    if (y == 1) return 2;
                    if (y == 2) return 3;
                    throw new ArgumentOutOfRangeException();
                }
            case 1:
                {
                    if (y == 0) return 8;
                    if (y == 1) return 0;
                    if (y == 2) return 4;
                    throw new ArgumentOutOfRangeException();
                }
            case 2:
                {
                    if (y == 0) return 7;
                    if (y == 1) return 6;
                    if (y == 2) return 5;
                    throw new ArgumentOutOfRangeException();
                }
            default: throw new ArgumentOutOfRangeException();
        }
    }
}

