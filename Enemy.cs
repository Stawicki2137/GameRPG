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
    double Attack();
    double Shield();
    double Health();
    void ChangeHelath(double h);
    bool IsEnemyDead();
}
public abstract class Enemy : IEnemy
{
    public virtual ConsoleColor GetColor()
    {
        return ConsoleColor.DarkBlue;
    }
    private string _name;
    private char _sign;

    private double _health;
    private double _attack;
    private double _shield;
    

    
   
    public Enemy(string name, double attack,double health=20, double shield=5,char sign='E')
    {
        _name = name;
        _sign = sign;
        _attack = attack;
        _shield = shield;
        _health = health;
    }

    public string GetName()
    { 
        return _name;
    }

    public char GetSign()
    {
       return _sign;
    }

    double IEnemy.Attack()
    {
        return _attack;
    }

    double IEnemy.Shield()
    {
        return _shield;
    }

    double IEnemy.Health()
    {
        return _health;
    }

    public void ChangeHelath(double h)
    {
        _health += h;
        if(_health < 0) 
            _health = 0;
    }
    public bool IsEnemyDead()
    {
        return _health == 0;
    }
}
public class Goblin : Enemy
{
    public Goblin(string name = "Goblin", double attack = 10,char sign = 'G') : base(name, sign)
    {
        
    }
}
public class Ork : Enemy
{
    public Ork(string name = "Ork", double attack = 15,char sign = 'O') : base(name, sign)
    {
    }
}