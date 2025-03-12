using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRPG;

/// <summary>
///  Zapytac Szymona o to koniecznie!
/// </summary>
//public abstract class EffectOnWeaponDecorator : Item {}

  // kurde nw 
  // pytanie na konsultacje:
// dekoratory do broni jak ogarnac??
// spytac czy coin i gold ogolnie currency moge trzymac na osobnej liscie jakby w tile albo rozpoznawac typ przy podnoszeniu
// tzn miec metode isCurrency ?

// kosmetyczne rzeczy: limit rzeczy w eq 
// poprawic generowanie (moze dac generowanie przedmiotow)

/*
public abstract class WeaponEffectDecorator : EffectDecorator
{

}
*/
public abstract class EffectDecorator : Item
{
    protected Item _item;

    public EffectDecorator(Item item) : base(item.Name, item.NeedsTwoArms)
    {
        _item = item;
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
