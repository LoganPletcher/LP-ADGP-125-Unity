using UnityEngine;
using System.Collections;

public class PrebattleDisplay : MonoBehaviour {
    private Party partyA;
    Canvas c;

    public PrebattleDisplay(Party partyA)
    {
        this.partyA = partyA;
    }

    // Use this for initialization
    void Start (){
        c = c.GetComponent<Canvas>();
    }
	
	// Update is called once per frame
	void Update () {

	}

    private void Yes_Click()
    {
        Save_and_Load<Party> sl = new Save_and_Load<Party>();
        //Party LoadedTeam = sl.Load();
        //foreach (Unit u in LoadedTeam.Members)
        //{
        //    partyA.Members.Add(u);
        //}
    }

}