using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRPG;


public abstract class Currency : Item
{
    public int Value { get; protected set; }
    public Currency(string name, int value)
        : base(name, false)
    {
        Value = value;
        signifying = 'C';
    }
   
}

public class Gold : Currency
{
    public Gold() : base("Gold", 8) { signifying = 'G'; }
    public override void OnPickUp(Player player)
    {
        player.ChangeGold(1);
    }
}
public class Coin : Currency
{
    public Coin() : base("Coin", 1) { signifying = 'C'; }
    public override void OnPickUp(Player player)
    {
        player.ChangeCoin(1);
    }
}