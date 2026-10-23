using UnityEngine;

public class AudioToPlay : MonoBehaviour
{
    [SerializeField] private AudioClip music;
    [SerializeField] private AudioClip button_click_audio;

    void OnEnable()
    {
        AudioBus.PlayButtonClickAudio +=  PlayClick;
    }
    void Start() {
        AudioManager.Instance.PlayMusic(music);
        
    }
    void PlayClick()
    {
        AudioManager.Instance.PlaySFX(button_click_audio);
    }
}
