public abstract class Animal
{
    // W�a�ciwo�ci reprezentuj�ce pozycj� zwierz�cia
    public int X { get; protected set; }
    public int Y { get; protected set; }

    // Generator liczb losowych dla ruch�w zwierz�t
    protected Random Random { get; } = new Random();

    // Abstrakcyjna metoda ruchu, implementowana przez klasy pochodne
    public abstract void Move();
}