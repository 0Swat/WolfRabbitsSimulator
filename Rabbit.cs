// Rabbit.cs
using System;

public class Rabbit : Animal
{
    public Rabbit(int x, int y)
    {
        X = x;
        Y = y;
    }

    public override void Move()
    {
        X += Random.Next(-1, 2);
        Y += Random.Next(-1, 2);
    }
}