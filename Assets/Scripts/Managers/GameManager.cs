using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool m_Paused { get; private set; } = false;

    private bool m_GameStarted = false;

    public UnityEvent OnRestart;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Keyboard.current.anyKey.wasPressedThisFrame || m_GameStarted) return;

        m_GameStarted = true;
        UIManager.Instance.SetCanvasToGameplay();
        PlayerController.Instance.SetMovement(true);
    }

    public void ChangePauseGameValue()
    {
        m_Paused = !m_Paused;
        Time.timeScale = m_Paused ? 0 : 1;
    }

    public void LoadLevel(int index, bool currentSceneReference)
    {
        int indexReference = currentSceneReference ? SceneManager.GetActiveScene().buildIndex : 0;
        int nextScene = indexReference + index;

        if(nextScene >= SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        
        SceneManager.LoadScene(nextScene);
    }

    private void OnLevelLoad(Scene arg0, LoadSceneMode arg1)
    {
        UIManager.Instance.SetCanvasToGameplay();
        PlayerController.Instance.SetMovement(true);
    }

    public void RestartLevel()
    {
        OnRestart?.Invoke();
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif

        Application.Quit();
    }
}
