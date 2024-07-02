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
        Y = Math.Max(1, Y - 1);
    }

    public void MoveDown()
    {
        Y = Math.Min(98, Y + 1);
    }

    public void MoveLeft()
    {
        X = Math.Max(1, X - 1);
    }

    public void MoveRight()
    {
        X = Math.Min(98, X + 1);
    }
}
