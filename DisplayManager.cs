using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GameRPG;
public static class StringBuilderExtensions
{
    public static void RemoveSubstring(this StringBuilder sb, string value)
    {
        int index = sb.ToString().IndexOf(value);
        if (index >= 0)
        {
            sb.Remove(index, value.Length);
        }
    }
}
public sealed class DisplayManager
{
    private DisplayManager() { }
    private static DisplayManager _instance;
    public static DisplayManager GetInstance()
    {
        if(_instance == null)
        {
            _instance = new DisplayManager();
        }
        return _instance;
    }
    public void DisplayHelp(Board board, Player player)
    {
        Console.Clear();
        bool isItem = false;
        for (int i = 0; i < board.GetH; i++)
        {
            for (int j = 0; j < board.GetW; j++)
            {
                if (board.ItemCount(new Point(i, j))!=0)

                {
                    isItem = true;
                    break;
                }

            }
        }
        if (!isItem)
        {
            board._binds.RemoveSubstring("E-Equip");
            board._help.RemoveSubstring("E - Pick up item");
        }
        Console.Write(board.GetHelp.ToString());
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nPress \'H\' to back to game");
        Console.ResetColor();
        Console.WriteLine(new String('-', 40));


        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.H:
                DisplayManager.GetInstance().DisplayGameState(board, player);
                break;
        }
    }
    public void DisplayMoves(Player player) { 
    }
    public void DisplayMessage(string message)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.SetCursorPosition(0, 22);
        Console.Write(new String(' ', 45));
        Console.SetCursorPosition(0, 22);
        Console.Write(message);
    }
    public void DisplayElixirs(string message)
    {
       int y = 13+2;
       int x = 42 + 33;
        Console.SetCursorPosition(x, y);
        Console.Write(new string(' ', 20));
        Console.SetCursorPosition(x, y);
        Console.Write(message);
    }

    public void DisplayGameState(Board board, Player player)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.SetCursorPosition(0, 22);
        Console.Write(new String(' ', 45));
        for (int i = 0; i < board.GetH; i++)
        {
            Console.SetCursorPosition(0, i);
            for (int j = 0; j < board.GetW; j++)
            {
                if (player.Position.X == i && player.Position.Y == j)
                {
                    //zawsze player pokryje wszystko na co moze isc
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(player.Sign);
                    Console.ResetColor();
                }
                else
                {
                    if(board._tiles[i, j].IsEnemy())
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(board._tiles[i, j].GetSymbol());
                        Console.ResetColor();
                    }
                    else 
                    Console.Write(board._tiles[i, j].GetSymbol());
                }

            }
        }
        Console.SetCursorPosition(0, board.GetH+ 1);
        {
            board.WriteBinds();
        }
        /* Console.SetCursorPosition(0, 22);
         Console.Write(new String(' ', 20)); */

        Console.SetCursorPosition(0, board.GetH + 3);
        {
            Console.Write(new String(' ', 2 * board.GetW));
        }
        Console.SetCursorPosition(0, board.GetH + 3);
        if (board._tiles[player.Position.X, player.Position.Y].IsItem() ||
            board._tiles[player.Position.X, player.Position.Y].IsEnemy())
        {
            board._tiles[player.Position.X, player.Position.Y].WriteItems();
        }
        Console.SetCursorPosition(board.GetW + 2, 0);
        int x = board.GetW;
        int y = 0;
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("---Equipment---");
        int k = 1;
        for (int i = 1; i < 12; i++)
        {
            Console.SetCursorPosition(x, y + i);
            Console.Write(new String(' ', 50));
        }
        foreach (Item item in player._equipment)
        {

            Console.SetCursorPosition(x, y + k);
            Console.Write($"{k}." + item.GetName());
            k++;
        }
        Console.ResetColor();
        y = 0;
        x = board.GetW+17;
        Console.SetCursorPosition(x, y);
        Console.Write(new string(' ', 20));
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write($"Player moves: {player.playerMoves}");
        Console.ResetColor();
        x = board.GetW+2;
        y = 13 + 3;
        y = y - 1;
        Console.SetCursorPosition(x, y);
        Console.SetCursorPosition(x, y);
        Console.Write(new String(' ', 30));
        Console.SetCursorPosition(x, y);
        Console.Write("---" + player.GetName() + "---");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(x + 32, y + 1);
        Console.Write(new String(' ', 20));
        Console.SetCursorPosition(x + 32, y + 1);
        Console.Write($"Coins: {player.GetCoinNumber}");
        Console.SetCursorPosition(x + 32, y + 2);
        Console.Write(new String(' ', 20));
        Console.SetCursorPosition(x + 32, y + 2);
        Console.Write($"Gold: {player.GetGoldNumber}");
        Console.ResetColor();

        Console.SetCursorPosition(x, y + 1);
        Console.Write(new String(' ', 30));
        Console.SetCursorPosition(x, y + 1);
        Console.Write($"Health {player.GetHealth}");
        Console.SetCursorPosition(x, y + 2);
        Console.Write(new String(' ', 30));
        Console.SetCursorPosition(x, y + 2);
        Console.Write($"Power {player.GetPower}");
        Console.SetCursorPosition(x, y + 3);
        Console.Write(new String(' ', 30));
        Console.SetCursorPosition(x, y + 3);
        Console.Write($"Luck {player.GetLuck}");
        Console.SetCursorPosition(x, y + 4);
        Console.Write(new String(' ', 30));
        Console.SetCursorPosition(x, y + 4);
        Console.Write($"Wisdom {player.GetWisdom}");
        Console.SetCursorPosition(x, y + 5);
        Console.Write(new String(' ', 30));
        Console.SetCursorPosition(x, y + 5);
        Console.Write($"Agility {player.GetAgility}");
        Console.SetCursorPosition(x, y + 6);
        Console.Write(new String(' ', 30));
        Console.SetCursorPosition(x, y + 6);
        Console.Write($"Aggression {player.GetAggression}");
        Console.ResetColor();
        x = board.GetW+2;
        y = 13;
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(x, y);
            Console.Write(new String(' ', 60));
            Console.SetCursorPosition(x, y);
            Console.Write("Left Hand: " + (player.IsLeftHandNull ? "empty" : player.LeftHandItemGetName));
            Console.SetCursorPosition(x, y + 1);
            Console.Write(new String(' ', 60));
            Console.SetCursorPosition(x, y + 1);
            Console.Write("Right Hand: " + (player.IsRightHandNull? "empty" : player.RighttHandItemGetName));
            Console.ResetColor();

        
     
    }
}