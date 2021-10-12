using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject mainCanvas;

    private void Awake()
    {
        Instance = this;
    }

    public void SetMainCanvas(bool value) => mainCanvas.SetActive(value);
}
