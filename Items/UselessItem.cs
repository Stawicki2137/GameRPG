using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRPG;

public abstract class UselessItem : Item
{
    protected UselessItem(string name, bool needsTwoArms = false) : base(name, needsTwoArms) { signifying = 'U'; }
}
public class HollowShield : UselessItem
{
    public HollowShield() : base("Hollow Shield")
    {
        signifying = 'H';
    }
}
public class Stone : UselessItem
{
    public Stone() : base("Stone")
    {
        signifying = 'S';
    }
    public override char GetSign() => signifying;
}
public class Log : UselessItem
{
    public Log() : base("Log", true) { signifying = 'L'; }
}
