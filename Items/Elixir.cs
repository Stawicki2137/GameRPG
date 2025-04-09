using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRPG;

public abstract class TimedElixirObserver : IObserver
{
    private int _remainingTurns;
    private bool _applied = false;

    protected TimedElixirObserver(int duration)
    {
        _remainingTurns = duration;
    }
    public void Update(ISubject subject)
    {
        if (subject is not Player player)
            return;

        if (!_applied)
        {
            OnApply(player);
            _applied = true;
        }
        _remainingTurns--;
        if (_remainingTurns <= 0)
        { 
            OnRemove(player);
            player.Detach(this);
        }
    }
    protected abstract void OnApply(Player player);
    protected abstract void OnRemove(Player player);
}

public abstract class Elixir : Item
{
    protected Item _item;
    protected Elixir(string name, bool needsTwoArms = false, bool apply=false) : base(name, needsTwoArms,apply) { 
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

public class WisdomElixir : Item
{
    private int _duration;

    public WisdomElixir(int duration = 20)
        : base("Wisdom Elixir") 
    {
        signifying = 'E';
        _duration = duration;
    }

    public override void ApplyModifiers(Player player)
    {
        player.Attach(new WisdomElixirEffect(_duration));
    }

    public override void RemoveModifiers(Player player)
    {
        // tu mozna dodac antidotum w razie cos 
    }
}

public class WisdomElixirEffect : TimedElixirObserver
{

    public WisdomElixirEffect(int duration) : base(duration) { }

    protected override void OnApply(Player player)
    {
        player.ChangeWisdom(7);
        //DisplayManager.GetInstance().DisplayMessageNew("[WisdomElixirEffect] +7 Wisdom.");
    }

    protected override void OnRemove(Player player)
    {
        player.ChangeWisdom(-7);
       // DisplayManager.GetInstance().DisplayMessageNew("[WisdomElixirEffect] -7 Wisdom (effect ended).");
    }
}
public class PudzianElixir : Item
{

    private int _duration;

    public PudzianElixir(int duration = 15)
        : base("Pudzian Elixir")
    {
        signifying = 'P';
        _duration = duration;
    }

    public override void ApplyModifiers(Player player)
    {
        player.Attach(new PudzianElixirEffect(_duration));
    }

    public override void RemoveModifiers(Player player)
    {
        // tu mozna dodac antidotum w razie cos 
    }

}
public class PudzianElixirEffect : TimedElixirObserver
{

    public PudzianElixirEffect(int duration) : base(duration) { }

    protected override void OnApply(Player player)
    {
        
        player.ChangeWisdom(7);
        player.ChangeAggression(6);
        player.ChangeLuck(4);
        player.ChangePower(7);
    }

    protected override void OnRemove(Player player)
    {
        player.ChangeWisdom(-7);
        player.ChangeAggression(-6);
        player.ChangeLuck(-4);
        player.ChangePower(-7);
    }
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


