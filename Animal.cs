// Animal.cs
using System;

public abstract class Animal
{
    public int X { get; protected set; }
    public int Y { get; protected set; }
    protected Random Random { get; } = new Random();

    public abstract void Move();
}