using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private PlayerMovement m_PlayerMovement;

    private void Awake()
    {
        Instance = this;

        TryGetComponent(out m_PlayerMovement);
    }

    public void SetMovement(bool value)
    {
        m_PlayerMovement.UnlockPlayer(value);
    }
}
