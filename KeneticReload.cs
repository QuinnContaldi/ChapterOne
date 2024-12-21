namespace ChapterOne;

public class KeneticReload : IReload
{
    public string AmmunitionType { get; set;} = "Kinetic";
    public void ReloadGun()
    {
        Console.WriteLine($"Reloading with {AmmunitionType}");
    }
}