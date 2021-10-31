using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeInOutEffector : MonoBehaviour
{
    [SerializeField] private float fadeInOutTime = 1;

    private CanvasGroup m_Cg;

    private void Awake()
    {
        TryGetComponent(out m_Cg);
    }

    private void OnEnable()
    {
        LeanTween.alphaCanvas(m_Cg, 0, fadeInOutTime)
            .setLoopPingPong()
            .setIgnoreTimeScale(true)
            .setEaseInOutCubic();
    }

    private void OnDisable()
    {
        LeanTween.cancel(gameObject);
        m_Cg.alpha = 1;
    }
}
