using UnityEngine;

public class AudioToPlay : MonoBehaviour
{
    [SerializeField] private AudioClip music;

    void Start() {
        AudioManager.Instance.PlayMusic(music);
    }
}
