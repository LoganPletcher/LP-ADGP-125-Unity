using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

public class Finite_State_Machine
{

    public class Transition
    {
        public Enum m_firstState;
        public Enum m_secondState;
        public Transition(Enum S1, Enum S2)
        {
            m_firstState = S1;
            m_secondState = S2;
        }
    }
    private Enum m_CS;
    public Enum CurrentState
    {
        get
        { return m_CS; }
    }
    private List<Enum> m_States;

    public Finite_State_Machine(Enum cs)
    {
        m_CS = cs;
        m_States = new List<Enum>();
    }

    public bool ChangeStates(string t)
    {
        bool startingState = false;
        bool validTransition = false;
        foreach (KeyValuePair<string, Transition> entry in TransitionTable)
        {
            if (entry.Key == t)
            {
                validTransition = true;
            }
            else
                validTransition = false;
            if (Convert.ToString(m_CS) == Convert.ToString(entry.Value.m_firstState))
            {
                startingState = true;
            }
            else
                startingState = false;
            if (((validTransition == true) && (startingState == true)))
            {
                Console.WriteLine
                    ("Transition is valid. Changing current state from " + m_CS + " to " + entry.Value.m_secondState + ".");
                m_CS = entry.Value.m_secondState;
                return true;
            }
        }
        Console.WriteLine("No such transition exists or the transition is not valid. Make sure there are no typos, that the transition and states exist, and that the current state matches the first state of the transition.");
        return false;
    }

    public bool AddState(Enum s)
    {
        if (m_States.Contains(s))
        {
            Console.WriteLine("The Finite State Machine already contains this state.");
            return false;
        }
        else if (!(s.GetType() == typeof(Enum)))
        {
            m_States.Add(s);
            Console.WriteLine("State " + s + " added.");
            return true;
        }
        else
        {
            Console.WriteLine(s + " is invalid as a state because it is not an Enum.");
            return false;
        }
    }

    public int info()
    {
        int count = 0;
        Console.WriteLine("The Finite State Machine is comprised of the following states: ");
        foreach (Enum s in m_States)
        {
            Console.WriteLine
                ("State " + count + ": " + s);
            count++;
        }
        count = 0;
        Console.WriteLine("The Finite State Machine contains the following transitions: ");
        foreach (KeyValuePair<string, Transition> entry in TransitionTable)
        {
            Console.WriteLine
                ("Transition " + count + ": " + entry.Key);
            count++;
        }
        Console.WriteLine("The current state is " + m_CS);
        return count;
    }

    //
    //
    //
    //
    //
    public bool AddTransition(Enum f, Enum t)
    {
        Transition transition = new Transition(f, t);
        TransitionTable.Add((Convert.ToString(f) + "->" + Convert.ToString(t)), transition);
        Console.WriteLine("Transition " + Convert.ToString(f) + "->" + Convert.ToString(t) + " created.");
        return true;
    }

    //private List<Transition> m_Transitions;
    Dictionary<string, Transition> TransitionTable = new Dictionary<string, Transition>();
}
