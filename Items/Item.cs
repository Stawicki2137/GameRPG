using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRPG;

public interface IItem
{ 
    


}
public abstract class Item : IItem
{
    public char signifying { get; protected set; }
    public string Name { get; protected set; }
    public bool NeedsTwoArms { get; protected set; }
    public Item(string name, bool needsTwoArms = false)
    {
        Name = name;
        NeedsTwoArms = needsTwoArms;
    }

}
