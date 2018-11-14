using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class newScene : MonoBehaviour {

    public void loadByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

}
