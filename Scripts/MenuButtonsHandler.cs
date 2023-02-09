using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonsHandler : MonoBehaviour
{
    public MenuManager menuManager;

    private void SendEventToMenuManager(MenuEventType eventType)
    {
        menuManager.ReceiveEvent(eventType);
    }

    public void ShowChoosePlayersNumberModal()
    {
        SendEventToMenuManager(MenuEventType.ShowChoosePlayersNumberModal);
    }

    public void QuitGame()
    {
        SendEventToMenuManager(MenuEventType.CloseGame);
    }
}
