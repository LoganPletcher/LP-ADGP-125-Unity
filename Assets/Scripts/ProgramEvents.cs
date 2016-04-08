using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ProgramEvents : MonoBehaviour {
    private bool TeamABuilt = false;
    private Party partyA;
    private Party partyB;
    private Finite_State_Machine FSM;
    public Canvas C;
    public Image NBB;
    public Text TT;
    public Button RB;
    public Button QB;
    public Button SB;
    public Button LB;
    public Button GB;
    public InputField PI;

    public Image BB;
    public Button A1;
    public Button A2;
    public Button A3;
    public Image AP1;
    public Image AP2;
    public Image AP3;
    public Image BP1;
    public Image BP2;
    public Image BP3;
    public Text BE;
    public Button CB;

    public void LoadInformation(Finite_State_Machine FSM, Party partyA, Party partyB)
    {
        this.FSM = FSM;
        this.partyA = partyA;
        this.partyB = partyB;
    }

    void PartyCreation()
    {
        TeamABuilt = (partyA.Members.Count >= 3) ? true : false;
        if (!TeamABuilt)
            partyA = new Party(PartyType.CHARACTER, 1);
        foreach (Unit u in partyA.Members)
        {
            UnityEngine.Debug.Log(u);
        }
        int sum = 0;
        foreach (Unit u in partyA.Members)
            sum += u.Level;

        int avgLvl = sum / partyA.Members.Count;
        partyB = new Party(PartyType.ENEMY, avgLvl);

        FSM.ChangeStates("Prebattle->teamAturn");
        Debug.Log(FSM.CurrentState);
        this.gameObject.SetActive(false);
    }

    // Use this for initialization
    public void Start (){
        C = C.GetComponent<Canvas>();
        NBB = NBB.GetComponent<Image>();
        BB = BB.GetComponent<Image>();
        TT = TT.GetComponent<Text>();
        RB = RB.GetComponent<Button>();
        QB = QB.GetComponent<Button>();
        SB = SB.GetComponent<Button>();
        LB = LB.GetComponent<Button>();
        GB = GB.GetComponent<Button>();
        PI = PI.GetComponent<InputField>();
        TT.text = "Load an existing party?";
        BB.gameObject.SetActive(false);
        RB.gameObject.SetActive(false);
        QB.gameObject.SetActive(false);
        SB.gameObject.SetActive(false);
        PI.gameObject.SetActive(false);
        A1 = A1.GetComponent<Button>();
        A2 = A2.GetComponent<Button>();
        A3 = A3.GetComponent<Button>();
        AP1 = AP1.GetComponent<Image>();
        AP2 = AP2.GetComponent<Image>();
        AP3 = AP3.GetComponent<Image>();
        BP1 = BP1.GetComponent<Image>();
        BP2 = BP2.GetComponent<Image>();
        BP3 = BP3.GetComponent<Image>();
        BE = BE.GetComponent<Text>();
        CB = CB.GetComponent<Button>();
    }
	
	// Update is called once per frame
	void Update () {
        NBB.gameObject.SetActive(true);
	}

    public void LoadClick()
    {
        TT.text = "Type in the filename.";
        PI.gameObject.SetActive(true);
    }

    public void PlayerInputEnd()
    {
        string Filename = PI.text;
        Save_and_Load<Party> sl = new Save_and_Load<Party>();
        Party LoadedTeam = sl.Load(Filename);
        if (LoadedTeam.Members.Count > 0)
        {
            foreach (Unit u in LoadedTeam.Members)
            {
                partyA.Members.Add(u);
            }
            PartyCreation();
        }
        PI.gameObject.SetActive(false);
        TT.text = "Load an existing party?";

    }

    public void StartGameClick()
    {
        PartyCreation();
    }

}