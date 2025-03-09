using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRPG;

public interface IItem
{
}
public abstract class Item : IItem
{
    public char signifying { get; protected set; }
    public string Name { get; protected set; }
    public bool NeedsTwoArms { get; protected set; }
    public Item(string name, bool needsTwoArms = false)
    {
        Name = name;
        NeedsTwoArms = needsTwoArms;
    }

}
public abstract class UselessItem : Item
{
    protected UselessItem(string name, bool needsTwoArms = false) : base(name, needsTwoArms) { signifying = 'U'; }
}
public class HollowShield : UselessItem
{
    public HollowShield() : base("Hollow Shield")
    {
    }
}
public class Log : UselessItem
{
    public Log() : base("Log", true) { signifying = 'L'; }
}
public abstract class Weapon : Item
{
    public int Damage { get; protected set; }
    protected Weapon(string name, bool needsTwoArms = false) : base(name, needsTwoArms)
    {
    }
}
public abstract class Sword : Weapon
{
    protected Sword(string name, bool needsTwoArms = false) : base(name, needsTwoArms)
    {
        Damage = 10;
    }
}
public class Dagger : Weapon
{
    public Dagger() : base("Dagger", false)
    {
        signifying = 'D';
    }
}
public class TwoHandedHeavySword : Sword
{
    public TwoHandedHeavySword() : base("Two Handed Heavy Sword", true)
    {
        Damage += 10;
        signifying = 'H';
    }
}
public abstract class Currency : Item
{
    public int Value { get; protected set; }
    public Currency(string name, int value)
        : base(name, false)
    {
        Value = value;
    }
}

public class Gold : Currency
{
    public Gold() : base("Gold", 8) { }
}
public class Coin : Currency
{
    public Coin() : base("Coin", 1) { signifying = 'C'; }
}