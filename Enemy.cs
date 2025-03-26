using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRPG;

public interface IEnemy
{
    string GetName();
    char GetSign();
}
public class Enemy : IEnemy
{
    private string _name;
    private char _sign;
    public Enemy(string name, char sign='E')
    {
        _name = name;
        _sign = sign;
    }

    public string GetName()
    { 
        return _name;
    }

    public char GetSign()
    {
       return _sign;
    }
}
public class Goblin : Enemy
{
    public Goblin(string name = "Goblin", char sign = 'G') : base(name, sign)
    {
    }
}
public class Ork : Enemy
{
    public Ork(string name = "Ork", char sign = 'O') : base(name, sign)
    {
    }
}