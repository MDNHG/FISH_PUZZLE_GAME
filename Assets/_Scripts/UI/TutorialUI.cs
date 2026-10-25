using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class TutorialUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private Button tutorialButton;
    [SerializeField] private Button doneButton;

    [Header("Animation")]
    [SerializeField] private CanvasGroup backgroundGroup;
    [SerializeField] private CanvasGroup videoGroup;
    [SerializeField] private CanvasGroup doneButtonGroup;
    [SerializeField] private RectTransform videoContainer;
    [SerializeField] private float transitionDuration = 0.3f;

    [Header("Video")]
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private VideoClip tutorialVideo;

    private bool isTutorialOpen;
    private Coroutine transitionCoroutine;
    private float previousTimeScale = 1f;
    

    private void Awake()
    {
        // Set up UI button events.
        tutorialButton.onClick.AddListener(ShowTutorial);
        doneButton.onClick.AddListener(HideTutorial);
        videoContainer.localScale = Vector3.one * 0.9f;
        tutorialPanel.SetActive(false);
        bool hasTuto = HasTutorial();

        tutorialButton.gameObject.SetActive(hasTuto);
        if (hasTuto) videoPlayer.clip = tutorialVideo;

        backgroundGroup.alpha = 0f;
        videoGroup.alpha = 0f;
        doneButtonGroup.alpha = 0f;
        videoGroup.interactable = false;
        videoGroup.blocksRaycasts = false;
        doneButtonGroup.interactable = false;
        doneButtonGroup.blocksRaycasts = false;

        
    }

    /*private void Start()
    {
        // Show tutorial automatically when the level loads.
        if (HasTutorial()) ShowTutorial();
    }*/
    private IEnumerator Start()
    {
        if (!HasTutorial())
            yield break;

        // Allow the scene to render first.
        yield return null;

        // Small delay for a more noticeable transition.
        yield return new WaitForSecondsRealtime(0.15f);

        ShowTutorial();
    }

    private bool HasTutorial()
    {
        return tutorialVideo != null;
    }

    private void StartTransition(bool opening)
    {
        if (transitionCoroutine != null)
            StopCoroutine(transitionCoroutine);

        transitionCoroutine =
            StartCoroutine(AnimateTransition(opening));
    }

    private IEnumerator AnimateTransition(bool opening)
    {
        float startBackgroundAlpha = backgroundGroup.alpha;
        float startVideoAlpha = videoGroup.alpha;
        float startDoneAlpha = doneButtonGroup.alpha;
        Vector3 startScale = videoContainer.localScale;

        float targetAlpha = opening ? 1f : 0f;
        Vector3 targetScale = opening
            ? Vector3.one
            : Vector3.one * 0.9f;

        float elapsed = 0f;
        float duration = Mathf.Max(0.001f, transitionDuration);

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;

            float t = Mathf.Clamp01(elapsed / duration);

            // Smoothstep easing.
            t = t * t * (3f - 2f * t);

            backgroundGroup.alpha =
                Mathf.Lerp(startBackgroundAlpha, targetAlpha, t);

            videoGroup.alpha =
                Mathf.Lerp(startVideoAlpha, targetAlpha, t);

            doneButtonGroup.alpha =
                Mathf.Lerp(startDoneAlpha, targetAlpha, t);

            videoContainer.localScale =
                Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }

        backgroundGroup.alpha = targetAlpha;
        videoGroup.alpha = targetAlpha;
        doneButtonGroup.alpha = targetAlpha;
        videoContainer.localScale = targetScale;

        if (opening)
        {
            videoGroup.interactable = true;
            videoGroup.blocksRaycasts = true;
            doneButtonGroup.interactable = true;
            doneButtonGroup.blocksRaycasts = true;
        }
        else
        {
            tutorialPanel.SetActive(false);
            tutorialButton.gameObject.SetActive(true);
            Time.timeScale = previousTimeScale;
        }

        transitionCoroutine = null;
    }

    private void ClearRenderTexture()
    {
        RenderTexture texture = videoPlayer.targetTexture;

        if (texture == null) return;

        RenderTexture previous = RenderTexture.active;

        RenderTexture.active = texture;
        GL.Clear(true, true, Color.black);
        RenderTexture.active = previous;
    }

    public void ShowTutorial()
    {
        if (isTutorialOpen || !HasTutorial()) return;

        isTutorialOpen = true;

        // Pause gameplay.
        previousTimeScale = Time.timeScale;
        Time.timeScale = 0f;

        tutorialPanel.SetActive(true);
        tutorialButton.gameObject.SetActive(false);

        // Start video from the beginning.
        videoPlayer.Stop();

        ClearRenderTexture(); // Remove the previous frame
        
        StartTransition(true);

        videoPlayer.Play();
    }

    public void HideTutorial()
    {
        if (!isTutorialOpen) return;

        isTutorialOpen = false;

        videoPlayer.Stop();

        videoGroup.interactable = false;
        videoGroup.blocksRaycasts = false;

        StartTransition(false);

        //tutorialPanel.SetActive(false);
        //tutorialButton.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        if (isTutorialOpen || transitionCoroutine != null)
        {
            Time.timeScale = previousTimeScale;
        }

        tutorialButton.onClick.RemoveListener(ShowTutorial);
        doneButton.onClick.RemoveListener(HideTutorial);
    }
}
