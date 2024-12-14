using System.Net;

namespace ChapterOne;

public abstract class Gun
{
    public string name;
    public IReload reload;
    
    public abstract void GunName();
    public abstract void FireGun();
    public abstract void Reload();
}