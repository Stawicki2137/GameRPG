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
    private char _symbol;
    List<Item> _items = new List<Item>();
    public void AddItem(Item item)
    {
        _items.Add(item);
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
    private char[,] _board;
    private Dictionary<Point, List<Item>> _itemsOnBoard = new Dictionary<Point, List<Item>>();  // itema
    public Board(int height = 20, int width = 40)
    {
        H = height + 2; //+2 for frames
        W = width + 2;
        _board = new char[H, W];
    }
    public int GetH => H + 2;
    public int GetW => W + 2;
    public int GetCountOfItems(Point position) => _itemsOnBoard[position].Count;
    public List<Item> GetItems(Point position)
    {
        return _itemsOnBoard[position];
    }
    public void PickItem(Point position, Item item)
    {
        if (_itemsOnBoard.ContainsKey(position))
        {
            _itemsOnBoard[position].Remove(item); // po prostu usune niech ta lista tam bedzie i ta pozycja w 
            //slowniku bo przeciez nie szkodzi a moze sie przydac
        }
    }
    public bool IsItem(Point point) //czy w danym punkcie jest item
    {
        foreach (var item in _itemsOnBoard.Keys)
        {
            if (item.X == point.X && item.Y == point.Y)
            {
                return true;
            }
        }
        return false;
    }
    public void AddItem(Point point, Item item) // dodaje itemaska
    {
        if (!_itemsOnBoard.ContainsKey(point))
        {
            _itemsOnBoard.Add(point, new List<Item>());
        }
        _itemsOnBoard[point].Add(item);
    }
    public bool IsLegalMove(Point position)
    {
        if (_itemsOnBoard.ContainsKey(position) && _itemsOnBoard[position].FirstOrDefault() == null)
            return true;
        if (_board[position.X, position.Y] == '█' ||
            position.X == 0 || position.Y == 0 ||
            position.X == H - 1 || position.Y == W - 1)
            return false;
        return true;
    }
    public void DrawBoard(Player player)
    {
        Console.OutputEncoding = Encoding.UTF8;
        player.PrintEquipment();
        Console.SetCursorPosition(0, 0);
        for (int i = 0; i < H; i++)
        {
            for (int j = 0; j < W; j++)
            {
                if (i == 0 || i == H - 1)
                {
                    Console.Write('-');
                }
                else if (j == 0 || j == W - 1)
                {
                    Console.Write("|");
                }
                else
                {
                    char positionMark = _board[i, j];
                    if (IsItem(new Point(i, j)) && positionMark != '¶') //zeby zawsze nad itemaskiem player byl
                    {
                        var item = _itemsOnBoard[new Point(i, j)].FirstOrDefault();
                        if (item != null)
                        {
                            positionMark = item.signifying;

                        }
                    }
                    Console.Write(positionMark);
                }
            }
            Console.WriteLine();
        }
        Console.SetCursorPosition(0, H);
        Console.Write("Q-quit E-pick up");
        Console.SetCursorPosition(0, H + 1);



    }

    public void UpdateBoard(Player player)
    {
        _board[player.GetPrevPosition.X, player.GetPrevPosition.Y] = ' ';
        _board[player.GetPosition.X, player.GetPosition.Y] = '¶';
    }

    public void Initialize()
    {
        Random rand = new Random();
        for (int i = 0; i < H; i++)
        {
            for (int j = 0; j < W; j++)
            {
                int number = rand.Next();
                if (number % 4 == 2)
                {
                    _board[i, j] = '█';
                }
                else
                {
                    _board[i, j] = ' ';
                }

            }
        }
        AddItem(new Point(3, 3), new HollowShield());
        AddItem(new Point(5, 5), new TwoHandedHeavySword());
        AddItem(new Point(10, 7), new Coin());
        AddItem(new Point(5,5), new Coin());

    }
}

