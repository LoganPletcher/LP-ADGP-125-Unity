using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

class Program : MonoBehaviour
{
    public Canvas C;

    enum PlayerStates   //A enum created for holding Enums that will be used as states in a Finite_State_Machine
    {
        init,
        Prebattle,
        teamAturn,
        teamBturn,
        victory,
    }

    static bool init(Finite_State_Machine FSM, Canvas C)      //A bool function that takes a Finite_State_Machine and represents the init state
    {
        Save_and_Load<Party> sl = new Save_and_Load<Party>();   //Creates a Save_and_Load that uses Partys called sl
        FSM.ChangeStates("init->Prebattle");        //Changes the current state from init to Prebattle
        FSM.info();                                 //Runs the info function of FSM
        if (Convert.ToString(FSM.CurrentState) == Convert.ToString(PlayerStates.Prebattle))     //Checks the current state of FSM to see if it is in the Prebattle state.
        { Prebattle(FSM, sl, C); }     //runs the Prebattle function, passing in the FSM Finite_State_Machine and the sl Save_and_Load<Part>
        return false;       //if the return has been reached then something went wrong and the function returns a false.
    }

    static bool Prebattle(Finite_State_Machine FSM, Save_and_Load<Party> sl, Canvas C)
    {
        bool TeamABuilt = false;
        Party partyA = new Party();
        Party partyB = new Party();
        //PrebattleDisplay LoadingScene = new PrebattleDisplay(partyA);
        PrebattleDisplay LoadingScene = C.GetComponentInChildren<PrebattleDisplay>();
        LoadingScene.CreateParty(partyA);
        LoadingScene.Start();
        TeamABuilt = (partyA.Members.Count >= 3) ? true : false;
        if (!TeamABuilt)
            partyA = new Party(PartyType.CHARACTER, 1);
        foreach (Unit u in partyA.Members)
        {
            UnityEngine.Debug.Log(u.Name);
        }
        int sum = 0;
        foreach (Unit u in partyA.Members)
            sum += u.Level;

        int avgLvl = sum / partyA.Members.Count;
        partyB = new Party(PartyType.ENEMY, avgLvl);

        FSM.ChangeStates("Prebattle->teamAturn");
        FSM.info();
        //if (Convert.ToString(FSM.CurrentState) == Convert.ToString(PlayerStates.teamAturn))
        //{
        //    teamAturn(FSM, sl, partyA, partyB);
        //}
        //Environment.Exit(0);
        return false;
    }

    //static bool teamAturn(Finite_State_Machine FSM, Save_and_Load<Party> sl, Party teamA, Party teamB)
    //{
    //    bool FirstUse = false;
    //    for (int i = 0; i < 3; i++)
    //    {
    //        if (i == 0)
    //            FirstUse = true;
    //        else
    //            FirstUse = false;
    //        if (teamA.Members[i].Health > 0)
    //        {
    //            if (teamA.Members[i].Stunned <= 0)
    //            {
    //                //BattleScene BS = new BattleScene(teamA, teamB, i, FirstUse);
    //                //Application.Run(BS);
    //            }
    //            else
    //                teamA.Members[i].Stunned--;
    //            if (teamA.Members[i].DamageOverTime > 0)
    //            {
    //                teamA.Members[i].Health -= teamA.Members[i].Level;
    //                teamA.Members[i].DamageOverTime--;
    //            }
    //            if (teamB.Members[0].Health <= 0 && teamB.Members[1].Health <= 0 && teamB.Members[2].Health <= 0)
    //                FSM.ChangeStates("teamAturn->victory");
    //            //if (Convert.ToString(FSM.CurrentState) == Convert.ToString(PlayerStates.victory))
    //            //{
    //            //    FSM.info();
    //            //    victory(FSM, sl, teamA, teamB, true);
    //            //}
    //            if (teamA.Members[0].Health <= 0 && teamA.Members[1].Health <= 0 && teamA.Members[2].Health <= 0)
    //                FSM.ChangeStates("teamBturn->victory");
    //            //if (Convert.ToString(FSM.CurrentState) == Convert.ToString(PlayerStates.victory))
    //            //{
    //            //    FSM.info();
    //            //    victory(FSM, sl, teamA, teamB, false);
    //            //}
    //        }
    //    }
    //    FSM.ChangeStates("teamAturn->teamBturn");
    //    FSM.info();
    //    if (Convert.ToString(FSM.CurrentState) == Convert.ToString(PlayerStates.teamBturn))
    //    {
    //        teamBturn(FSM, sl, teamA, teamB);
    //    }
    //    return false;
    //}

    //static bool teamBturn(Finite_State_Machine FSM, Save_and_Load<Party> sl, Party teamA, Party teamB)
    //{
    //    for (int i = 0; i < 3; i++)
    //    {
    //        if (teamB.Members[i].Health > 0)
    //        {
    //            if (teamB.Members[i].Stunned <= 0)
    //            {
    //                //TeamBbattlescene TBbs = new TeamBbattlescene(teamA, teamB, i);
    //                //Application.Run(TBbs);
    //            }
    //            else
    //                teamB.Members[i].Stunned--;
    //            if (teamB.Members[i].DamageOverTime > 0)
    //            {
    //                teamB.Members[i].Health -= teamB.Members[i].Level;
    //                teamB.Members[i].DamageOverTime--;
    //            }
    //            if (teamA.Members[0].Health <= 0 && teamA.Members[1].Health <= 0 && teamA.Members[2].Health <= 0)
    //                FSM.ChangeStates("teamBturn->victory");
    //            //if (Convert.ToString(FSM.CurrentState) == Convert.ToString(PlayerStates.victory))
    //            //{
    //            //    FSM.info();
    //            //    victory(FSM, sl, teamA, teamB, false);
    //            //}
    //        }
    //    }
    //    FSM.ChangeStates("teamBturn->teamAturn");
    //    FSM.info();
    //    if (Convert.ToString(FSM.CurrentState) == Convert.ToString(PlayerStates.teamAturn))
    //    {
    //        teamAturn(FSM, sl, teamA, teamB);
    //    }
    //    return false;
    //}

    //static bool victory(Finite_State_Machine FSM, Save_and_Load<Party> sl, Party teamA, Party teamB, bool teamAwin)
    //{
    //    //VictoryWindow vw = new VictoryWindow(FSM, teamA, teamB, teamAwin);
    //    //Application.Run(vw);
    //    if (Convert.ToString(FSM.CurrentState) == Convert.ToString(PlayerStates.Prebattle))
    //        Prebattle(FSM, sl);
    //    return false;
    //}

    void Start()
    {
        C = C.GetComponent<Canvas>();
        Finite_State_Machine FSM = new Finite_State_Machine(PlayerStates.init); //A Finite_State_Machine called FSM is made with the intial state of init
        FSM.AddState(PlayerStates.init);                                        //The init state is added to FSM
        FSM.AddState(PlayerStates.Prebattle);                                   //The Prebattle state is added to FSM
        FSM.AddState(PlayerStates.teamAturn);                                   //The teamAturn state is added to FSM
        FSM.AddState(PlayerStates.teamBturn);                                   //The teamBturn state is added to FSM
        FSM.AddState(PlayerStates.victory);                                     //The victory state is added to FSM

        FSM.AddTransition(PlayerStates.init, PlayerStates.Prebattle);           //The transition from init to Prebattle is added to FSM
        FSM.AddTransition(PlayerStates.Prebattle, PlayerStates.teamAturn);      //The transition from Prebattle to teamAturn is added to FSM
        FSM.AddTransition(PlayerStates.teamAturn, PlayerStates.teamBturn);      //The transition from teamAturn to teamBturn is added to FSM
        FSM.AddTransition(PlayerStates.teamBturn, PlayerStates.teamAturn);      //The transition from teamBturn to teamAturn is added to FSM
        FSM.AddTransition(PlayerStates.teamAturn, PlayerStates.victory);        //The transition from teamAturn to victory is added to FSM
        FSM.AddTransition(PlayerStates.teamBturn, PlayerStates.victory);        //The transition from teamBturn to victory is added to FSM
        FSM.AddTransition(PlayerStates.victory, PlayerStates.Prebattle);        //The transition from victory to Prebattle is added to FSM
        //UnityEngine.Debug.Log(FSM.info());                                                             //Runs the info function of FSM

        init(FSM, C);                                                          //Runs the init function, passing in the FSM Finite_State_Machine

        Console.ReadLine();                                         //Allows the command console to print text written to the command console
    }

    //[STAThread]
    //static void Main(string[] args) //The function the program starts in
    //{
    //    Finite_State_Machine FSM = new Finite_State_Machine(PlayerStates.init); //A Finite_State_Machine called FSM is made with the intial state of init
    //    FSM.AddState(PlayerStates.init);                                        //The init state is added to FSM
    //    FSM.AddState(PlayerStates.Prebattle);                                   //The Prebattle state is added to FSM
    //    FSM.AddState(PlayerStates.teamAturn);                                   //The teamAturn state is added to FSM
    //    FSM.AddState(PlayerStates.teamBturn);                                   //The teamBturn state is added to FSM
    //    FSM.AddState(PlayerStates.victory);                                     //The victory state is added to FSM

    //    FSM.AddTransition(PlayerStates.init, PlayerStates.Prebattle);           //The transition from init to Prebattle is added to FSM
    //    FSM.AddTransition(PlayerStates.Prebattle, PlayerStates.teamAturn);      //The transition from Prebattle to teamAturn is added to FSM
    //    FSM.AddTransition(PlayerStates.teamAturn, PlayerStates.teamBturn);      //The transition from teamAturn to teamBturn is added to FSM
    //    FSM.AddTransition(PlayerStates.teamBturn, PlayerStates.teamAturn);      //The transition from teamBturn to teamAturn is added to FSM
    //    FSM.AddTransition(PlayerStates.teamAturn, PlayerStates.victory);        //The transition from teamAturn to victory is added to FSM
    //    FSM.AddTransition(PlayerStates.teamBturn, PlayerStates.victory);        //The transition from teamBturn to victory is added to FSM
    //    FSM.AddTransition(PlayerStates.victory, PlayerStates.Prebattle);        //The transition from victory to Prebattle is added to FSM
    //    UnityEngine.Debug.Log(FSM.info());                                                             //Runs the info function of FSM

    //    init(FSM);                                                          //Runs the init function, passing in the FSM Finite_State_Machine

    //    Console.ReadLine();                                         //Allows the command console to print text written to the command console
    //}
}
