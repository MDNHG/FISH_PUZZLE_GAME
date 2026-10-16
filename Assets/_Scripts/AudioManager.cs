using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance {
        get; private set;
    }

    [Header("---- Audio Sources ----")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("---- Audio Clips ----")]
    public AudioClip bgm;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        if (bgm != null) {
            PlayMusic(bgm);
        }
    }

    public void PlayMusic(AudioClip clip) {
        if (musicSource.clip == clip && musicSource.isPlaying)
            return;

        musicSource.clip = clip;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip) {
        // Allows sound effects to overlap without cutting each other off
        sfxSource.PlayOneShot(clip);
    }
}