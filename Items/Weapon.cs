using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRPG;

public interface IWeapon
{
    //int GetDamage();
    void ApplyModifiersOnWeapon(int x);
    void RemoveModifiersOnWeapon(int x);
}
public abstract class Weapon : Item , IWeapon
{
    public int Damage { get; protected set; }
    protected Weapon(string name, bool needsTwoArms = false) : base(name, needsTwoArms)
    {
        signifying = 'W';
    }

    public void ApplyModifiersOnWeapon(int x)
    {
        Damage += x;
    }

    public void RemoveModifiersOnWeapon(int x)
    {
        Damage -= x;
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
        Damage = 10;
    }
}
public class LightSword : Sword
{
    public LightSword() : base("Light Sword")
    {
        Damage += 2;
        signifying = 'L';
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
