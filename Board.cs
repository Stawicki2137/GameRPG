using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace GameRPG;
public class Tile // kazde pole bedzie symbolizowane przez taki tile 
{
    private char _symbol; // to co sie wyswietla 
    private List<Item> _items = new List<Item>(); // lista itemow na danym polu
   
    public void WriteItems()
    {
        int k = 1;
        foreach (var item in _items)
        {
            Console.Write($"{k}."+item.Name + " ");
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
            return _items.First().signifying;
        }
        else
        { return _symbol; }
    }
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
    /*
     * plan jest taki:
     * itemasy beda do tile wrzucane, ewnetualni przeciwnicy tez a player bedzie chodzil se jako char po prostu jego
     * pozycje sie bedzie pamietac u niego i go wyswietlac wtedy jak najade na przedmiot wyswietlam playera ale moge brac normlanie
     * przemdioty co leza bez kombinacji 
     */
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
        Console.Write("Q-Quit  W-S-A-D - moves");
    }
    public void DrawBoard(Player player)
    {
        Console.OutputEncoding = Encoding.UTF8;
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

        Console.SetCursorPosition(0, H + 3);
        {
            Console.Write(new String(' ', W+3));
        }
        Console.SetCursorPosition(0, H + 3);
        if (_tiles[player.Position.X, player.Position.Y].IsItem())
        {
            _tiles[player.Position.X, player.Position.Y].WriteItems();
        }


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
        AddItem(new Point(3, 6), new Gold());
        AddItem(new Point(6, 4), new TwoHandedHeavySword());
        AddItem(new Point(6, 4), new Log());


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
                    if (random.Next() % 5 == 0)
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
