using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlatform : MonoBehaviour
{
    [SerializeField] private GameObject winCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        PlayerController.Instance.SetMovement(false);
        winCanvas.SetActive(true);
    }
}
