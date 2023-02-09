using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGameModal : MonoBehaviour
{
    public CloseGameButton closeGameButton;
    public GameManager gameManager;

    public void ShowModal()
    {
        this.gameObject.SetActive(true);
    }

    public void HideModal()
    {
        this.gameObject.SetActive(false);
    }

    public void OnCloseModalButton()
    {
        HideModal();
        closeGameButton.ShowButton();
    }

    public void OnSaveAndExitButton()
    {
        HideModal();
        gameManager.SaveGame();
        gameManager.GoToMenu();
    }

    public void OnExitWithoutSavingButton()
    {
        HideModal();
        gameManager.GoToMenu();
    }
}
