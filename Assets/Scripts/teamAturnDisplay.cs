using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class teamAturnDisplay : MonoBehaviour {
    private Party partyA;
    private Party partyB;
    private Finite_State_Machine FSM;
    public Canvas C;
    public Image NBB;
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

    public void EnableScript()
    {
        this.gameObject.SetActive(true);
    }

    public void DisableScript()
    {
        this.gameObject.SetActive(false);
    }

    // Use this for initialization
    public void Start () {
        C = C.GetComponent<Canvas>();
        NBB = NBB.GetComponent<Image>();
        BB = BB.GetComponent<Image>();
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
        CB = CB.GetComponent<Button>();    }
	
	// Update is called once per frame
	void Update () {
        UnityEngine.Debug.Log("booty");
        NBB.gameObject.SetActive(false);
        BB.gameObject.SetActive(true);
        CB.gameObject.SetActive(false);
	}
}
