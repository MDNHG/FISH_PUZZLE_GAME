using UnityEngine;

public class AudioToPlay : MonoBehaviour
{
    [SerializeField] private AudioClip music;
    [SerializeField] private AudioClip button_click_audio;
    [SerializeField] private AudioClip change_option_audio;

    void OnEnable()
    {
        AudioBus.PlayButtonClickAudio +=  PlayClick;
        AudioBus.PlayChangeOptionAudio += PlayChangeOption;
    }
    void Start() {
        AudioManager.Instance.PlayMusic(music);
        
    }
    void PlayClick()
    {
        AudioManager.Instance.PlaySFX(button_click_audio);
    }
    void PlayChangeOption()
    {
        AudioManager.Instance.PlaySFX(change_option_audio);
    }
}
