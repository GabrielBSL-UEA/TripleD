using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    [SerializeField] private Transform initialPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float timeBetweenPoints;

    void Start()
    {
        ToEnd();
        GameManager.Instance.OnRestart.AddListener(ReturnToStart);
    }

    private void ToStart()
    {
        transform.LeanMove(initialPoint.position, timeBetweenPoints)
            .setEaseInOutSine()
            .setOnComplete(() => ToEnd());
    }

    private void ToEnd()
    {
        transform.LeanMove(endPoint.position, timeBetweenPoints)
            .setEaseInOutSine()
            .setOnComplete(() => ToStart());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(initialPoint.position, endPoint.position);
    }

    public void ReturnToStart()
    {
        LeanTween.cancel(gameObject);
        transform.position = initialPoint.position;
        ToEnd();
    }
}
