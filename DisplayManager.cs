using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public void DisplayMessage(string message)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.SetCursorPosition(0, 22);
        Console.Write(new String(' ', 45));
        Console.SetCursorPosition(0, 22);
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
        player.WriteEquipment(board.GetW + 2, 0);
        player.WritePLayer(board.GetW + 2, 13 + 3);
        player.WriteHands(board.GetW + 2, 13);
    }
}