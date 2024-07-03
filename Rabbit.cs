public class Rabbit : Animal
{
    // Konstruktor inicjalizuj¹cy pozycjê zaj¹ca
    public Rabbit(int x, int y)
    {
        X = x;
        Y = y;
    }

    // Implementacja metody Move() dla zaj¹ca
    public override void Move()
    {
        // Losowy ruch o -1, 0 lub 1 w ka¿dym kierunku
        int newX = X + Random.Next(-1, 2);
        int newY = Y + Random.Next(-1, 2);

        // Zapewnienie, ¿e zaj¹c pozostaje w granicach planszy (100x100)
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