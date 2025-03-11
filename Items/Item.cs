using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRPG;

public interface IItem
{
    string GetName();
    void ApplyModifiers(Player player);
    void RemoveModifiers(Player player);
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
    public virtual string GetName()
    {
        return Name;
    }
    public virtual void ApplyModifiers(Player player) { } 
    public virtual void RemoveModifiers(Player player) { }

}
