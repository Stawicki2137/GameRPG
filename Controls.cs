using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRPG;

public interface IControlHandler
{
    IControlHandler SetNext(IControlHandler controlHandler);
    bool Handle(ConsoleKey key, Player player, Board board, ref bool running);
}

public abstract class BaseControlHandler : IControlHandler
{
    private IControlHandler? _nextControlHandler;
    public IControlHandler SetNext(IControlHandler nextControlHandler)
    {
        _nextControlHandler = nextControlHandler;
        return nextControlHandler;
    }
    public virtual bool Handle(ConsoleKey key, Player player,Board board, ref bool running)
    {
        if(_nextControlHandler != null)
        {
            return _nextControlHandler.Handle(key, player, board, ref running);
        }
        else
        {
            return false;
        }
    }
}

public class MoveHandler : BaseControlHandler
{
    public override bool Handle(ConsoleKey key, Player player, Board board, ref bool running)
    {
        switch (key)
        {
            case ConsoleKey.W:
                player.Move(-1, 0);
                DisplayManager.GetInstance().DisplayMessage($"Player moved north");
                return true; 
            case ConsoleKey.S:
                player.Move(1, 0);
                DisplayManager.GetInstance().DisplayMessage($"Player moved south");
                return true;
            case ConsoleKey.A:
                player.Move(0, -1);
                DisplayManager.GetInstance().DisplayMessage($"Player moved west");
                return true;
            case ConsoleKey.D:
                player.Move(0, 1);
                DisplayManager.GetInstance().DisplayMessage($"Player moved east");
                return true;
            default:
                return base.Handle(key, player, board, ref running);
        }
    }
}

public class PickItemHandler : BaseControlHandler
{
    public override bool Handle(ConsoleKey key, Player player, Board board, ref bool running)
    {
        if (key == ConsoleKey.E)
        {

            if(player.PickItem())
            { 
                DisplayManager.GetInstance().DisplayGameState(board, player);
                DisplayManager.GetInstance().DisplayMessage($"Item picked up");

                return true;
            }
            else
            {
                DisplayManager.GetInstance().DisplayMessage($"Nothing to pick up");
                return true; 
            }
        }

        return base.Handle(key, player, board, ref running);
    }
}

public class DropItemHandler : BaseControlHandler
{
    public override bool Handle(ConsoleKey key, Player player, Board board, ref bool running)
    {
        if (key == ConsoleKey.T)
        {

            if (player.DropItem())
            {
                DisplayManager.GetInstance().DisplayGameState(board, player);
                DisplayManager.GetInstance().DisplayMessage($"Item dropped");
                return true;
            }
            else
            {
                DisplayManager.GetInstance().DisplayMessage($"Nothing to drop");

                return true;
            }
        }

        return base.Handle(key, player, board, ref running);
    }
}

public class DrinkElixirHandler : BaseControlHandler
{
    public override bool Handle(ConsoleKey key, Player player, Board board, ref bool running)
    {
        if (key == ConsoleKey.K)
        {
            int p = player.DrinkElixir();
            if (p==1)
            {
                DisplayManager.GetInstance().DisplayGameState(board, player);
                DisplayManager.GetInstance().DisplayMessage($"Elixir drunk");
                return true;
            }
            else if(p==-1)
            {
                DisplayManager.GetInstance().DisplayMessage($"Nothing to drink");

                return true;
            }
            else if (p==0)
            {
                DisplayManager.GetInstance().DisplayMessage($"Vial is empty!");

                return true;

            }
        }

        return base.Handle(key, player, board, ref running);
    }
}
public class FightHandler:BaseControlHandler
{
    public override bool Handle(ConsoleKey key, Player player, Board board, ref bool running)
    {
        if (key==ConsoleKey.F)
        {
            string communicate = "debug- FIghtHandler-Handle";
            int ans;
            switch(ans=player.Fight())
            {
                case 10:
                    {
                        communicate = "Enemy is dead!";
                        break;
                    }
                case 11:
                    {
                        communicate = "Player is dead";
                        break;
                    }
                case -1:
                    {
                        communicate = "This hand is empty!";
                        break;
                    }
                case 0:
                    {
                        communicate="Player has empty hands!";
                        break;
                    }
                case 1:
                    {
                        communicate = "Fight has ended";
                        break;
                    }
              
                case 2:
                    {
                        communicate = "2hand attack is impossible right now";
                        break;
                    }
                case 3:
                    {
                        communicate = "Probably you've chosen wrong hand";
                        break;
                    }
                case 4:
                    {
                        communicate = "You have chosen wrong attack type";
                        break;
                    }
                case 5:
                    {
                        communicate = "No enemy to fight";
                        break;
                    }
                case 6:
                    {
                        communicate = "No attack was made";
                        break;
                    }
                default:
                    {
                        ;
                        communicate = "not predicted behaviour";
                        break;
                    }
            }
            DisplayManager.GetInstance().DisplayMessage(communicate);
            return true;
        }
        return base.Handle(key, player, board, ref running);
    }
}

public class MoveItemFromEqToHand: BaseControlHandler
{
    public override bool Handle(ConsoleKey key, Player player, Board board, ref bool running)
    {
        if (key == ConsoleKey.G)
        {

            if (player.MoveItemFromEqToHand())
            {
                DisplayManager.GetInstance().DisplayGameState(board, player);
                DisplayManager.GetInstance().DisplayMessage($"Item moved to hand");

                return true;
            }
            else
            {
                DisplayManager.GetInstance().DisplayMessage($"Nothing to take to hand");

                return true;
            }
        }

        return base.Handle(key, player, board, ref running);
    }
}

public class MoveItemFromHandToEq : BaseControlHandler
{
    public override bool Handle(ConsoleKey key, Player player, Board board, ref bool running)
    {
        if (key == ConsoleKey.V)
        {

            if (player.MoveItemFromHandToEq())
            {
                DisplayManager.GetInstance().DisplayGameState(board, player);
                DisplayManager.GetInstance().DisplayMessage($"Item moved to equipment");

                return true;
            }
            else
            {
                DisplayManager.GetInstance().DisplayMessage($"Nothing to move to equipment");
                return true;
            }
        }

        return base.Handle(key, player, board, ref running);
    }
}

public class DisplayHelp : BaseControlHandler
{
    public override bool Handle(ConsoleKey key, Player player, Board board, ref bool running)
    {
        if (key == ConsoleKey.H)
        {

            DisplayManager.GetInstance().DisplayHelp(board, player);
            return true;
        }

        return base.Handle(key, player, board, ref running);
    }
}
public class ExitHandler : BaseControlHandler
{
    public override bool Handle(ConsoleKey key, Player player, Board board, ref bool running)
    {
        if (key == ConsoleKey.Q)
        {
            DisplayManager.GetInstance().DisplayMessage($"Game exited");

            running = false;
            return true;
        }
        return base.Handle(key, player, board, ref running);
    }
}

public class InvalidInputHandler : BaseControlHandler
{
    public override bool Handle(ConsoleKey key, Player player, Board board, ref bool running)
    {
        DisplayManager.GetInstance().DisplayMessage($"Invalid control: {key}");
        return true;
    }
}
public class InputHandlerChain
{
    private readonly IControlHandler _chain;

    public InputHandlerChain()
    {

        _chain = new MoveHandler();
        _chain
            .SetNext(new PickItemHandler())
            .SetNext(new DropItemHandler())
            .SetNext(new MoveItemFromEqToHand())
            .SetNext(new MoveItemFromHandToEq())
            .SetNext(new DrinkElixirHandler())
            .SetNext(new FightHandler())
            .SetNext(new DisplayHelp())
            .SetNext(new ExitHandler())
            .SetNext(new InvalidInputHandler());
    }
    public void HandleInput(ConsoleKey key, Player player, Board board, ref bool running)
    {
        _chain.Handle(key, player, board, ref running);
    }
}