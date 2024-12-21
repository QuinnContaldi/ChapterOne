namespace ChapterOne;

public class LazerReload : IReload
{
    public string AmmunitionType { get; set; } = "Lazer";
    public void ReloadGun()
    {
        Console.WriteLine($"Recharging {AmmunitionType}");
    }
}