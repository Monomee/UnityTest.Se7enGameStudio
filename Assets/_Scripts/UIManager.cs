using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    private void OnEnable()
    {
        Instance = this;
    }
    private void OnDisable()
    {
        Instance = null;
    }

    public Button kickButton;
    public Button autoKickButton;

    public void SetActiveKickButton(bool isInteractable) => kickButton.interactable = isInteractable;
    public void SetActiveAutoKickButton(bool isInteractable) => autoKickButton.interactable = isInteractable;
}
