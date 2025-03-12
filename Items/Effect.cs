using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRPG;


public abstract class WeaponEffectDecorator : Item
{
    protected Item _item;
    public WeaponEffectDecorator(Item item): base(item.Name, item.NeedsTwoArms)
    {
        _item = item;
    }
    public override char GetSign()
    {
        return _item.GetSign();
    }
    public override string GetName()
    {
        return $"{_item.GetName()} ({this.GetType().Name.Replace("Decorator", "")})";
    }
    public void ApplyModifiersOnWeapon(int x) { }
    public void RemoveModifiersOnWeapon(int x) { }

    public override void ApplyModifiers(Player player)
    {
        _item.ApplyModifiers(player);
    }

    public override void RemoveModifiers(Player player)
    {
        _item.RemoveModifiers(player);
    }

}
public class UltraStrong : WeaponEffectDecorator
{
    public UltraStrong(Item item) : base(item) { }
    public override void ApplyModifiers(Player player)
    {
        base.ApplyModifiers(player);
        player.ChangeAggression(3);
        ApplyModifiersOnWeapon(5);
    }
    public override void RemoveModifiers(Player player)
    {
        base.RemoveModifiers(player);
        player.ChangeAggression(-3);
        RemoveModifiersOnWeapon(5);

    }
}

public abstract class EffectDecorator : Item
{
    protected Item _item;

    public EffectDecorator(Item item) : base(item.Name, item.NeedsTwoArms)
    {
        _item = item;
    }
    public override char GetSign()
    {
        return _item.GetSign();
    }
    public override string GetName()
    {
        return $"{_item.GetName()} ({this.GetType().Name.Replace("Decorator", "")})";
    }

    public override void ApplyModifiers(Player player)
    {
        _item.ApplyModifiers(player);
    }

    public override void RemoveModifiers(Player player)
    {
        _item.RemoveModifiers(player);
    }
}

public class Damned : EffectDecorator
{
    public Damned(Item item) : base(item) { }
    public override void ApplyModifiers(Player player)
    {
        base.ApplyModifiers(player);
        player.ChangeAll(-2);
    }
    public override void RemoveModifiers(Player player)
    {
        base.RemoveModifiers(player);
        player.ChangeAll(2);

    }
}
public class Strong : EffectDecorator
{
    public Strong(Item item) : base(item) { }
    public override void ApplyModifiers(Player player)
    {
        base.ApplyModifiers(player);
        player.ChangePower(4);
    }
    public override void RemoveModifiers(Player player)
    {
        base.RemoveModifiers(player);
        player.ChangePower(-4);
    }
}
public class HolyDecorator : EffectDecorator
{
    public HolyDecorator(Item item) : base(item) { }
    public override void ApplyModifiers(Player player)
    {
        base.ApplyModifiers(player);
        player.ChangeLuck(5);
        player.ChangeWisdom(4);
    }
    public override void RemoveModifiers(Player player)
    {
        base.RemoveModifiers(player);
        player.ChangeLuck(-5);
        player.ChangeWisdom(-4);
    }

}
public class LuckyDecorator : EffectDecorator
{
    public LuckyDecorator(Item item) : base(item) { }

    public override void ApplyModifiers(Player player)
    {
        base.ApplyModifiers(player);
        player.ChangeLuck(3);
    }
    public override void RemoveModifiers(Player player)
    {
        base.RemoveModifiers(player);
        player.ChangeLuck(-3);
    }


}
public class UnluckyDecorator : EffectDecorator
{
    public UnluckyDecorator(Item item) : base(item) { }

    public override void ApplyModifiers(Player player)
    {
        base.ApplyModifiers(player);
        player.ChangeLuck(-3);
    }

    public override void RemoveModifiers(Player player)
    {
        base.RemoveModifiers(player);
        player.ChangeLuck(3);
    }
}
