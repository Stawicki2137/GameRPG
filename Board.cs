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
    private List<Item> _items = new List<Item>();
    // pomysl jeset  zeby das liste na currency i jak bede wchodzil graczem na tile na ktorym lista currency nie 
    // jest pusta to moge podniesc do kiermany od razu

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
    private Tile[,] _tiles; //tiles
    private char _horizontalFrame;
    private char _verticalFrame;
    public Board(int height = 20, int width = 40, char horizontalFrame = '-', char verticalFrame = '|')
    {
        H = height + 2; //+2 for frames
        W = width + 2;
        _tiles = new Tile[H, W];
        _horizontalFrame = horizontalFrame;
        _verticalFrame = verticalFrame;
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
        GenerateItems();
    }
    private void GenerateItems()
    {
        AddItem(new Point(3, 3), new Coin());
        AddItem(new Point(5, 2), new Dagger());
        AddItem(new Point(5, 2), new LuckyDecorator(new Log()));

        AddItem(new Point(15, 30), new LuckyDecorator(new UltraStrong(new LightSword())));

        AddItem(new Point(5, 2), new Damned(new Dagger()));
        AddItem(new Point(3, 6), new Gold());
        AddItem(new Point(6, 5), new TwoHandedHeavySword());
        AddItem(new Point(6, 4), new TwoHandedHeavySword());
        AddItem(new Point(6, 4), new Log());
        AddItem(new Point(9, 5), new LuckyDecorator( new HolyDecorator(new Stone())));
        AddItem(new Point(7, 7), new HollowShield());
        AddItem(new Point(10, 10), new LightSword());
        AddItem(new Point(5, 5), new LightSword());
        AddItem(new Point(3, 3), new Strong(new HolyDecorator(new HollowShield())));
        AddItem(new Point(10, 10), new LuckyDecorator((new LightSword())));
        AddItem(new Point(2,2), new Log());
        AddItem(new Point(2,2), new Strong(new LuckyDecorator(new HolyDecorator(new Dagger()))));
    }
    private void InitializeBoard()
    {
        Random random = new Random();
        for (int i = 0; i < H; i++)
        {
            for (int j = 0; j < W; j++)
            {
                if (i == 0 || i == H - 1)
                {
                    _tiles[i, j] = new Tile(_horizontalFrame);
                }
                else if (j == 0 || j == W - 1)
                {
                    _tiles[i, j] = new Tile(_verticalFrame);
                }
                else
                {
                    if (random.Next() % 8 == 0)
                    {
                        _tiles[i, j] = new Tile('█');
                    }
                    else
                    {
                        _tiles[i, j] = new Tile(' ');
                    }
                }

            }
        }
    }

}
