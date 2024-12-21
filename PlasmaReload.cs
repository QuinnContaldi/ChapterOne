namespace ChapterOne;

public class PlasmaReload : IReload
{
    public string AmmunitionType {get; set;} = "Plasma";
    public void ReloadGun()
    {
        Console.WriteLine($"Reloading {AmmunitionType}");
    }
}