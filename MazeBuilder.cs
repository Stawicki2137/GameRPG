using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GameRPG;

public interface IMazeBuilder
{
    void BuildEmptyDungegon();
    void BuildFilledDungegon();
    void AddRandomPaths();
    void AddCentralRoom();
    void AddChamber();
    void GenerateItems(int amount);
    void GenerateModifiedItems(int amount);
    void GenerateWeapons(int amount);
    void GenerateModifiedWeapons(int amount);
    void GenerateCurrencies(int amount);
    void GenerateElixirs(int amount);
    void GenerateEnemies(int amount);
    Board GetBoard();
}

public class DefaultMazeBuilder : IMazeBuilder
{
    private Board _board;
    private Random _rand = new Random();
    bool addedInfo = false;
    public DefaultMazeBuilder(int width = 20, int height = 40)
    {
        _board = new Board(width, height);
    }
    public void AddChamber()
    {
        int centerX = _board.GetW - 5;
        int centerY = _board.GetH - 5;
        int width = 3;

        for (int y = centerY - width; y < centerY + width; y++)
        {
            for (int x = centerX - width; x <= centerX + width; x++)
            {
                _board._tiles[y, x]._symbol = ' '; // floor

            }
        }
    }
    public void AddCentralRoom()
    {
        int centerX = _board.GetW / 2;
        int centerY = _board.GetH / 2;
        int width = 5;
        for (int y = centerY - width; y <= centerY + width; y++)
        {
            for (int x = centerX - width; x <= centerX + width; x++)
            {
                _board._tiles[y, x]._symbol = ' '; // floor

            }
        }
    }

    public void AddRandomPaths()
    {
        int moveNumber = 0;
        int x = 1;
        int y = 1;
        while (moveNumber < 80)
        {
            moveNumber++;
            if (x >= _board.GetH - 1 || y >= _board.GetW - 1)
                break;
            _board._tiles[x, y]._symbol = ' ';
            if (_rand.Next() % 3 == 0)
            {
                x++;
            }
            else
                y++;
        }
        moveNumber = 0;
        int row = 20;
        int col = 1;
        while (moveNumber < 80)
        {
            moveNumber++;
            if (row >= _board.GetH - 1 || col >= _board.GetW - 1 ||
                row < 1 || col < 1)
                break;
            _board._tiles[row, col]._symbol = ' ';
            if (_rand.Next() % 4 == 0)
            {
                row--;
            }
            else
                col++;
        }
        moveNumber = 0;
        row = 20;
        col = 40;
        while (moveNumber < 80)
        {
            moveNumber++;
            if (row >= _board.GetH - 1 || col >= _board.GetW - 1 ||
                row < 1 || col < 1)
                break;
            _board._tiles[row, col]._symbol = ' ';
            if (_rand.Next() % 4 == 0)
            {
                row--;
            }
            else
                col--;
        }
        moveNumber = 0;
        row = 1;
        col = 40;
        while (moveNumber < 80)
        {
            moveNumber++;
            if (row >= _board.GetH - 1 || col >= _board.GetW - 1 ||
                row < 1 || col < 1)
                break;
            _board._tiles[row, col]._symbol = ' ';
            if (_rand.Next() % 4 == 0)
            {
                row++;
            }
            else
                col--;
        }

        for (int i = 1; i < _board.GetW - 1; i++)
        {
            for (int j = 1; j < _board.GetH - 1; j++)
            {
                if (_board._tiles[j, i]._symbol == '█')
                {
                    if (_rand.Next() % 3 == 0)
                    {
                        _board._tiles[j, i]._symbol = ' ';
                    }
                }
            }
        }

    }

    public void BuildEmptyDungegon()
    {
        int w = _board.GetW;
        int h = _board.GetH;
        for (int x = 0; x < h; x++)
        {
            for (int y = 0; y < w; y++)
            {
                if (x == 0 || y == 0 || x == h - 1 || y == w - 1)
                    _board._tiles[x, y]._symbol = '█';
                else
                {
                    _board._tiles[x, y]._symbol = ' ';
                }
            }
        }
    }

    public void BuildFilledDungegon()
    {
        int w = _board.GetW;
        int h = _board.GetH;
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < w; x++)
            {
                _board._tiles[y, x]._symbol = '█';
            }
        }
    }

    public Board GetBoard()
    {
        return _board;
    }
    public void GenerateWeapons(int amount)
    {
        int Xend = _board.GetW;
        int Yend = _board.GetH;

        int i = 0;

        while (i < amount)
        {
            int x = _rand.Next(1, Xend);
            int y = _rand.Next(1, Yend);
            if (_board._tiles[y, x]._symbol == ' ')
            {
                int rand = _rand.Next();
                if (rand % 3 == 0)
                {
                    _board.AddItem(new Point(y, x), new TwoHandedHeavySword());
                    i++;
                }
                else if (rand % 3 == 1)
                {
                    _board.AddItem(new Point(y, x), new LightSword());
                    i++;
                }
                else if (rand % 3 == 2)
                {
                    _board.AddItem(new Point(y, x), new Dagger());
                    i++;
                }

            }
        }
        if (!addedInfo)
        {
            _board._help.AppendLine("INVENTORY CONTROLS:");
            _board._help.AppendLine("E - Pick up item");
            _board._help.AppendLine("T - Drop item");
            _board._help.AppendLine("V - Move item from hand to equipment");
            _board._help.AppendLine("G - Move item from equipment to hand");
               addedInfo = true;
            _board._binds.Append(" E-Equip T-DropItem G-TakeItemToHand V-ItemFromHandToEq");

        }

    }
    public void GenerateElixirs(int amount)
    {
        int Xend = _board.GetW;
        int Yend = _board.GetH;
        int i = 0;
        while (i < amount)
        {
            int x = _rand.Next(1, Xend - 1);
            int y = _rand.Next(1, Yend - 1);
            int rand = _rand.Next();
            if (_board._tiles[y, x]._symbol == ' ')
            {

                if (rand % 3 == 0)
                {
                    _board.AddItem(new Point(y, x), new HealthElixir());
                    i++;

                }
                if (rand % 3 == 1)
                {
                    _board.AddItem(new Point(y, x), new PudzianElixir(16));
                    i++;

                }
                if (rand % 3 == 2)
                {
                    _board.AddItem(new Point(y, x), new WisdomElixir(20));
                    i++;

                }
            }
        }
        if (!addedInfo)
        {
            _board._help.AppendLine("INVENTORY CONTROLS:");
            _board._help.AppendLine("E - Pick up item");
            _board._help.AppendLine("T - Drop item");
            _board._help.AppendLine("V - Move item from hand to equipment");
            _board._help.AppendLine("G - Move item from equipment to hand");

            addedInfo = true;
            _board._binds.Append(" E-Equip T-DropItem G-TakeItemToHand V-ItemFromHandToEq");

        }
        _board._help.AppendLine("K - Drink Elixir");
        _board._binds.Append(" K-Drink");


    }
    public void GenerateModifiedWeapons(int amount)
    {
        int Xend = _board.GetW;
        int Yend = _board.GetH;

        int i = 0;

        while (i < amount)
        {
            int x = _rand.Next(1, Xend);
            int y = _rand.Next(1, Yend);
            if (_board._tiles[y, x]._symbol == ' ')
            {
                int rand = _rand.Next();
                if (rand % 7 == 0)
                {
                    _board.AddItem(new Point(y, x), new HolyDecorator(new UltraStrong(new TwoHandedHeavySword())));
                    i++;
                }
                else if (rand % 7 == 1)
                {
                    _board.AddItem(new Point(y, x), new LuckyDecorator(new Damned(new TwoHandedHeavySword())));
                    i++;
                }
                else if (rand % 7 == 2)
                {
                    _board.AddItem(new Point(y, x), new HolyDecorator(new TwoHandedHeavySword()));
                    i++;
                }
                else if (rand % 7 == 1)
                {
                    _board.AddItem(new Point(y, x), new HolyDecorator(new UltraStrong(new LightSword())));
                    i++;
                }
                else if (rand % 7 == 2)
                {
                    _board.AddItem(new Point(y, x), new Damned(new Dagger()));
                    i++;
                }
                else if (rand % 7 == 3)
                {
                    _board.AddItem(new Point(y, x), new LuckyDecorator(new LightSword()));
                    i++;
                }
                else if (rand % 7 == 4)
                {
                    _board.AddItem(new Point(y, x), new UltraStrong(new Dagger()));
                    i++;
                }
                else if (rand % 7 == 5)
                {
                    _board.AddItem(new Point(y, x), new HolyDecorator(new LightSword()));
                    i++;
                }
                else if (rand % 7 == 6)
                {
                    _board.AddItem(new Point(y, x), new UnluckyDecorator(new Dagger()));
                    i++;
                }

            }
        }
        if (!addedInfo)
        {
            _board._help.AppendLine("INVENTORY CONTROLS:");
            _board._help.AppendLine("E - Pick up item");
            _board._help.AppendLine("T - Drop item");
            _board._help.AppendLine("V - Move item from hand to equipment");
            _board._help.AppendLine("G - Move item from equipment to hand");
            _board._binds.Append(" E-Equip T-DropItem G-TakeItemToHand V-ItemFromHandToEq");
            addedInfo = true;
        }
    }
    public void GenerateItems(int amount)
    {
        int Xend = _board.GetW;
        int Yend = _board.GetH;
        int i = 0;
        while (i < amount)
        {
            int x = _rand.Next(1, Xend - 1);
            int y = _rand.Next(1, Yend - 1);
            int rand = _rand.Next();
            if (_board._tiles[y, x]._symbol == ' ')
            {

                if (rand % 3 == 0)
                {
                    _board.AddItem(new Point(y, x), new Log());
                    i++;

                }
                if (rand % 3 == 1)
                {
                    _board.AddItem(new Point(y, x), new Stone());
                    i++;

                }
                if (rand % 3 == 2)
                {
                    _board.AddItem(new Point(y, x), new HollowShield());
                    i++;

                }

            }
        }
        if (!addedInfo)
        {
            _board._help.AppendLine("INVENTORY CONTROLS:");
            _board._help.AppendLine("E - Pick up item");
            _board._help.AppendLine("T - Drop item");
            _board._help.AppendLine("V - Move item from hand to equipment");
            _board._help.AppendLine("G - Move item from equipment to hand");
            _board._binds.Append(" E-Equip T-DropItem G-TakeItemToHand V-ItemFromHandToEq");
            addedInfo = true;
        }
    }
    public void GenerateCurrencies(int amount)
    {
        int i = 0;
        int Xend = _board.GetW;
        int Yend = _board.GetH;
        while (i < amount)
        {
            int x = _rand.Next(1, Xend - 1);
            int y = _rand.Next(1, Yend - 1);
            int rand = _rand.Next();
            if (rand % 2 == 0)
            {
                _board.AddItem(new Point(y, x), new Gold());
                i++;

            }
            else
            {
                _board.AddItem(new Point(y, x), new Coin());
                i++;

            }
        }
        _board._help.AppendLine("CURRENCY INTERACTIONS:");
        _board._help.AppendLine("Collect as much money as you can!");
        _board._help.AppendLine("E - Collect money to pocket");
        _board._binds.Append(" E-Collect");

    }

    public void GenerateModifiedItems(int amount)
    {
        int Xend = _board.GetW;
        int Yend = _board.GetH;
        int i = 0;
        while (i < amount)
        {
            int x = _rand.Next(1, Xend - 1);
            int y = _rand.Next(1, Yend - 1);
            int rand = _rand.Next();
            if (_board._tiles[y, x]._symbol == ' ')
            {

                if (rand % 5 == 0)
                {
                    _board.AddItem(new Point(y, x), new LuckyDecorator(new Log()));
                    i++;

                }
                else if (rand % 5 == 1)
                {
                    _board.AddItem(new Point(y, x), new HolyDecorator(new Stone()));
                    i++;

                }
                else if (rand % 5 == 2)
                {
                    _board.AddItem(new Point(y, x), new Strong(new UnluckyDecorator(new HollowShield())));
                    i++;

                }
                else if (rand % 5 == 4)
                {
                    _board.AddItem(new Point(y, x), new Damned(new UnluckyDecorator(new HollowShield())));
                    i++;

                }
                else if (rand % 5 == 4)
                {
                    _board.AddItem(new Point(y, x), new HolyDecorator(new Log()));
                    i++;
                }
            }
        }
        if (!addedInfo)
        {
            _board._help.AppendLine("INVENTORY CONTROLS:");
            _board._help.AppendLine("E - Pick up item");
            _board._help.AppendLine("T - Drop item");
            _board._help.AppendLine("V - Move item from hand to equipment");
            _board._help.AppendLine("G - Move item from equipment to hand");
            _board._binds.Append(" E-Equip T-DropItem G-TakeItemToHand V-ItemFromHandToEq");

            addedInfo = true;
        }
    }
    public void GenerateEnemies(int amount)
    {

        int Xend = _board.GetW;
        int Yend = _board.GetH;
        int i = 0;
        while (i < amount)
        {
            int x = _rand.Next(1, Xend - 1);
            int y = _rand.Next(1, Yend - 1);
            int rand = _rand.Next();
            if (_board._tiles[y, x]._symbol == ' ')
            {
                if (rand % 2 == 0)
                {

                    if (_board._tiles[y, x].AddEnemy(new Goblin()))
                        i++;

                }
                else
                {
                    if (_board._tiles[y, x].AddEnemy(new Ork()))
                        i++;

                }

            }


        }
        _board._binds.Append(" F-fight");
        _board._help.AppendLine("INTERACTIONS WITH ENEMY:");
        _board._help.AppendLine("F - fight with enemy");
    }
}
public class Director
{
    private IMazeBuilder _builder;
    private bool _startDone = false;
    public Director(IMazeBuilder builder)
    {
        _builder = builder;
    }
    public void GenerateItems(int amount = 10)
    {
        if (!_startDone)
            throw new InvalidOperationException("At the begining use starting procedure");
        _builder.GenerateItems(amount);
    }
    public void GenerateElixirs(int amount = 10)
    {
        if (!_startDone)
            throw new InvalidOperationException("At the begining use starting procedure");
        _builder.GenerateElixirs(amount);
    }
    public void GenerateEnemies(int amount = 5)
    {
        if (!_startDone)
            throw new InvalidOperationException("At the begining use starting procedure");
        _builder.GenerateEnemies(amount);
    }
    public void AddRandomPaths()
    {
        if (!_startDone)
            throw new InvalidOperationException("At the begining use starting procedure");
        _builder.AddRandomPaths();
    }
    public void BuildEmptyDungegon()
    {
        if (_startDone)
            throw new InvalidOperationException("Starting procedure already started");
        _startDone = true;
        _builder.BuildEmptyDungegon();
    }
    public void BuildFilledDungegon()
    {
        if (_startDone)
            throw new InvalidOperationException("Starting procedure already started");
        _builder.BuildFilledDungegon();
        _startDone = true;
    }
    public void AddCentralRoom()
    {
        if (!_startDone)
            throw new InvalidOperationException("At the begining use starting procedure");
        _builder.AddCentralRoom();
    }
    public void GenerateModifiedItems(int amount = 10)
    {
        if (!_startDone)
            throw new InvalidOperationException("At the begining use starting procedure");
        _builder.GenerateModifiedItems(amount);
    }
    public void GenerateModifiedWeapons(int amount = 10)
    {
        if (!_startDone)
            throw new InvalidOperationException("At the begining use starting procedure");
        _builder.GenerateModifiedWeapons(amount);
    }
    public void GenerateWeapons(int amount = 10)
    {
        if (!_startDone)
            throw new InvalidOperationException("At the begining use starting procedure");
        _builder.GenerateWeapons(amount);
    }
    public void GenerateCurrencies(int amount = 30)
    {
        if (!_startDone)
            throw new InvalidOperationException("At the begining use starting procedure");
        _builder.GenerateCurrencies(amount);
    }


    public void AddChamber()
    {
        if (!_startDone)
            throw new InvalidOperationException("At the begining use starting procedure");
        _builder.AddChamber();
    }

    public Board GetBoard()
    {
        return _builder.GetBoard();
    }


}

