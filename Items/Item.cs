using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameRPG;
// TODO OnItem i wtedy dla waluty zrobic ze sie inkrementuje kiermana xd a dla reszty normalnie leci do eq 
public interface IItem
{
    void OnPickUp(Player player);
    string GetName();
    char GetSign();
    void ApplyModifiers(Player player);
    void RemoveModifiers(Player player);
    bool ApplyOnEquip();
}
public abstract class Item : IItem
{
    public char signifying { get; protected set; } = 'I';
    private bool _applayOnEquip = true;

    public string Name { get; protected set; }
    public bool NeedsTwoArms { get; protected set; }
    public Item(string name, bool needsTwoArms = false, bool applyOnEquip = true)
    {
        Name = name;
        NeedsTwoArms = needsTwoArms;
        _applayOnEquip = applyOnEquip;
    }
    public virtual char GetSign() => signifying;
    public virtual string GetName()
    {
        return Name;
    }
    public virtual void ApplyModifiers(Player player) { } 
    public virtual void RemoveModifiers(Player player) { }
    public bool ApplyOnEquip() => _applayOnEquip;

    public virtual void OnPickUp(Player player)
    { 
        player._equipment.Add(this);
    }

  
}
