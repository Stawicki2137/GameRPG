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
    }
}

public class Gold : Currency
{
    public Gold() : base("Gold", 8) { signifying = 'G'; }
}
public class Coin : Currency
{
    public Coin() : base("Coin", 1) { signifying = 'C'; }
}