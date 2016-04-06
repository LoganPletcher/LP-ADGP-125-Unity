using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The Base interface for an object.
/// </summary>
public interface IBaseStats<T>
{
    string Name
    { get; set; } // Name is a stored string that represents what an object is called in game.
    int MaxHealth
    { get; set; } // MaxHealth is a stored integer that represents an object's total hit points.
    int Health
    { get; set; } // Health is a stored integer that represents an object's current hit points.
    int Attack
    { get; set; } // Attack is a stored integer that represents the base for an object's abilities that affect another object's Health integer.
    void Ability1(T enemy, Text TB); // A void function that exists to be further defined by an object.
    void Ability2(T enemy, Text TB); // A void function that exists to be further defined by an object.
    void Ability3(T enemy, Text TB); // A void function that exists to be further defined by an object.
    void Refresh(); // A void function that resets an object's stats, with what stats those are to be defined in the object.
    int Stunned
    { get; set; } // Stunned is a stored integer that represents how long an object cannot attack.
    int DamageOverTime
    { get; set; } // DamageOverTime is a stored integer that represents how long an object takes minor decrements to their Health integer.
    int CharacterClass
    { get; set; } // CharacterClass is a stored integer that can be used by an object to identify what Character Class that object is.
}

/// <summary>
/// The Leveling interface that helps an object detail levels, leveling, and experience points.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ILevelingSystem<T>
{
    int Level
    { get; set; } // Level is a stored integer that represents an object's level.
    int ExperiencePoints
    { get; set; } // ExperiencePoints is a stored integer that represents an object's current experience points.
    int ExperiencetoLevelUp
    { get; set; } //ExperiencetoLevelUp is a stored integer that represents an object's total experience points.
    void LevelingUp(); // A void function that should change an object's Level, ExperiencePoints, and ExperiencetoLevelUp as defined by the object.
}

/// <summary>
/// A base class for character objects. It is serializable to allow for it to be saved in a file.
/// </summary>
[XmlInclude(typeof(Black_Mage))]
[XmlInclude(typeof(Archer))]
[XmlInclude(typeof(Blue_Mage))]
[XmlInclude(typeof(Fighter))]
[XmlInclude(typeof(Paladin))]
[XmlInclude(typeof(White_Mage))]
[Serializable()]
public class Unit : IBaseStats<Unit>, ILevelingSystem<Unit>
{
    public System.Random rng = new System.Random(); // A stored Random object that can be used for rolling.
    private string m_Name; private int m_HP; private int m_MaxHP; private int m_Att; private int m_S; private int m_DoT; private int m_CC;
    private int m_LVL; private int m_XP; private int m_MaxXP;

    public string Name // Makes Name refer to m_Name
    {
        get
        { return m_Name; }
        set
        { m_Name = value; }
    }

    public int Attack // Makes Attack refer to m_Att
    {
        get
        { return m_Att; }
        set
        { m_Att = value; }
    }

    public int DamageOverTime // Makes DamageOverTime refer to m_DoT
    {
        get
        { return m_DoT; }
        set
        { m_DoT = value; }
    }

    public int MaxHealth // Makes MaxHealth refer to m_MaxHP
    {
        get
        { return m_MaxHP; }
        set
        { m_MaxHP = value; }
    }

    public int Health // Makes Health refer to m_HP
    {
        get
        { return m_HP; }
        set
        { m_HP = value; }
    }

    public int Stunned // Makes Stunned refer to m_S
    {
        get
        { return m_S; }
        set
        { m_S = value; }
    }

    public int CharacterClass // Makes Character Class refer to m_CC
    {
        get
        { return m_CC; }
        set
        { m_CC = value; }
    }

    public virtual void Ability1(Unit enemy, Text TB) // Sets the parameter's type to Unit and is made to be virtual so classes that inherit this function from Unit can override the function and make it specific to the inheriting class.
    { }

    public virtual void Ability2(Unit enemy, Text TB) // Sets the parameter's type to Unit and is made to be virtual so classes that inherit this function from Unit can override the function and make it specific to the inheriting class.
    { }

    public virtual void Ability3(Unit enemy, Text TB) // Sets the parameter's type to Unit and is made to be virtual so classes that inherit this function from Unit can override the function and make it specific to the inheriting class.
    { }

    public virtual void Refresh() // Sets an object's Health to it's MaxHealth and sets it's Stunned and DamageOverTime both to zero.
    {
        this.Health = this.MaxHealth;
        this.Stunned = 0;
        this.DamageOverTime = 0;
    }

    public int Level // Makes Level refer to m_LVL
    {
        get
        { return m_LVL; }
        set
        { m_LVL = value; }
    }

    public int ExperiencePoints // Makes ExperiencePoints refer to m_XP
    {
        get
        { return m_XP; }
        set
        { m_XP = value; }
    }

    public int ExperiencetoLevelUp // Makes ExperiencetoLevelUp refer to m_MaxXP
    {
        get
        { return m_MaxXP; }
        set
        { m_MaxXP = value; }
    }

    public virtual void LevelingUp() // Set's an object's ExperiencePoints to the difference of ExperiencePoints subtracting ExperiencetoLevelUp, increases the object's level and lastly sets the object's ExperiencetoLevelUp to the product of ExperiencetoLevelUp and Level. The fucntion is made virtual so inheriting classes can modify the function for personal use.
    {
        ExperiencePoints -= ExperiencetoLevelUp;
        Level++;
        ExperiencetoLevelUp *= Level;
    }
}

public enum PartyType
{
    ENEMY,
    CHARACTER,
}
public enum ClassType
{
    DEFAULT = 0,
    BlackMage = 1,
    Archer = 2,
    BlueMage =3,
    Fighter = 4,
    Paladin =5,
    WhiteMage = 6,
}
[Serializable]
public class Party
{
    public Party()
    {
        _members = new List<Unit>();
    }
    public Party(PartyType p, int avg)
    {        
        _members = new List<Unit>();
        RandomTeam(p, avg);
    }

    List<Unit> _members;
    public List<Unit> Members
    {
        get { return _members; }
        set { _members = value; }
    }

    private void RandomTeam(PartyType p, int averageLevel)
    {
        System.Random r = new System.Random();
        for (int i = 0; i < 3; ++i)
        {
            Thread.Sleep(20);
            int randomClass = r.Next(1, 7);
            string name = (p == PartyType.ENEMY) ? "Enemy: " : "Character: ";
            int level = (p == PartyType.ENEMY) ? averageLevel : 1;
            
            switch (randomClass)
            {
                case 1:                    
                    _members.Add(new Black_Mage(name + i.ToString(), level));
                    break;
                case 2:
                    _members.Add(new Archer(name + i.ToString(), level));
                    break;
                case 3:
                    _members.Add(new Blue_Mage(name + i.ToString(), level));
                    break;
                case 4:
                    _members.Add(new Fighter(name + i.ToString(), level));
                    break;
                case 5:
                    _members.Add(new Paladin(name + i.ToString(), level));
                    break;
                case 6:
                    _members.Add(new White_Mage(name + i.ToString(), level));
                    break;
                default:
                    break;
            }
        } 

        
    }
}
/// <summary>
/// 
/// </summary>
[Serializable()]
public class Black_Mage : Unit
{
    private Black_Mage() { }

    public Black_Mage(string n, int l)
    {
        this.Name = n;
        this.Level = l;
        this.MaxHealth = 20 + ((l - 1) * 5);
        this.Health = 20 + ((l - 1) * 5);
        this.Attack = 3 + ((l - 1) * 1);
        this.Stunned = 0;
        this.DamageOverTime = 0;
        this.CharacterClass = 1;
        this.ExperiencetoLevelUp = 100;
        if (l > 1)
        {
            for (int i = 1; i < l + 1; i++)
            {
                this.ExperiencetoLevelUp *= i;
            }
        }
    }

    public override void Ability1(Unit enemy, Text TB)
    {
        enemy.Health -= this.Attack + 2;
        TB.text += (this.Name + " dealt " + (this.Attack + 2) + " magic damage to " + enemy.Name + ".\r\n");
        enemy.DamageOverTime = 2;
        TB.text += (this.Name + " poisoned " + enemy.Name + " for 2 rounds with dark magic.\r\n");
        
    }

    public override void Ability2(Unit enemy, Text TB)
    {
        for (int i = 0; i < (3 * this.Level); i++)
        {
            int damage = rng.Next(1, 3);
            enemy.Health -= damage;
            TB.text += (this.Name + " dealt " + damage + " magic damage to " + enemy.Name + ".\r\n");
            
        }
    }

    public override void Ability3(Unit enemy, Text TB)
    {
        enemy.Health -= this.Attack + 4;
        TB.text += (this.Name + " dealt " + (this.Attack + 4) + " magic damage to " + enemy.Name + ".\r\n");
        
    }

    public override void LevelingUp()
    {
        base.LevelingUp();
        this.MaxHealth += 5;
        this.Attack += 1;
    }
}

/// <summary>
/// 
/// </summary>
[Serializable()]
public class Archer : Unit
{
    private Archer() { }

    public Archer(string n, int l)
    {
        this.Name = n;
        this.Level = l;
        this.MaxHealth = 20 + ((l - 1) * 6);
        this.Health = 20 + ((l - 1) * 6);
        this.Attack = 3 + ((l - 1) * 2);
        this.Stunned = 0;
        this.DamageOverTime = 0;
        this.CharacterClass = 2;
        this.ExperiencetoLevelUp = 100;
        if (l > 1)
        {
            for (int i = 1; i < l + 1; i++)
            {
                this.ExperiencetoLevelUp *= i;
            }
        }
    }

    public override void Ability1(Unit enemy, Text TB)
    {
        for (int i = 0; i < 2; i++)
        {
            enemy.Health -= this.Attack + 2;
            TB.text += (this.Name + " dealt " + (this.Attack + 2) + " ranged damage to " + enemy.Name + ".\r\n");
        }
    }

    public override void Ability2(Unit enemy, Text TB)
    {
        enemy.Health -= this.Attack;
        TB.text += (this.Name + " dealt " + this.Attack + " ranged damage to " + enemy.Name + ".\r\n");
        enemy.Stunned = 2;
        TB.text += (this.Name + " stunned " + enemy.Name + " for 2 rounds.\r\n");
        
    }

    public override void Ability3(Unit enemy, Text TB)
    {
        enemy.Health -= this.Attack + 6;
        TB.text += (this.Name + " dealt " + (this.Attack + 6) + " ranged damage to " + enemy.Name + ".\r\n");
        
    }

    public override void LevelingUp()
    {
        base.LevelingUp();
        this.MaxHealth += 6;
        this.Attack += 2;
    }
}


/// <summary>
/// 
/// </summary>
[Serializable()]
public class Blue_Mage : Unit
{
    private Blue_Mage() { }

    public Blue_Mage(string n, int l)
    {
        this.Name = n;
        this.Level = l;
        this.MaxHealth = 20 + ((l - 1) * 5);
        this.Health = 20 + ((l - 1) * 5);
        this.Attack = 3 + ((l - 1) * 1);
        this.Stunned = 0;
        this.DamageOverTime = 0;
        this.CharacterClass = 3;
        this.ExperiencetoLevelUp = 100;
        if (l > 1)
        {
            for (int i = 1; i < l + 1; i++)
            {
                this.ExperiencetoLevelUp *= i;
            }
        }
    }

    public override void Ability1(Unit enemy, Text TB)
    {
        enemy.DamageOverTime = 2;
        TB.text += (this.Name + " poisoned " + enemy.Name + " for 2 rounds with magic.\r\n");
        enemy.Stunned = 2;
        TB.text += (this.Name + " stunned " + enemy.Name + " for 2 rounds with magic.\r\n");
        
    }

    public override void Ability2(Unit enemy, Text TB)
    {
        enemy.Health -= (this.Attack + 2);
        TB.text += (this.Name + " dealt " + (this.Attack + 2) + " magic damage to " + enemy.Name + ".\r\n");
        enemy.Stunned = 4;
        TB.text += (this.Name + " stunned " + enemy.Name + " for 4 rounds with magic.\r\n");
        
    }

    public override void Ability3(Unit enemy, Text TB)
    {
        enemy.Health -= this.Attack;
        TB.text += (this.Name + " dealt " + this.Attack + " magic damage to " + enemy.Name + ".\r\n");
        enemy.DamageOverTime = 4;
        TB.text += (this.Name + " poisoned " + enemy.Name + " for 4 rounds with magic.\r\n");
        
    }

    public override void LevelingUp()
    {
        base.LevelingUp();
        this.MaxHealth += 5;
        this.Attack += 1;
    }
}

/// <summary>
/// 
/// </summary>
[Serializable()]
public class Fighter : Unit
{
    private Fighter() { }

    public Fighter(string n, int l)
    {
        this.Name = n;
        this.Level = l;
        this.MaxHealth = 25 + ((l - 1) * 7);
        this.Health = 25 + ((l - 1) * 7);
        this.Attack = 6 + ((l - 1) * 2);
        this.Stunned = 0;
        this.DamageOverTime = 0;
        this.CharacterClass = 4;
        this.ExperiencetoLevelUp = 100;
        if (l > 1)
        {
            for (int i = 1; i < l + 1; i++)
            {
                this.ExperiencetoLevelUp *= i;
            }
        }
    }

    public override void Ability1(Unit enemy, Text TB)
    {
        enemy.Health -= (this.Attack + 6);
        TB.text += (this.Name + " dealt " + (this.Attack + 6) + " melee damage to " + enemy.Name + ".\r\n");
        
    }

    public override void Ability2(Unit enemy, Text TB)
    {
        enemy.Health -= (this.Attack + 2);
        TB.text += (this.Name + " dealt " + (this.Attack + 2) + " melee damage to " + enemy.Name + ".\r\n");
        enemy.DamageOverTime = 2;
        TB.text += (this.Name + " bloodied " + enemy.Name + " for 2 rounds.\r\n");
        
    }

    public override void Ability3(Unit enemy, Text TB)
    {
        enemy.Health -= (this.Attack + this.Attack);
        TB.text += (this.Name + " dealt " + (this.Attack + this.Attack) + " melee damage to " + enemy.Name + ".\r\n");
        
    }

    public override void LevelingUp()
    {
        base.LevelingUp();
        this.MaxHealth += 7;
        this.Attack += 2;
    }
}

/// <summary>
/// 
/// </summary>
[Serializable()]
public class Paladin : Unit
{
    private Paladin() { }

    public Paladin(string n, int l)
    {
        this.Name = n;
        this.Level = l;
        this.MaxHealth = 25 + ((l - 1) * 7);
        this.Health = 25 + ((l - 1) * 7);
        this.Attack = 6 + ((l - 1) * 2);
        this.Stunned = 0;
        this.DamageOverTime = 0;
        this.CharacterClass = 5;
        this.ExperiencetoLevelUp = 100;
        if (l > 1)
        {
            for (int i = 1; i < l + 1; i++)
            {
                this.ExperiencetoLevelUp *= i;
            }
        }
    }

    public override void Ability1(Unit enemy, Text TB)
    {
        enemy.Health -= (this.Attack + 10);
        TB.text += (this.Name + " dealt " + (this.Attack + 10) + " melee damage to " + enemy.Name + ".\r\n");
        enemy.Stunned = 0;
        TB.text += (this.Name + " cured " + enemy.Name + " of being stunned.\r\n");
        
    }

    public override void Ability2(Unit enemy, Text TB)
    {
        enemy.Health -= (this.Attack + 10);
        TB.text += (this.Name + " dealt " + (this.Attack + 10) + " melee damage to " + enemy.Name + ".\r\n");
        enemy.DamageOverTime = 0;
        TB.text += (this.Name + " cured " + enemy.Name + " of harmful side effects.\r\n");
        
    }

    public override void Ability3(Unit enemy, Text TB)
    {
        enemy.Health -= (this.Attack + 6);
        TB.text += (this.Name + " dealt " + (this.Attack + 6) + " melee damage to " + enemy.Name + ".\r\n");
        
    }

    public override void LevelingUp()
    {
        base.LevelingUp();
        this.MaxHealth += 7;
        this.Attack += 2;
    }
}

/// <summary>
/// 
/// </summary>
[Serializable()]
public class White_Mage : Unit
{
    private White_Mage() { }

    public White_Mage(string n, int l)
    {
        this.Name = n;
        this.Level = l;
        this.MaxHealth = 25 + ((l - 1) * 5);
        this.Health = 25 + ((l - 1) * 5);
        this.Attack = 2 + ((l - 1) * 1);
        this.Stunned = 0;
        this.DamageOverTime = 0;
        this.CharacterClass = 6;
        this.ExperiencetoLevelUp = 100;
        if (l > 1)
        {
            for (int i = 1; i < l + 1; i++)
            {
                this.ExperiencetoLevelUp *= i;
            }
        }
    }

    public override void Ability1(Unit enemy, Text TB)
    {
        if ((enemy.Health < enemy.MaxHealth) && (enemy.Health > 0))
        {
            enemy.Health += this.Attack + 10;
            if (enemy.Health > enemy.MaxHealth)
                enemy.Health = enemy.MaxHealth;
            TB.text += (this.Name + " healed " + enemy.Name + " for " + (this.Attack + 10) + " points.\r\n");
        }
        else
            TB.text += (enemy.Name + " is already at max health or is dead.\r\n");
        
    }

    public override void Ability2(Unit enemy, Text TB)
    {
        enemy.Stunned = 0;
        TB.text += (this.Name + " cured " + enemy.Name + " of being stunned.\r\n");
        
    }

    public override void Ability3(Unit enemy, Text TB)
    {
        enemy.DamageOverTime = 0;
        TB.text += (this.Name + " cured " + enemy.Name + " of harmful side effects.\r\n");
        
    }

    public override void LevelingUp()
    {
        base.LevelingUp();
        this.MaxHealth += 5;
        this.Attack += 1;
    }
}