using System;

#region ICubeView
public interface ICubeView
{
    Field GetField();
    void UpdateField(Field field);
    event EventHandler<FieldEventArgs> FieldUpdated;
}
public interface IControlView
{
    void Reset();
    event EventHandler ResetClicked;
}
#endregion ICubeView


