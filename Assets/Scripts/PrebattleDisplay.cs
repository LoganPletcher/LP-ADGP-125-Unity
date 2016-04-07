using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PrebattleDisplay : MonoBehaviour {
    private Party partyA;
    public Canvas C;
    public Image NBB;
    public Text TT;
    public Button RB;
    public Button QB;
    public Button SB;
    public Button LB;
    public Button GB;
    public InputField PI;
    

    public void CreateParty(Party partyA)
    {
        this.partyA = partyA;
    }

    // Use this for initialization
    public void Start (){
        C = C.GetComponent<Canvas>();
        NBB = NBB.GetComponent<Image>();
        TT = TT.GetComponent<Text>();
        RB = RB.GetComponent<Button>();
        QB = QB.GetComponent<Button>();
        SB = SB.GetComponent<Button>();
        LB = LB.GetComponent<Button>();
        GB = GB.GetComponent<Button>();
        PI = PI.GetComponent<InputField>();
        TT.text = "Load an existing part?";
        RB.gameObject.SetActive(false);
        QB.gameObject.SetActive(false);
        SB.gameObject.SetActive(false);
        PI.gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {

	}

    public void LoadClick()
    {
        PI.gameObject.SetActive(true);
    }

    public void PlayerInputEnd()
    {
        string Filename = PI.text;
        Save_and_Load<Party> sl = new Save_and_Load<Party>();
        Party LoadedTeam = sl.Load(Filename);
        foreach(Unit u in LoadedTeam.Members)
        {
            partyA.Members.Add(u);
        }
        PI.gameObject.SetActive(false);
        this.enabled = false;
    }

}