﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRPG;

public interface IComponent //kategorie broni to komponenty
{
    void Accept(IVisitor visitor);
}
public class HeavyWeapon : IComponent
{ 
    public void Accept(IVisitor visitor)
    {
        visitor.VisitHeavyWeapon(this);
    }
}
public class LightWeapon : IComponent
{
    public void Accept(IVisitor visitor)
    {
        visitor.VisitLightWeapon(this);
    }
}

public class MagicWeapon : IComponent
{
    public void Accept(IVisitor visitor)
    {
        visitor.VisitMagicWeapon(this);
    }
}
public interface IVisitor 
{
    void VisitHeavyWeapon(HeavyWeapon weapon);
    void VisitLightWeapon(LightWeapon weapon);
    void VisitMagicWeapon(MagicWeapon weapon);
    void VisitOtherItem(Item uselessItem);
}

public class CommonAttack : IVisitor
{
    private Player _player;
    private IEnemy _enemy;
    public CommonAttack(Player p,IEnemy e) { _player = p; _enemy = e; }
    private double _playerAttack = 0;
    private double _playerDefence = 0;
    public void VisitHeavyWeapon(HeavyWeapon weapon)
    {
        _playerAttack += (_player.GetAggression + _player.GetPower);
        _playerDefence += (_player.GetPower + _player.GetLuck);
        double damagePlayerGet = Math.Max(_enemy.Attack() - _playerDefence,0);
        double damageEnemyGet = Math.Max(_playerAttack - _enemy.Shield(),0);
        _enemy.ChangeHelath(-damageEnemyGet);
        if (_enemy.IsEnemyDead())
        {
            return;
            
        }
        else
        {
            _player.ChangeHealth(-(int)damagePlayerGet);

        } 
         
    }

    public void VisitLightWeapon(LightWeapon weapon)
    {
        _playerAttack += _player.GetAgility + _player.GetLuck;
        _playerDefence += _player.GetAgility + _player.GetLuck;
        double damagePlayerGet = Math.Max(_enemy.Attack() - _playerDefence, 0);
        double damageEnemyGet = Math.Max(_playerAttack - _enemy.Shield(), 0);
        _enemy.ChangeHelath(-damageEnemyGet);
        if (_enemy.IsEnemyDead())
        {
            return;

        }
        else
        {
            _player.ChangeHealth(-(int)damagePlayerGet);

        }
    }

    public void VisitMagicWeapon(MagicWeapon weapon)
    {
        _playerAttack += _player.GetWisdom;
        _playerDefence += _player.GetAgility + _player.GetLuck;
        double damagePlayerGet = Math.Max(_enemy.Attack() - _playerDefence, 0);
        double damageEnemyGet = Math.Max(_playerAttack - _enemy.Shield(), 0);
        _enemy.ChangeHelath(-damageEnemyGet);
        if (_enemy.IsEnemyDead())
        {
            return;

        }
        else
        {
            _player.ChangeHealth(-(int)damagePlayerGet);

        }

    }

    public void VisitOtherItem(Item uselessItem)
    {
        _playerAttack += 0;
        _playerDefence += _player.GetAgility;
        double damagePlayerGet = Math.Max(_enemy.Attack() - _playerDefence, 0);
        double damageEnemyGet = Math.Max(_playerAttack - _enemy.Shield(), 0);
        _enemy.ChangeHelath(-damageEnemyGet);
        if (_enemy.IsEnemyDead())
        {
            return;

        }
        else
        {
            _player.ChangeHealth(-(int)damagePlayerGet);

        }
        
    }
}

public class SecretAttack : IVisitor
{
    private Player _player;
    private IEnemy _enemy;
    public SecretAttack(Player p, IEnemy e) { _player = p; _enemy = e; }
    private double _playerAttack = 0;
    private double _playerDefence = 0;
    public void VisitHeavyWeapon(HeavyWeapon weapon)
    {
        _playerAttack += (double)(_player.GetAggression + _player.GetPower)*0.5;
        _playerDefence += _player.GetPower;
        double damagePlayerGet = Math.Max(_enemy.Attack() - _playerDefence, 0);
        double damageEnemyGet = Math.Max(_playerAttack - _enemy.Shield(), 0);
        _enemy.ChangeHelath(-damageEnemyGet);
        if (_enemy.IsEnemyDead())
        {
            return;

        }
        else
        {
            _player.ChangeHealth(-(int)damagePlayerGet);

        }
    }

    public void VisitLightWeapon(LightWeapon weapon)
    {
        _playerAttack += (_player.GetAgility + _player.GetLuck)*2;
        _playerDefence += _player.GetAgility;
        double damagePlayerGet = Math.Max(_enemy.Attack() - _playerDefence, 0);
        double damageEnemyGet = Math.Max(_playerAttack - _enemy.Shield(), 0);
        _enemy.ChangeHelath(-damageEnemyGet);
        if (_enemy.IsEnemyDead())
        {
            return;

        }
        else
        {
            _player.ChangeHealth(-(int)damagePlayerGet);

        }
    }

    public void VisitMagicWeapon(MagicWeapon weapon)
    {
        _playerAttack += 1;
        _playerDefence += 0;
        double damagePlayerGet = Math.Max(_enemy.Attack() - _playerDefence, 0);
        double damageEnemyGet = Math.Max(_playerAttack - _enemy.Shield(), 0);
        _enemy.ChangeHelath(-damageEnemyGet);
        if (_enemy.IsEnemyDead())
        {
            return;

        }
        else
        {
            _player.ChangeHealth(-(int)damagePlayerGet);

        }
    }

    public void VisitOtherItem(Item uselessItem)
    {
        _playerAttack += 0;
        _playerDefence += 0;
        double damagePlayerGet = Math.Max(_enemy.Attack() - _playerDefence, 0);
        double damageEnemyGet = Math.Max(_playerAttack - _enemy.Shield(), 0);
        _enemy.ChangeHelath(-damageEnemyGet);
        if (_enemy.IsEnemyDead())
        {
            return;

        }
        else
        {
            _player.ChangeHealth(-(int)damagePlayerGet);

        }
    }
}

public class MagicAttack : IVisitor
{
    private Player _player;
    private IEnemy _enemy;
    public MagicAttack(Player p, IEnemy e) { _player = p; _enemy = e; }
    private double _playerAttack = 0;
    private double _playerDefence = 0;
    public void VisitHeavyWeapon(HeavyWeapon weapon)
    {
        _playerAttack += 1;
        _playerDefence += _player.GetLuck;
        double damagePlayerGet = Math.Max(_enemy.Attack() - _playerDefence, 0);
        double damageEnemyGet = Math.Max(_playerAttack - _enemy.Shield(), 0);
        _enemy.ChangeHelath(-damageEnemyGet);
        if (_enemy.IsEnemyDead())
        {
            return;

        }
        else
        {
            _player.ChangeHealth(-(int)damagePlayerGet);

        }
    }

    public void VisitLightWeapon(LightWeapon weapon)
    {
        _playerAttack += 1;
        _playerDefence += _player.GetLuck;
        double damagePlayerGet = Math.Max(_enemy.Attack() - _playerDefence, 0);
        double damageEnemyGet = Math.Max(_playerAttack - _enemy.Shield(), 0);
        _enemy.ChangeHelath(-damageEnemyGet);
        if (_enemy.IsEnemyDead())
        {
            return;

        }
        else
        {
            _player.ChangeHealth(-(int)damagePlayerGet);

        }

    }

    public void VisitMagicWeapon(MagicWeapon weapon)
    {
        _playerAttack += _player.GetWisdom;
        _playerDefence += _player.GetWisdom*2;
        double damagePlayerGet = Math.Max(_enemy.Attack() - _playerDefence, 0);
        double damageEnemyGet = Math.Max(_playerAttack - _enemy.Shield(), 0);
        _enemy.ChangeHelath(-damageEnemyGet);
        if (_enemy.IsEnemyDead())
        {
            return;

        }
        else
        {
            _player.ChangeHealth(-(int)damagePlayerGet);

        }

    }

    public void VisitOtherItem(Item uselessItem)
    {
        _playerAttack += 0;
        _playerDefence += _player.GetLuck;
        double damagePlayerGet = Math.Max(_enemy.Attack() - _playerDefence, 0);
        double damageEnemyGet = Math.Max(_playerAttack - _enemy.Shield(), 0);
        _enemy.ChangeHelath(-damageEnemyGet);
        if (_enemy.IsEnemyDead())
        {
            return;

        }
        else
        {
            _player.ChangeHealth(-(int)damagePlayerGet);

        }

    }
}



public interface IWeapon
{
    //int GetDamage();
    void ApplyModifiersOnWeapon(int x);
    void RemoveModifiersOnWeapon(int x);
}
public abstract class Weapon : Item, IWeapon
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
public class Dagger : Weapon, IComponent
{
    private readonly IComponent _category = new MagicWeapon();
    public Dagger() : base("Dagger", false)
    {
        signifying = 'D';
        Damage = 10;
    }

    public void Accept(IVisitor visitor)
    {
       _category.Accept(visitor);
    }
}
public class LightSword : Sword, IComponent
{
    private readonly IComponent _category = new LightWeapon();

    public LightSword() : base("Light Sword")
    {
        Damage += 2;
        signifying = 'L';
    }
    public void Accept(IVisitor visitor)
    {
        _category.Accept(visitor);
    }
}
public class TwoHandedHeavySword : Sword, IComponent
{
    private readonly IComponent _category = new HeavyWeapon();
    public TwoHandedHeavySword() : base("Two Handed Heavy Sword",true)
    {
        Damage += 10;
        signifying = 'H';
    }

    public void Accept(IVisitor visitor)
    {
        _category.Accept(visitor);
    }
}
