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
    public char Symbol; // to co sie wyswietla 
    List<Item> _items = new List<Item>(); // lista itemow na danym polu
    public void AddItem(Item item)
    {
        _items.Add(item);
    }

    public Tile(char symbol)
    {
        Symbol = symbol;
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
    public bool IsLegalMove(Point position)
    {
        if (_tiles[position.X, position.Y].Symbol == _horizontalFrame ||
            _tiles[position.X, position.Y].Symbol == _verticalFrame ||
            _tiles[position.X, position.Y].Symbol == '█')
            return false;
        return true;
    }
    
    public void DrawBoard(Player player)
    {
        Console.OutputEncoding = Encoding.UTF8;
        for (int i = 0; i < H; i++)
        {
            Console.SetCursorPosition(0, i);
            for (int j = 0; j < W; j++)
            {
                if(player.Position.X == i && player.Position.Y == j)
                {
                    //zawsze player pokryje wszystko na co moze isc
                    Console.Write(player.Sign);
                }
                else
                {
                    // tutaj jak dodam itemy to dodam zeby pierwszy item ze stosu wyswietlac
                    Console.Write(_tiles[i, j].Symbol);

                }
            }
        }
    }
    public void InitializeBoard()
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
                    if (random.Next() % 4 == 0)
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
