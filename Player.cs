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
public class Player
{
    public Point Position;
    private Board _board;
    public char Sign = '¶';
    private List<Item> _equipment = new List<Item>();

    private Item? _leftHand;
    private Item? _rightHand;
    // player attr
    private string _name;
    private int _power;
    private int _agility;
    private int _health;
    private int _luck;
    private int _aggression;
    private int _wisdom;
    // end 
    public Player(Board board/*gracz musi byc przypisany do planszy*/, Point position,String name = "Hero 1", 
        int power = 3, int agility = 3, int health = 10, int luck = 5, int wisdom = 3)
    {
        Position = position;
        _board = board;
        _name = name;

        _power = power;
        _agility = agility;
        _health = health;
        _luck = luck;
        _wisdom = wisdom;
    }

    public void Move(int x, int y)
    {
        Point newPosition = new Point(Position.X + x, Position.Y + y);
        if (_board.IsLegalMove(newPosition))
        {
            Position = newPosition;
            _board.DrawBoard(this);
        }
    }
    
    public void WritePLayer(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.SetCursorPosition(x, y);
        Console.Write(new String(' ', 30));
        Console.SetCursorPosition(x, y);
        Console.Write("---" + _name + "---");

        Console.SetCursorPosition(x, y+1);
        Console.Write(new String(' ', 30));
        Console.SetCursorPosition(x, y + 1);
        Console.Write($"Health {_health}");
        Console.SetCursorPosition(x, y + 2);
        Console.Write(new String(' ', 30));
        Console.SetCursorPosition(x, y + 2);
        Console.Write($"Power {_power}");
        Console.SetCursorPosition(x, y + 3);
        Console.Write(new String(' ', 30));
        Console.SetCursorPosition(x, y + 3);
        Console.Write($"Luck {_luck}");
        Console.SetCursorPosition(x, y + 4);
        Console.Write(new String(' ', 30));
        Console.SetCursorPosition(x, y + 4);
        Console.Write($"Wisdom {_wisdom}");
        Console.SetCursorPosition(x, y + 5);
        Console.Write(new String(' ', 30));
        Console.SetCursorPosition(x, y + 5);
        Console.Write($"Agility {_agility}");

        Console.ResetColor();
        


    }

    public void WriteHands(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.SetCursorPosition(x, y);
        Console.Write(new String(' ', 60));
        Console.SetCursorPosition(x, y);
        Console.Write("Left Hand: " + (_leftHand == null ? "empty" : _leftHand.Name));
        Console.SetCursorPosition(x, y+1);
        Console.Write(new String(' ', 60));
        Console.SetCursorPosition(x, y+1);
        Console.Write("Right Hand: " + (_rightHand == null ? "empty" : _rightHand.Name));
        Console.ResetColor();

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="x">horizontal cursor position</param>
    /// <param name="y">vertical cursor postion</param>
    public void WriteEquipment(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("---Equipment---");
        int k = 1;
        for (int i = 1; i < 12; i++)
        {
            Console.SetCursorPosition(x, y + i);
            Console.Write(new String(' ', 50));
        }
        foreach (Item item in _equipment)
        {

            Console.SetCursorPosition(x, y + k);
            Console.Write($"{k}." + item.Name);
            k++;
        }
        Console.ResetColor();
    }
    // TODO Items refactore
    // TODO write pocket
    // TODO currency shoud have special place (pocket for gold and coins not keep them in eq) 
    public bool MoveItemFromHandToEq()
    {
        Console.SetCursorPosition(0, 22);
        Console.Write("Select hand (R - Right, L - Left): ");

        ref Item? hand = ref _rightHand;
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.R:
                hand = ref _rightHand;
                if (hand == null) return false;
                break;
            case ConsoleKey.L:
                hand = ref _leftHand;
                if (hand == null) return false;
                break;
            default:
                return false;
        }
        _equipment.Add(hand);
        hand = null;
        return true;
    }
    public bool MoveItemFromEqToHand()
    {
       
        Console.SetCursorPosition(0, 22);
        Console.Write("Select hand (R - Right, L - Left): ");

        ref Item? hand = ref _rightHand;

        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.R:
                hand = ref _rightHand;
                if(hand != null) return false;
                break;
            case ConsoleKey.L:
                hand = ref _leftHand;
                if (hand != null) return false;
                break;
            default:
                return false; 
        }
        int count = _equipment.Count;
        Console.SetCursorPosition(0, 22);
        Console.Write(new String(' ', 40));
        Console.SetCursorPosition(0, 22);
        Console.Write("Select item to take:");
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.D1:
                if (count < 1) return false;
                hand = _equipment[0];
                _equipment.RemoveAt(0);
                break;
            case ConsoleKey.D2:
                if (count < 2) return false;
                hand = _equipment[1];
                _equipment.RemoveAt(1);
                break;
            case ConsoleKey.D3:
                if (count < 3) return false;
                hand = _equipment[2];
                _equipment.RemoveAt(2);
                break;
            case ConsoleKey.D4:
                if (count < 4) return false;
                hand = _equipment[3];
                _equipment.RemoveAt(3);
                break;
            case ConsoleKey.D5:
                if (count < 5) return false;
                hand = _equipment[4];
                _equipment.RemoveAt(4);
                break;
            case ConsoleKey.D6:
                if (count < 6) return false;
                hand = _equipment[5];
                _equipment.RemoveAt(5);
                break;
            case ConsoleKey.D7:
                if (count < 7) return false;
                hand = _equipment[6];
                _equipment.RemoveAt(6);
                break;
            case ConsoleKey.D8:
                if (count < 8) return false;
                hand = _equipment[7];
                _equipment.RemoveAt(7);
                break;
            case ConsoleKey.D9:
                if (count < 9) return false;
                hand = _equipment[8];
                _equipment.RemoveAt(8);
                break;
            default:
                return false;
        }
        return true;


    }

    public bool DropItem()
    {
        int count = _equipment.Count();

        if (count>0)
        {
            if (count == 1)
            {
                _board.AddItem(Position, _equipment[0]);
                _equipment.RemoveAt(0);
                return true;
            }
            else
            {

                Console.SetCursorPosition(0, 22);
                Console.Write("Select item:");
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
            int count = _board.ItemCount(Position);
            if (count== 1)
            {
                _equipment.Add(_board.Remove(Position));
                return true;
            }
            else
            {

                Console.SetCursorPosition(0, 22);
                Console.Write("Select item:");
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.D1:
                        if (count < 1) return false;
                        _equipment.Add(_board.RemoveAtIndex(Position,0));
                        break;
                    case ConsoleKey.D2:
                        if (count < 2) return false;
                        _equipment.Add(_board.RemoveAtIndex(Position, 1));
                        break;
                    case ConsoleKey.D3:
                        if (count < 3) return false;
                        _equipment.Add(_board.RemoveAtIndex(Position, 2));
                        break;
                    case ConsoleKey.D4:
                        if (count < 4) return false;
                        _equipment.Add(_board.RemoveAtIndex(Position, 3));
                        break;
                    case ConsoleKey.D5:
                        if (count < 5) return false;
                        _equipment.Add(_board.RemoveAtIndex(Position, 4));
                        break;
                    case ConsoleKey.D6:
                        _equipment.Add(_board.RemoveAtIndex(Position, 5));
                        if (count < 6) return false;
                        break;
                    case ConsoleKey.D7:
                        if (count < 7) return false;
                        _equipment.Add(_board.RemoveAtIndex(Position, 6));
                        break;
                    case ConsoleKey.D8:
                        if (count < 8) return false;
                        _equipment.Add(_board.RemoveAtIndex(Position, 7));
                        break;
                    case ConsoleKey.D9:
                        if (count < 9) return false;
                        _equipment.Add(_board.RemoveAtIndex(Position, 8));
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
