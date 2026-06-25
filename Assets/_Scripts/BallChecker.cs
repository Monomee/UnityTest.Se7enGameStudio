using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallChecker : MonoBehaviour
{
    [SerializeField] private GameObject kickButton;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;
        kickButton.SetActive(true);
        BallController.instance.ball = other.transform;
    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Ball")) return;
        kickButton.SetActive(false);
        BallController.instance.ball = null;
    }
}
