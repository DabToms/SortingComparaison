namespace SortingComparaison;

/// <summary>
/// Kontekst wyniku sortowania.
/// </summary>
internal class SortingResultContext
{
    /// <summary>
    /// Nazwa algorytmu sortowania.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// PRzewidywana złożoność.
    /// </summary>
    public string? Complexity { get; set; }

    /// <summary>
    /// Czas trwania sortowania.
    /// </summary>
    public long SortingDuration { get; set; }

    /// <summary>
    /// Wyświetl wynik sortowania.
    /// </summary>
    public void PrintResults()
    {
        Console.WriteLine($"{this.Name}\t\t||\t\t{this.Complexity}\t\t||\t\t{this.SortingDuration}");
    }
}
