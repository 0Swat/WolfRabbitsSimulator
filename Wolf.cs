// Wolf.cs
using System;

public class Wolf : Animal
{
    public Wolf(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override void Move()
    {
        X += Random.Next(-2, 3);
        Y += Random.Next(-2, 3);
    }

    public void MoveUp()
    {
        Y = Math.Max(0, Y - 1);
    }

    public void MoveDown()
    {
        Y = Math.Min(99, Y + 1);
    }

    public void MoveLeft()
    {
        X = Math.Max(0, X - 1);
    }

    public void MoveRight()
    {
        X = Math.Min(99, X + 1);
    }
}
