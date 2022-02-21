using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interaction : MonoBehaviour
{
    [SerializeField]
    InteractionInfo info;
    [SerializeField]
    string sceneName;
    [SerializeField]
    GameObject gameObjectUI;
    [SerializeField]
    DoorController doorController;

    public bool IsInteracting { get; set; }
    public InteractionInfo Info { get { return info; } }

    public void Awake()
    {
        IsInteracting = false;
    }

    public void Start()
    {
        if(gameObjectUI != null)
            gameObjectUI.SetActive(false);
    }

    public void Update()
    {
        if(gameObjectUI != null)
            IsInteracting = gameObjectUI.activeSelf;
    }

    public void Interact()
    {
        switch(info)
        {
            case InteractionInfo.MoveScene:
                SceneManager.LoadScene(sceneName);
                break;
            case InteractionInfo.ShowUI:
                gameObjectUI.SetActive(true);
                break;
            case InteractionInfo.Door:
                doorController.Open();
                break;
            case InteractionInfo.Nothing:
                break;
        }
    }
}

public enum InteractionInfo
{
    Nothing,
    MoveScene,
    ShowUI,
    Door,
}