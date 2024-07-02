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
        int newX = X + Random.Next(-1, 2);
        int newY = Y + Random.Next(-1, 2);

        // Ensure the rabbit stays within the bounds (assuming a grid of 100x100)
        if (newX >= 1 && newX < 99)
        {
            X = newX;
        }

        if (newY >= 1 && newY < 99)
        {
            Y = newY;
        }
    }
}
