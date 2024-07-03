public abstract class Animal
{
    // W³aœciwoœci reprezentuj¹ce pozycjê zwierzêcia
    public int X { get; protected set; }
    public int Y { get; protected set; }

    // Generator liczb losowych dla ruchów zwierz¹t
    protected Random Random { get; } = new Random();

    // Abstrakcyjna metoda ruchu, implementowana przez klasy pochodne
    public abstract void Move();
}