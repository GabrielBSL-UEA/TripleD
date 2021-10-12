using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class FadeInOutEffector : MonoBehaviour
{
    [SerializeField] private float fadeInOutTime = 1;

    private CanvasGroup m_Cg;

    private void Awake()
    {
        TryGetComponent(out m_Cg);

        LeanTween.value(0, 1, fadeInOutTime)
            .setLoopPingPong()
            .setEaseInOutCubic()
            .setOnUpdate((float value) =>
            {
                m_Cg.alpha = value;
            });
    }
}
