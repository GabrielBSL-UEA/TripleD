using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private List<GameObject> canvasList;

    [Header("Panels")]
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject hudPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject victoryPanel;

    [Header("Buttons")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button reloadLevelButton;
    [SerializeField] private Button quitGameButton;

    private enum panels
    {
        start,
        hud,
        pause,
        victory
    }

    private void Awake()
    {
        if(Instance != null)
        {
            foreach (GameObject canvas in canvasList) Destroy(canvas);

            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        foreach (GameObject canvas in canvasList) DontDestroyOnLoad(canvas);

        resumeButton.onClick.AddListener(() => SetPause());
        pauseButton.onClick.AddListener(() => SetPause());

        nextLevelButton.onClick.AddListener(() => StartLevelLoading(1, true));
        reloadLevelButton.onClick.AddListener(() => StartLevelLoading(0, true));

        quitGameButton.onClick.AddListener(() => StartGameQuitting());

        SetActivePanel(panels.start);
    }

    public void SetCanvasToGameplay() => SetActivePanel(panels.hud);
    public void ActivateVictoryPanel() => SetActivePanel(panels.victory);

    private void SetPause()
    {
        GameManager.Instance.ChangePauseGameValue();

        var panelToActivate = GameManager.Instance.m_Paused ? panels.pause : panels.hud;
        SetActivePanel(panelToActivate);
    }

    private void SetActivePanel(panels panel)
    {
        startPanel.SetActive(panel == panels.start);
        hudPanel.SetActive(panel == panels.hud);
        pausePanel.SetActive(panel == panels.pause);
        victoryPanel.SetActive(panel == panels.victory);
    }

    private void StartLevelLoading(int index, bool getCurrentIndex) => GameManager.Instance.LoadLevel(index, getCurrentIndex);
    private void StartGameQuitting() => GameManager.Instance.QuitGame();
    
}