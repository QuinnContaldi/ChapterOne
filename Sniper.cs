using System.Net.NetworkInformation;

namespace ChapterOne;

public class Sniper : Gun
{
    public Sniper(string name, IReload reload)
    {
        this.name = name;
        this.reload = reload;
    }
    public override void GunName()
    {
        Console.WriteLine("Your current gun is a heavy duty " + name);
    }

    public override void FireGun()
    {
        Console.WriteLine($"Your {name} lets off a loud BANG firing a {reload.AmmunitionType} Heavy Round" );
    }

    public override void Reload()
    {
        reload.ReloadGun();
    }
}