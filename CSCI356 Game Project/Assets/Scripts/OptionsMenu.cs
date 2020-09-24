using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;

    public void setSensitivity(float Sensitivity)
    {
        PlayerController.turnSpeed = Sensitivity;
    }

    public void setVolume (float Sound)
    {
        audioMixer.SetFloat("MasterVolume", Sound);
    }
}
