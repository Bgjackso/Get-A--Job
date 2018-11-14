// dis shit useless right now boi
using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class GameOver : NetworkBehaviour {

    //ask brandom about for the script
    public GameObject losingPanel;
    public GameObject winningPanel;
    
    public static bool redWinner, blueWinner;

	// Use this for initialization
	void Awake () {
        redWinner = false;
        blueWinner = false;

        losingPanel.SetActive(false);
        winningPanel.SetActive(false);
	
	}
	
	// Update is called once per frame
	void Update () {


	
	}
   public static void setWinner(string winner)
    {

    }
}
