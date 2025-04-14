using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameRPG;

public class Tile
{
    public char _symbol;
    private List<Item> _items = new List<Item>();
    private IEnemy? _enemy = null;
    public bool IsEnemy() => _enemy != null;
    public Item RemoveAtIndex(int index)
    {

        Item item = _items[index];
        _items.RemoveAt(index);
        return item;

    }
    public void WriteItems()
    {
        int k = 1;
        if (_enemy != null)
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write(_enemy.GetName() + " ");
            Console.ResetColor();
        }
        foreach (var item in _items)
        {
            Console.Write($"{k}." + item.GetName() + " ");
            k++;
        }
    }
    public IEnemy? GetEnemy() => _enemy;
    public bool AddEnemy(IEnemy enemy)
    {
        if (_enemy == null)
        {
            _enemy = enemy;
            return true;
        }
        return false;
    }
    public Item Remove()
    {
        Item item = _items[0];
        _items.RemoveAt(0);
        return item;
    }
    public bool AddItem(Item item)
    {
        if (_symbol == ' ')
        {
            _items.Add(item);
            return true;
        }
        return false;
    }
    public char GetSymbol()
    {
        if (IsItem())
        {
            if (_enemy == null)
                return _items.First().GetSign();
            else
                return _enemy.GetSign();
        }
        else if (_enemy != null)
        {
            return _enemy.GetSign();

        }
        else
        { return _symbol; }
    }
    public int ItemCount => _items.Count;

    public bool IsItem()
    {
        return _items.Count > 0;
    }
    public Tile(char symbol)
    {
        _symbol = symbol;
    }
}
public class Board
{
    private readonly int H;
    private readonly int W;
    public Tile[,] _tiles;
    public StringBuilder _help = new StringBuilder();
    public StringBuilder _binds = new StringBuilder();
    private char _horizontalFrame;
    private char _verticalFrame;
    public StringBuilder GetHelp => _help;
    public int GetH => H;
    public int GetW => W;
    public Board(int height = 20, int width = 40, char horizontalFrame = '-', char verticalFrame = '|')
    {
        H = height + 2; //+2 for frames
        W = width + 2;
        _tiles = new Tile[H, W];
        _horizontalFrame = horizontalFrame;
        _verticalFrame = verticalFrame;
        for (int i = 0; i < H; i++)
        {
            for (int j = 0; j < W; j++)
            {
                _tiles[i, j] = new Tile(' ');
            }
        }
        _help.AppendLine(new String('-', 40));
        _help.AppendLine("MOVE CONTROLS:");
        _help.AppendLine("W - move forward");
        _help.AppendLine("S - move backward");
        _help.AppendLine("A - move left");
        _help.AppendLine("D - move right");
        _binds.Append("Q-Quit H-Help W-S-A-D-moves");



    }
    public bool IsEnemy(Point position)
    {
        return GetEnemy(position) != null;
    }
    public IEnemy? GetEnemy(Point position)
    {
        return _tiles[position.X, position.Y].GetEnemy();
    }
    public int ItemCount(Point position)
    {
        return _tiles[position.X, position.Y].ItemCount;
    }
    public bool IsItem(Point position)
    {
        return _tiles[position.X, position.Y].IsItem();
    }
    public Item RemoveAtIndex(Point position, int index)
    {
        return _tiles[position.X, position.Y].RemoveAtIndex(index);

    }
    public Item Remove(Point position)
    {
        return _tiles[position.X, position.Y].Remove();
    }
    public bool AddItem(Point position, Item item)
    {
        return _tiles[position.X, position.Y].AddItem(item);
    }
    public bool IsLegalMove(Point position)
    {
        if (_tiles[position.X, position.Y].GetSymbol() == _horizontalFrame ||
            _tiles[position.X, position.Y].GetSymbol() == _verticalFrame ||
            _tiles[position.X, position.Y].GetSymbol() == '█')
            return false;
        return true;
    }
    public void WriteBinds()
    {
        Console.Write(_binds.ToString());
    }

    public void StartGame()
    {
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        var builder = new DefaultMazeBuilder();
        var director = new Director(builder);

        director.BuildFilledDungegon();
        //director.BuildEmptyDungegon();

        director.AddCentralRoom();
        director.AddRandomPaths();
        director.AddChamber();
        director.GenerateItems(1);

        
        director.GenerateEnemies();
        director.GenerateItems();
        director.GenerateModifiedItems();
        director.GenerateModifiedWeapons();
        director.GenerateCurrencies();
        director.GenerateWeapons();
        director.GenerateElixirs();
        

        Board builtBoard = director.GetBoard();
        this._binds = builtBoard._binds;
        this._help = builtBoard.GetHelp;
        this._tiles = builtBoard._tiles;

    }

}
