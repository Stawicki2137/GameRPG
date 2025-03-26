using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRPG;

public abstract class Elixir : Item
{
    protected Item _item;

    protected Elixir(string name, bool needsTwoArms = false) : base(name, needsTwoArms) { 
        signifying = 'E'; 
        _item = this;
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
public class WisdomElixir : UselessItem
{
    public WisdomElixir() : base("Wisdom Elixir")
    {
        signifying = 'E';
    }
    public override void ApplyModifiers(Player player)
    {
        base.ApplyModifiers(player);
        player.ChangeWisdom(7);
    }
    public override void RemoveModifiers(Player player)
    {
        base.RemoveModifiers(player);
        player.ChangeWisdom(-7);
    }
}
public class PudzianElixir : UselessItem
{
    public PudzianElixir() : base("Pudzian Elixir")
    {
        signifying = 'E';
    }
    public override void ApplyModifiers(Player player)
    {
        base.ApplyModifiers(player);
        player.ChangeWisdom(7);
        player.ChangeAggression(6);
        player.ChangeLuck(4);
        player.ChangePower(7);
    }
    public override void RemoveModifiers(Player player)
    {
        base.RemoveModifiers(player);
        player.ChangeWisdom(-7);
        player.ChangeAggression(-6);
        player.ChangeLuck(-4);
        player.ChangePower(-7);
    }
    public override char GetSign() => signifying;
}
public class  HealthElixir: UselessItem
{
    public HealthElixir() : base("Health Elixir") { signifying = 'E'; }
    public override void ApplyModifiers(Player player)
    {
        base.ApplyModifiers(player);
        player.ChangeHealth(5);
    }
    public override void RemoveModifiers(Player player)
    {
        base.RemoveModifiers(player);
        player.ChangeHealth(5);
    }
}
