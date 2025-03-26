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
    public Item RemoveAtIndex(int index)
    {

        Item item = _items[index];
        _items.RemoveAt(index);
        return item;

    }
    public void WriteItems()
    {
        int k = 1;
        foreach (var item in _items)
        {
            Console.Write($"{k}." + item.GetName() + " ");
            k++;
        }
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
            return _items.First().GetSign();
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
    private char _horizontalFrame;
    private char _verticalFrame;    
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
                // Na start ustaw jakiś znak (np. spację)
                _tiles[i, j] = new Tile(' ');
            }
        }
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
    private void WriteBinds()
    {
        Console.Write("Q-Quit E-Equip T-DropItem G-TakeItemToHand V-ItemFromHandToEq W-S-A-D-moves");
    }
    public void DrawBoard(Player player)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.SetCursorPosition(0, 22);
        Console.Write(new String(' ', 40));
        for (int i = 0; i < H; i++)
        {
            Console.SetCursorPosition(0, i);
            for (int j = 0; j < W; j++)
            {
                if (player.Position.X == i && player.Position.Y == j)
                {
                    //zawsze player pokryje wszystko na co moze isc
                    Console.Write(player.Sign);
                }
                else
                {

                    Console.Write(_tiles[i, j].GetSymbol());

                }

            }
        }
        Console.SetCursorPosition(0, H + 1);
        {
            WriteBinds();
        }
        /* Console.SetCursorPosition(0, 22);
         Console.Write(new String(' ', 20)); */

        Console.SetCursorPosition(0, H + 3);
        {
            Console.Write(new String(' ', 2 * W));
        }
        Console.SetCursorPosition(0, H + 3);
        if (_tiles[player.Position.X, player.Position.Y].IsItem())
        {
            _tiles[player.Position.X, player.Position.Y].WriteItems();
        }
        Console.SetCursorPosition(W + 2, 0);
        player.WriteEquipment(W + 2, 0);
        player.WritePLayer(W + 2, 13 + 3);
        player.WriteHands(W + 2, 13);

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
        /*
        director.GenerateItems();
        director.GenerateModifiedItems();
        director.GenerateModifiedWeapons();
        director.GenerateCurrencies();
        director.GenerateWeapons();
        */
        director.GenerateElixirs();
        Board builtBoard = director.GetBoard();
        this._tiles = builtBoard._tiles;

    }

}
