using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class BackToMenu : NetworkBehaviour {

    public void returnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
