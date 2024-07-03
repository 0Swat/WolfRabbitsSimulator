public class Wolf : Animal
{
    // Konstruktor inicjalizuj�cy pozycj� wilka
    public Wolf(int x, int y)
    {
        X = x;
        Y = y;
    }

    // Implementacja metody Move() dla wilka (obecnie nieu�ywana)
    public override void Move()
    {
        // Losowy ruch o -2, -1, 0, 1 lub 2 w ka�dym kierunku
        X += Random.Next(-2, 3);
        Y += Random.Next(-2, 3);
    }

    // Metody do kontrolowanego ruchu wilka
    public void MoveUp()
    {
        // Ruch w g�r� z ograniczeniem do g�rnej granicy planszy
        Y = Math.Max(1, Y - 1);
    }

    public void MoveDown()
    {
        // Ruch w d� z ograniczeniem do dolnej granicy planszy
        Y = Math.Min(98, Y + 1);
    }

    public void MoveLeft()
    {
        // Ruch w lewo z ograniczeniem do lewej granicy planszy
        X = Math.Max(1, X - 1);
    }

    public void MoveRight()
    {
        // Ruch w prawo z ograniczeniem do prawej granicy planszy
        X = Math.Min(98, X + 1);
    }
}