using System;

public interface INaC_Model
{
    Field GetField(int i, int j);
    void UpdateGame(Field field);
    void Reset();
    event EventHandler<BoardEventArgs> BoardUpdated;
}
