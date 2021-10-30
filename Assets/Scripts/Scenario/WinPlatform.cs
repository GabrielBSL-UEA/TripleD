using UnityEngine;

public class WinPlatform : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerController.Instance.SetMovement(false);
        UIManager.Instance.ActivateVictoryPanel();
    }
}
