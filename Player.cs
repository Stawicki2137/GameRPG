using System;
using System.Collections.Generic;
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
    public Player(Board board/*gracz musi byc przypisany do planszy*/, Point position = default(Point))
    {
        Position = position;
        _board = board;
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
    public void DropItem()
    {
        if (_equipment.Count > 0)
        {
            _board.AddItem(Position, _equipment[0]);
            _equipment.RemoveAt(0);
        }
    }
    public bool PickItem()
    {
        if (_board.IsItem(Position))
        {
            if (_board.ItemCount(Position) == 1)
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
                        _equipment.Add(_board.RemoveAtIndex(Position,0));
                       

                        break;
                    case ConsoleKey.D2:
                        _equipment.Add(_board.RemoveAtIndex(Position, 1));
                        break;
                    case ConsoleKey.D3:
                        _equipment.Add(_board.RemoveAtIndex(Position, 2));
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
/*
public struct Point
{
    public int X;
    public int Y;
    public Point(int x = 0, int y = 0)
    {
        X = x;
        Y = y;
    }
}
public class Player
{
    private string _playerName;
    private int _strength;
    private int _dexterity;
    private int _health;
    private int _luck;
    private int _aggression;
    private int _wisdom;

    // private Item _leftHand;


    private Board _board;
    public Player(string name, Board board)
    {
        _playerName = name;
        _board = board;
    }
    private Point _position = new Point(1, 1);
    private Point _prevPos = new Point(1, 1);
    public Point GetPosition => _position;
    public Point GetPrevPosition => _prevPos;

    public bool IsPlayerOnItem()
    {
        Console.SetCursorPosition(0, _board.GetH + 1);

        if (_board.IsItem(_position))
        {
            foreach (var item in _board.GetItems(_position))
            {
                Console.Write(item.signifying + "-" + item.Name + " ");
            }
            return true;
        }
        else
        {
            Console.SetCursorPosition(0, _board.GetH + 1);
            Console.Write("                                    ");
            return false;
        }

    }
    public void ThrowItemFromEq(int index)
    {

    }
    private List<Item> _equipment = new List<Item>();
    public void PrintEquipment()
    {
        Console.SetCursorPosition(_board.GetW, 0);
        Console.WriteLine("Equipment");
        int i = 1;
        foreach (var item in _equipment)
        {
            Console.SetCursorPosition(_board.GetW, i);
            Console.Write($"{i} - " + item.Name);
            i++;
        }
    }
    public void PickItem()
    {
        if (_isItem)
        {
            // if (_board.GetCountOfItems(_position) == 0)

            var item = _board.GetItems(_position).First();
            // TO DO wybor itemaska ktory podniose w przypadku jak lezy wiecej itemasow?
            _equipment.Add(item);
            _board.PickItem(_position, item);
            Console.SetCursorPosition(0, _board.GetH);
            Console.Write("Item picked up");
        }
        else
        {
            Console.SetCursorPosition(0, _board.GetH);
            Console.Write("Nothing to pick up!");
        }
    }
    private bool _isItem = false;
    public void Move(int dx, int dy)
    {
        Point newPosition = new Point(_position.X + dx, _position.Y + dy);
        if (!IsLegalMove(newPosition))
            return;
        _prevPos = _position;
        _position.X += dx;
        _position.Y += dy;
        _isItem = IsPlayerOnItem();

    }
    private bool IsLegalMove(Point newPosition)
    {
        if (_board.IsLegalMove(newPosition))
            return true;
        return false;
    }
}
*/