using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource source;

    void Update()
    {
        if (ButtonManager.isMuted)
            GetComponent<AudioSource>().volume = 0f; 
        else
            GetComponent<AudioSource>().volume = 0.5f;

}
}
