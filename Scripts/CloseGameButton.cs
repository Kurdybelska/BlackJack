using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGameButton : MonoBehaviour
{
    public QuitGameModal quitGameModal;

    public void ShowButton()
    {
        this.gameObject.SetActive(true);
    }

    public void HideButton()
    {
        this.gameObject.SetActive(false);
    }

    public void OnButtonClicked()
    {
        HideButton();
        quitGameModal.ShowModal();
    }
}
