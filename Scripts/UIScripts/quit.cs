using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class quit : NetworkBehaviour {

    public void Quit()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
