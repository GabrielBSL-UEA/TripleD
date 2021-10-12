using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private bool m_GameStarted = false;

    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Keyboard.current.anyKey.wasPressedThisFrame || m_GameStarted) return;

        m_GameStarted = true;
        UIManager.Instance.SetMainCanvas(false);
        PlayerController.Instance.SetMovement(true);
    }
}
