using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class masterVol : NetworkBehaviour {

    public Slider slider;
    public void OnValueChanged()
    {
        AudioListener.volume = slider.value;
    }
}
