Chapter One Strategy Design Pattern
===================================

## A Behavioral Design Pattern

**Strategy** is a behavioral design pattern that lets you define a family of algorithms,
put each of them into a separate class, and make their objects interchangeable. This may seem like a
verbose way to describe this design pattern, but stay with me! The big idea is to dynamically change
behavior and decouple our code as much as possible using SOLID OOP principles

1. **"Why use this design pattern instead of interfaces or abstract methods?"**
   - Interfaces and abstract methods are helpful! In fact, we use interfaces in our Abstract Gun Class, However they
     can couple behavior too tightly.
   - The **Strategy** design pattern gives us a way to leverage **runtime polymorphism**
     by dynamically assign behavior!

2. **How can we utilize runtime polymorphism with this strategy?**
   - By creating behaviors as separate classes that implement a common interface: we can encapsulate behavior and reuse it.
     This allows reduces the amount of code we would have to implement.
   - Every child that inherits from and abstract parent must implement the abstract functions. Imagine if we had 40 futuristic guns all having to implement their own reload
     functions.
   - Imagine if we wanted to add some new bullets like Acid bullets. Now Imagine if we want to change the
     ammunition type mid game! This further complicates the problem... Thats where our Strategy Design Pattern saves the day.
   - This also provides one more benefit dependency injection meaning that its easy to add another gun. We
     wont have to go into every gun class we made to implement a whack if statement to create different bullets.

3. **Why should we program to an interface, not an implementation?**
   - Programming to an interface lets us depend on abstractions rather than concrete implementations. It improves
   flexibility,
   - reduces coupling, and enables easy substitution when behavior changes without affecting the high-level components.

4. **Interfaces are not a bad approach**
    - They obviously have their spot in programming however,
     they are not always the best solution. Interfaces and abstract classes are valid solutions but can become rigid when
     heavy logic is tied directly to the implementation.
   - *"What are the issues? programming to an interface seems like a good solution"*  **WAIT** I promise ill get to that soon.
     Just keep that question in the back of your mind for now. Let's start out by exploring a primitive method to creating
     guns for our video game. Through this we can better understand the problem we will be facing

### We Want To Create Some Guns For Our SUPER COOL FPS GAME!
***

## A Primitive Implementation


```csharp
Public Class Shotgun
{
    fire()
    {
        Console.Writeline("Bang!");   
    }
    Reload()
    {
        Console.WriteLine("I am loading shells!");    
    }
}
// Our next gun! pew pew 
Public Class Sniper
{
    string Ammo = "HeavyKenetic";
    fire()
    {
        Console.Writeline("Large Caliber Bang!");   
    }
    Reload()
    {
        Console.WriteLine("I am loading!" + Ammo + " Ammo");    
    }
}
// Oh sick our players are shooting their guns pew pew 
public PlayerGuns()
{
    int currentGun;
    private Sniper sniper = new sniper();
    private Shotgun shotgun = new shotgun();
    
    public void ShootGuns(int gun)
    {
        if(gun == 0)
        {
            sniper.fire();       
        }
        if(gun == 1)
        {
            shotgun.fire();
        }
    }
}
```
***

## Exploring What's Wrong With This Code

This was a lesson taught to me in my freshman computer science courses. *"Just because
it compiles does not mean it is correct"*. Even if it is correct other solutions can be **More** correct.
by having our high level code depend on low level details we do a bad job of decoupling our code.
Furthermore, we are unable to leverage runtime polymorphism to change guns mid game. Now think
about adding a new gun. Things become far more complicated, as every class that we
placed our `ShootGuns` if statement to we need to add our additional guns. What a headache for extending our code.
Lets take a look at the problem for this code segment

1. **High-level code depends on low-level details**
    - The `PlayerGuns` class is tightly coupled to specific gun types (`Sniper`, `Shotgun`). This makes the code harder
      to maintain or extend.

2. **Limited extensibility**
    - Adding a new gun requires modifying multiple places in your code. For instance:
    - Create a new gun class.
    - Update all methods (e.g., `ShootGuns`) to support the new gun by adding `if-else` or `switch` statements.

3. **No runtime polymorphism**
    - Switching guns at runtime is cumbersome because the logic for gun management is embedded in `PlayerGuns`. Adding
      conditions or more guns makes this harder over time.

4. **Code duplication**
    - Similar functionality—for example, the kinetic reload logic for guns using kinetic ammo—gets duplicated, leading
      to unnecessary redundancy.

**Clearly, we need a better solution.**

"See! this is the perfect place to use an interface! We can create a parent gun class and have all the children
define their respective fire and reload methods!"*

That is not a bad idea, in fact I would consider that a correct solution. Let's give it a try! We will attempt to
write a solution that utilizes the IGun interface where child objects define their own reload and shoot behavior.
***

## Moving Toward an Improved Design

You might think, _"Why not use an interface? Each gun can define its own `Fire()` and `Reload()` methods!"_ While that’s
a better approach,

```csharp
public abstract class Gun
{
    public string name;
    public string AmmunitionType;
    public void abstract FireGun();
    public void abstract ReloadGun();
}

public class sniper : Gun 
{
    string = Sniper
    AmmunitionType = "Kinetic Rounds"
    public void FireGun()
    {
        Console.Writeline($"Sniper is very loud and Firing heavy {AmmunitionType}")    
    }
    public void ReloadGun()
    {
        Console.Writeline($"Sniper is Reloading new {AmmunitionType}")
    }
}
public class Shotgun : Gun
{
    public string AmmoType = "Shells";

    public void FireGun()
    {
        Console.WriteLine($"Shotgun fires {AmmoType} loudly scattering pellets everywhere!");
    }

    public void ReloadGun()
    {
        Console.WriteLine($"Inserting multiple {AmmoType} into the shotgun...");
    }
}

public class Pistol : Gun
{
    public string AmmoType = "Light";

    public void FireGun()
    {
        Console.WriteLine($"Pistol fires quickly with {AmmoType} ammunition!");
    }

    public void ReloadGun()
    {
        Console.WriteLine($"Reloading the pistol with a fresh magazine of {AmmoType} ammo.");
    }
}
```

***
## Why Using Abstract Classes Isn't Always Modular

Interfaces and Abstract classes are useful in many scenarios to enforce functions that child classes must follow.
As you can see this is not a bad use of inheritance with abstract classes, but adding more weapons like this is straightforward.
However, as the number of guns grows and additional unique behaviors are introduced, maintaining this structure will eventually
become a nightmare. For example in a game where we have multiple `Guns`, we can create a `Gun` class forcing each gun to implement methods
like `FireGun()` and `ReloadGun()`. Again we face similar problems to our first design.

1. **Tight Coupling**
    - Each gun must implement its firing and reloading behavior directly. Even if multiple guns share the same logic (e.g., `ReloadGun()` for all guns using kinetic ammo), they must still duplicate that implementation.
    - If we decide to modify the behavior for `ReloadGun` (lets say we introduce a new mechanic or ammo type), we must update every class where it’s implemented... Now that is what I call technical debt

2. **Inflexibility to Change at Runtime**
    - Suppose we want the player to change ammunition types mid-game (e.g., switching a sniper to use "Explosive Rounds" instead of "Kinetic Rounds"). With this design, it would require changing the logic for each gun manually or introducing unwieldy conditional statements.

3. **Guns become harder to maintain**
    - I know I keep beating a dead horse here, but I cant stress this enough, especially for game developers. This is a large violation of the **Open/Closed Principle**
      Your code should be open for extension, but closed to modification. Who wants to track down every gun child object and add changes... not this developer.

4. **Lastly, just like the last design we have code duplication**
    - There is a lot of opinions on code duplication. Some love it because its more readable, and some hate it because it violates good OOP practices.
      For me, it depends, if only two or three classes have to duplicate the same logic. It may be worth your time to simple keep this implementation, instead of implementing
      the strategy design pattern.
    - Is the implementation of the strategy design pattern take more lines of code then just duplicating a small segment of code between classes?
      maybe that's a situation where you could "get away with it." Just be careful, what is three spots of duplicated logic can quickly balloon to 100 spots.
---
## Using The Strategy Design Pattern: A Modular Approach To Gun Reload Behavior

### Introduction To The Strategy Design Pattern

Consider a First-Person Shooter (FPS) game where guns have different types of reloading mechanisms. For example:
- A **Sniper Rifle** uses *Kinetic Rounds*.
- A **Plasma Gun** uses *Plasma Rounds*.
Instead of tightly coupling the reload logic to each gun class, we can offload this behavior to separate `IReload` strategies. We keep the `Fire()` method as an interface
*"Oh? so we are replacing the Ireload method becomes our **strategy object**, instead of making it a function? we then can swap out the `IReload` with other **Strategy objects** during runtime!"*
Yes that is exactly it! One more thing to note, is that `Fire()` is still a function that needs to be defined. The IReload interface contains a `ammunitionType`
Since the ammunition we reload into the gun should dictate what round is fired from the gun.
---
## The Architecture

### 1. The `Gun` Abstract Class

The `Gun` class serves as the base class for all types of guns. It defines the common behavior for all guns while delegating specific actions (e.g., reloading) to a strategy object implementing the `IReload` interface.

```csharp
namespace ChapterOne;

public abstract class Gun
{
    public string name;
    public IReload reload;
    
    public abstract void GunName();
    public abstract void FireGun();
    public abstract void Reload();
}
```

### 2. The Strategy Interface: `IReload`

The `IReload` interface represents the **reload behavior**. Each specific implementation of `IReload` will describe how a gun reloads and the type of ammunition it uses.

```csharp
namespace ChapterOne;

public interface IReload
{
    string AmmunitionType { get; set; }
    public void ReloadGun();
}
```

### 3. Concrete Reload Strategies

The **reload mechanism** varies between guns. By implementing `IReload`, we create specific reload strategies that can be reused for multiple guns.

#### Kinetic Reload

```csharp
namespace ChapterOne;

public class KeneticReload : IReload
{
    public string AmmunitionType { get; set;} = "Kinetic";
    public void ReloadGun()
    {
        Console.WriteLine($"Reloading with {AmmunitionType}");
    }
}
```

#### Plasma Reload

```csharp
namespace ChapterOne;

public class PlasmaReload : IReload
{
    public string AmmunitionType {get; set;} = "Plasma";
    public void ReloadGun()
    {
        Console.WriteLine($"Reloading {AmmunitionType} Rounds");
    }
}
```

### 4. Concrete Gun Class: `Sniper`

The `Sniper` class extends the `Gun` abstract class and delegates actions like firing and reloading to the associated `IReload` strategy.

```csharp
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
        Console.WriteLine("Your current gun is " + name);
    }

    public override void FireGun()
    {
        Console.WriteLine($"Your current gun is firing {reload.AmmunitionType}");
    }

    public override void Reload()
    {
        reload.ReloadGun();
    }
}
```

---

## Explanation Of The Implementation

1. **The Strategy**  
   - Each gun delegates its reloading behavior to a strategy object (`IReload`). This makes it easy to reuse the same reload logic across multiple gun types.

2. **Runtime Behavior Changes**  
   - The reloading behavior (`reload`) can be swapped at runtime, allowing guns to dynamically change their ammunition without modifying the class itself.

3. **Separation of Concerns**  
   - The `Gun` class focuses solely on managing gun-related behavior (e.g., firing, naming). Reload behavior is managed independently by `IReload` implementations.

4. **Code Reusability**  
   - Multiple guns can share the same reload logic, reducing code duplication (e.g., both a sniper and a rifle could use `KeneticReload`).

5. **For More Information** 
    - Please read the first chapter of the Head First Design Patterns! If you are a student you can access this books for free at [O'RIELLY press](https://www.oreilly.com/member/login/?next=%2Fapi%2Fv1%2Fauth%2Fopenid%2Fauthorize%2F%3Fclient_id%3D814340%26redirect_uri%3Dhttps%3A%2F%2Fmembers.oreilly.com%2Fcallback%2F%26response_type%3Dcode%26scope%3Dopenid&locale=en)
---

## Usage Example

This example demonstrates how to use the `Sniper` class with different `IReload` strategies:

```csharp
using ChapterOne;

class Program
{
    static void Main()
    {
        // Create reload strategies
        var kineticReload = new KeneticReload();
        var plasmaReload = new PlasmaReload();

        // Create a sniper gun using kinetic reload
        var sniper = new Sniper("Sniper Rifle", kineticReload);

        sniper.GunName();       // Output: Your current gun is Sniper Rifle
        sniper.FireGun();       // Output: Your current gun is firing Kinetic
        sniper.Reload();        // Output: Reloading with Kinetic

        // Switch to plasma reload at runtime
        sniper.reload = plasmaReload;

        sniper.FireGun();       // Output: Your current gun is firing Plasma
        sniper.Reload();        // Output: Reloading Plasma Rounds
    }
}
```

---

## Benefits of the Strategy Pattern in This Example

1. **Decoupling Behavior**:  
   Guns are decoupled from their reload logic. This makes the `Gun` class more manageable and not cluttered with behavior specifics.

2. **Code Extensibility**:
   Adding a new reloading behavior (e.g., Acid Rounds) is as simple as creating a new class implementing `IReload`. Guns can instantly adopt this new behavior without modifying existing code.

```csharp
public class AcidReload : IReload
   {
       public string AmmunitionType { get; set; } = "Acid";
       public void ReloadGun()
       {
           Console.WriteLine($"Carefully reloading corrosive {AmmunitionType} rounds...");
       }
   }
```

3. **Runtime Polymorphism**:  
   Guns can change their reload strategies mid-game, allowing dynamic behavior changes (as shown in the usage example). This is impossible with rigid inheritance structures.

4. **Adherence to SOLID Principles**:
    - **Single Responsibility**: Guns and reload behaviors are separate concerns.
    - **Open/Closed**: New behaviors can be added without changing the existing guns.
    - **Dependency Inversion**: High-level classes (e.g., guns) depend on abstractions (`IReload`), not concrete implementations.

---

## Conclusion

This implementation follows the **Strategy Design Pattern** to simplify and modularize the behavior of guns in an FPS game. By encapsulating `Reload` behavior into separate strategies,
the design becomes more flexible, reusable, and extensible, all while promoting clean and maintainable code.

# A Message from Northstar
## "Thankyou for reading! I cant wait to see you in the next chapter! Have fun being epic with OOP!"
![Northstar the awesome amazing cute catgirl](Images/Catgirl.png)
   
