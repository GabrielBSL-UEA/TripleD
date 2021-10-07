using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlatform : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        collision.gameObject.TryGetComponent(out PlayerMovement pMovement);
        pMovement.LockPlayer();
    }
}
