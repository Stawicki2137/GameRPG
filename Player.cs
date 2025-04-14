using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;

namespace GameRPG;
public struct Point
{
    public int X;
    public int Y;
    public Point(int x = 1, int y = 1)
    {
        X = x;
        Y = y;
    }
}
public class Player : ISubject
{
    private List<IObserver> _observers = new();

    public void Attach(IObserver observer) => _observers.Add(observer);
    public void Detach(IObserver observer) => _observers.Remove(observer);

    public void Notify()
    {
        foreach (var observer in _observers.ToList())
        {
            observer.Update(this);
        }
    }

    public void NextTurn()
    {
        playerMoves++;
        Notify();
    }
    public Point Position;
    private Board _board;
    public char Sign = '¶';
    public List<Item> _equipment = new List<Item>();
    public int playerMoves = 0;

    private Item? _leftHand;
    private Item? _rightHand;
    public bool IsLeftHandNull => _leftHand == null;
    public bool IsRightHandNull => _rightHand == null;
    public string LeftHandItemGetName => _leftHand.GetName();
    public string RighttHandItemGetName => _rightHand.GetName();

    // player attr
    private string _name;
    private int _power;
    private int _agility;
    private int _health;
    private int _luck;
    private int _aggression;
    private int _wisdom;
    private int _eqCapacity;
    public int GetWisdom => _wisdom;
    public int GetAggression => _aggression;
    public int GetHealth => _health;
    public int GetLuck => _luck;
    public int GetAgility => _agility;
    public int GetPower => _power;
    public string GetName() => _name;
    public int GetCoinNumber => _coinNumber;
    public int GetGoldNumber => _goldNumber;

    private int _coinNumber = 0;
    private int _goldNumber = 0;
    public void ChangeGold(int amount) => _goldNumber += amount;
    public void ChangeCoin(int amount) => _coinNumber += amount;

    // end 
    public Player(Board board/*gracz musi byc przypisany do planszy*/, Point position, String name = "Hero 1",
        int power = 3, int agility = 6, int health = 10, int luck = 5, int wisdom = 3, int aggression = 3, int eqCapacity = 9)
    {
        Position = position;
        _board = board;
        _name = name;

        _power = power;
        _agility = agility;
        _health = health;
        _luck = luck;
        _wisdom = wisdom;
        _aggression = aggression;
        _eqCapacity = eqCapacity;
    }
    public void ChangeAll(int amount)
    {
        _power += amount;
        _agility += amount;
        _health += amount;
        _luck += amount;
        _aggression += amount;
        _wisdom += amount;

    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <returns>0-empty hands, -1-empty hand, 2-2handAttackImpossible  
    /// 3-badHandChoose 4- bad attack type 1-success 5- no enemy to fight 6-no attack done</returns>
    /// 
    public int Fight()
    {
        if (!_board.IsEnemy(Position)) return 5;
        if (IsLeftHandNull && IsRightHandNull) return 0;
        DisplayManager.GetInstance().DisplayMessage("Select hand (R - Right, L - Left - BothHands): ");
        IComponent? weaponToAttack = null;
        IVisitor? attackVisitor = null;
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.R:
                {

                    if (IsRightHandNull) return -1;
                    DisplayManager.GetInstance().DisplayMessage("Select attack type: 1-CommonAttack 2-SecretAttack 3-MagicAttack");

                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.D1:
                            { 
                                weaponToAttack = _rightHand as IComponent;
                                attackVisitor = new CommonAttack(this, _board.GetEnemy(Position)!);
                                DisplayManager.GetInstance().DisplayMessage("U have just attacked an enemy");

                                if (weaponToAttack != null)
                                {
                                    weaponToAttack.Accept(attackVisitor);
                                    if (_board.GetEnemy(Position).IsEnemyDead())
                                    {
                                        DisplayManager.GetInstance().DisplayMessage("Enemy is dead");
                                        _board.RemoveEnemy(Position);
                                    }
                                    else
                                    {
                                        DisplayManager.GetInstance().DisplayMessage("You are killed! GAME OVER");

                                    }
                                    return 1;
                                }
                                return 6;
                                break;
                            }
                        case ConsoleKey.D2:
                            {
                                break;
                            }
                        case ConsoleKey.D3:
                            {
                                break;
                            }
                        default:
                            {
                                return 4;
                            }
                    }
                    break;

                }
            case ConsoleKey.L:
                {
                    if(IsLeftHandNull) return -1;
                    DisplayManager.GetInstance().DisplayMessage("Select attack type: 1-CommonAttack 2-SecretAttack 3-MagicAttack");

                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.D1:
                            {
                                break;
                            }
                        case ConsoleKey.D2:
                            {
                                break;
                            }
                        case ConsoleKey.D3:
                            {
                                break;
                            }
                        default:
                            {
                                return 4;
                            }
                    }
                    break;
                }
            case ConsoleKey.B:
                {
                    if (IsLeftHandNull || IsRightHandNull) return 2;
                    switch (Console.ReadKey().Key)
                    {
                        case ConsoleKey.D1:
                            {
                                break;
                            }
                        case ConsoleKey.D2:
                            {
                                break;
                            }
                        case ConsoleKey.D3:
                            {
                                break;
                            }
                        default:
                            {
                                return 4;
                            }
                    }
                    break;
                }
             default:
                return 3;
               


        }

        return 1;
    }
    public void ChangeAggression(int aggression) { _aggression += aggression; }
    public void ChangeHealth(int health) { _health += health; }
    public void ChangeAgility(int agility) { _agility += agility; }
    public void ChangeLuck(int luck) { _luck += luck; }
    public void ChangeWisdom(int wisdom) { _wisdom += wisdom; }
    public void ChangePower(int power) { _power += power; }
    public bool IsPlayerDead => _health <= 0;
    public void Move(int x, int y)
    {
        Point newPosition = new Point(Position.X + x, Position.Y + y);
        if (_board.IsLegalMove(newPosition))
        {
            Position = newPosition;
            NextTurn();
            DisplayManager.GetInstance().DisplayGameState(_board, this);

        }
    }

    public bool MoveItemFromHandToEq()
    {
        DisplayManager.GetInstance().DisplayMessage("Select hand (R - Right, L - Left): ");

        ref Item? hand = ref _rightHand;
        if (_rightHand == null && _leftHand == null) return false;
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.R:
                hand = ref _rightHand;

                if (hand == null) return false;
                if (hand.NeedsTwoArms)
                {
                    if (_equipment.Count >= _eqCapacity) return false; // eq limit cap
                    ;
                    _equipment.Add(hand);
                    hand.RemoveModifiers(this);
                    _leftHand = null;
                    _rightHand = null;
                    return true;
                }
                break;
            case ConsoleKey.L:
                hand = ref _leftHand;
                if (hand == null) return false;
                if (hand.NeedsTwoArms)
                {
                    if (_equipment.Count >= _eqCapacity) return false; // eq limit cap
                    _equipment.Add(hand);
                    hand.RemoveModifiers(this);
                    _leftHand = null;
                    _rightHand = null;
                    return true;
                }
                break;
            default:
                return false;
        }
        if (_equipment.Count >= _eqCapacity) return false; // eq limit cap
        _equipment.Add(hand);
        hand.RemoveModifiers(this);
        hand = null;
        return true;
    }
    public bool MoveItemFromEqToHand()
    {

        DisplayManager.GetInstance().DisplayMessage("Select hand (R - Right, L - Left): ");
        if (_equipment.Count == 0) return false;
        ref Item? hand = ref _rightHand;
        bool bothHandsEmpty = true;
        if (_leftHand != null || _rightHand != null) // jesli ktoras jest nie pusta to obie nie sa puste
            bothHandsEmpty = false;

        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.R:
                hand = ref _rightHand;
                if (hand != null) return false;
                break;
            case ConsoleKey.L:
                hand = ref _leftHand;
                if (hand != null) return false;
                break;
            default:
                return false;
        }
        int count = _equipment.Count;
        DisplayManager.GetInstance().DisplayMessage("Select item to take:");

        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.D1:
                if (count < 1) return false;
                if (_equipment[0].NeedsTwoArms)
                {
                    if (!bothHandsEmpty) return false;
                    _leftHand = _equipment[0];
                    _rightHand = _equipment[0];
                    _equipment.RemoveAt(0);
                    return true;
                }
                hand = _equipment[0];
                if (hand.ApplyOnEquip())
                {
                    hand.ApplyModifiers(this);
                }
                _equipment.RemoveAt(0);
                break;
            case ConsoleKey.D2:
                if (count < 2) return false;
                if (_equipment[1].NeedsTwoArms)
                {
                    if (!bothHandsEmpty) return false;
                    _leftHand = _equipment[1];
                    _rightHand = _equipment[1];
                    _equipment.RemoveAt(1);
                    return true;
                }
                hand = _equipment[1];
                if (hand.ApplyOnEquip())
                {
                    hand.ApplyModifiers(this);
                }
                _equipment.RemoveAt(1);
                break;
            case ConsoleKey.D3:
                if (count < 3) return false;
                if (_equipment[2].NeedsTwoArms)
                {
                    if (!bothHandsEmpty) return false;
                    _leftHand = _equipment[2];
                    _rightHand = _equipment[2];
                    _equipment.RemoveAt(2);
                    return true;
                }
                hand = _equipment[2];
                if (hand.ApplyOnEquip())
                {
                    hand.ApplyModifiers(this);
                }
                _equipment.RemoveAt(2);
                break;
            case ConsoleKey.D4:
                if (count < 4) return false;
                if (_equipment[3].NeedsTwoArms)
                {
                    if (!bothHandsEmpty) return false;
                    _leftHand = _equipment[3];
                    _rightHand = _equipment[3];
                    _equipment.RemoveAt(3);
                    return true;
                }
                hand = _equipment[3];
                if (hand.ApplyOnEquip())
                {
                    hand.ApplyModifiers(this);
                }
                _equipment.RemoveAt(3);
                break;
            case ConsoleKey.D5:
                if (count < 5) return false;
                if (_equipment[4].NeedsTwoArms)
                {
                    if (!bothHandsEmpty) return false;
                    _leftHand = _equipment[4];
                    _rightHand = _equipment[4];
                    _equipment.RemoveAt(4);
                    return true;
                }
                hand = _equipment[4];
                if (hand.ApplyOnEquip())
                {
                    hand.ApplyModifiers(this);
                }
                _equipment.RemoveAt(4);
                break;
            case ConsoleKey.D6:
                if (count < 6) return false;
                if (_equipment[5].NeedsTwoArms)
                {
                    if (!bothHandsEmpty) return false;
                    _leftHand = _equipment[5];
                    _rightHand = _equipment[5];
                    _equipment.RemoveAt(5);
                    return true;
                }
                hand = _equipment[5];
                if (hand.ApplyOnEquip())
                {
                    hand.ApplyModifiers(this);
                }
                _equipment.RemoveAt(5);
                break;
            case ConsoleKey.D7:
                if (count < 7) return false;
                if (_equipment[6].NeedsTwoArms)
                {
                    if (!bothHandsEmpty) return false;
                    _leftHand = _equipment[6];
                    _rightHand = _equipment[6];
                    _equipment.RemoveAt(6);
                    return true;
                }
                hand = _equipment[6];
                if (hand.ApplyOnEquip())
                {
                    hand.ApplyModifiers(this);
                }
                _equipment.RemoveAt(6);
                break;
            case ConsoleKey.D8:
                if (count < 8) return false;
                if (_equipment[7].NeedsTwoArms)
                {
                    if (!bothHandsEmpty) return false;
                    _leftHand = _equipment[7];
                    _rightHand = _equipment[7];
                    _equipment.RemoveAt(7);
                    return true;
                }
                hand = _equipment[7];
                if (hand.ApplyOnEquip())
                {
                    hand.ApplyModifiers(this);
                }
                _equipment.RemoveAt(7);
                break;
            case ConsoleKey.D9:
                if (count < 9) return false;
                if (_equipment[8].NeedsTwoArms)
                {
                    if (!bothHandsEmpty) return false;
                    _leftHand = _equipment[8];
                    _rightHand = _equipment[8];
                    _equipment.RemoveAt(8);
                    return true;
                }
                hand = _equipment[8];
                if (hand.ApplyOnEquip())
                {
                    hand.ApplyModifiers(this);
                }
                _equipment.RemoveAt(8);
                break;
            default:
                return false;
        }
        return true;


    }
    public int DrinkElixir()
    {
        if (_leftHand == null && _rightHand == null)
        {
            return -1;
        }
        else
        {
            ref Item? hand = ref _rightHand;
            DisplayManager.GetInstance().DisplayMessage("Choose arm (R-right L-Left)");
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.R:
                    hand = ref _rightHand;
                    if (hand == null) return -1;
                    break;
                case ConsoleKey.L:
                    hand = ref _leftHand;
                    if (hand == null) return -1;
                    break;
                default:
                    return -1;
            }
            if (hand.IsUsed) return 0;
            if (hand.ApplyOnEquip() == false)
            {
                hand.ApplyModifiers(this);
                NextTurn();

                return 1;
            }
            return -1;
        }
    }

    public bool DropItem()
    {
        int count = _equipment.Count();

        if (count > 0)
        {
            if (count == 1)
            {
                _board.AddItem(Position, _equipment[0]);
                _equipment.RemoveAt(0);
                return true;
            }
            else
            {
                DisplayManager.GetInstance().DisplayMessage("Select item:");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        if (count < 1) return false;
                        _board.AddItem(Position, _equipment[0]);
                        _equipment.RemoveAt(0);
                        break;
                    case ConsoleKey.D2:
                        if (count < 2) return false;
                        _board.AddItem(Position, _equipment[1]);
                        _equipment.RemoveAt(1);
                        break;
                    case ConsoleKey.D3:
                        if (count < 3) return false;
                        _board.AddItem(Position, _equipment[2]);
                        _equipment.RemoveAt(2);
                        break;
                    case ConsoleKey.D4:
                        if (count < 4) return false;
                        _board.AddItem(Position, _equipment[3]);
                        _equipment.RemoveAt(3);
                        break;
                    case ConsoleKey.D5:
                        if (count < 5) return false;
                        _board.AddItem(Position, _equipment[4]);
                        _equipment.RemoveAt(4);
                        break;
                    case ConsoleKey.D6:
                        if (count < 6) return false;
                        _board.AddItem(Position, _equipment[5]);
                        _equipment.RemoveAt(5);
                        break;
                    case ConsoleKey.D7:
                        if (count < 7) return false;
                        _board.AddItem(Position, _equipment[6]);
                        _equipment.RemoveAt(6);
                        break;
                    case ConsoleKey.D8:
                        if (count < 8) return false;
                        _board.AddItem(Position, _equipment[7]);
                        _equipment.RemoveAt(7);
                        break;
                    case ConsoleKey.D9:
                        if (count < 9) return false;
                        _board.AddItem(Position, _equipment[8]);
                        _equipment.RemoveAt(8);
                        break;
                    default:
                        return false;
                }
                return true;
            }
        }
        return false;


    }
    public bool PickItem()
    {
        if (_board.IsItem(Position))
        {

            if (_equipment.Count >= _eqCapacity) return false; // limit eq capacity
            int count = _board.ItemCount(Position);
            if (count == 1)
            {
                _board.Remove(Position).OnPickUp(this);
                return true;
            }
            else
            {

                DisplayManager.GetInstance().DisplayMessage("Select item:");

                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        if (count < 1) return false;
                        _board.RemoveAtIndex(Position, 0).OnPickUp(this);
                        break;
                    case ConsoleKey.D2:
                        if (count < 2) return false;
                        _board.RemoveAtIndex(Position, 1).OnPickUp(this);
                        break;
                    case ConsoleKey.D3:
                        if (count < 3) return false;
                        _board.RemoveAtIndex(Position, 2).OnPickUp(this);
                        break;
                    case ConsoleKey.D4:
                        if (count < 4) return false;
                        _board.RemoveAtIndex(Position, 3).OnPickUp(this);
                        break;
                    case ConsoleKey.D5:
                        if (count < 5) return false;
                        _board.RemoveAtIndex(Position, 4).OnPickUp(this);
                        break;
                    case ConsoleKey.D6:
                        _board.RemoveAtIndex(Position, 5).OnPickUp(this);
                        if (count < 6) return false;
                        break;
                    case ConsoleKey.D7:
                        if (count < 7) return false;
                        _board.RemoveAtIndex(Position, 6).OnPickUp(this);
                        break;
                    case ConsoleKey.D8:
                        if (count < 8) return false;
                        _board.RemoveAtIndex(Position, 7).OnPickUp(this);
                        break;
                    case ConsoleKey.D9:
                        if (count < 9) return false;
                        _board.RemoveAtIndex(Position, 8).OnPickUp(this);
                        break;
                    default:
                        return false;
                }
                return true;
            }
        }
        return false;

    }
}
