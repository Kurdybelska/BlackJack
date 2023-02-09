using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Text;

public enum MenuEventType
{
    GoToGame,
    CloseGame,
    ShowChoosePlayersNumberModal,
    ShowEnterPlayerNameModal
}

public class MenuManager : MonoBehaviour
{
    public ChoosePlayersNumberModal choosePlayersNumberModal;
    public EnterPlayerNameModal enterPlayerNameModal;

    public void ReceiveEvent(MenuEventType eventType)
    {
        switch (eventType)
        {
            case MenuEventType.GoToGame:
                StartGame();
                break;
            case MenuEventType.CloseGame:
                CloseGame();
                break;
            case MenuEventType.ShowChoosePlayersNumberModal:
                ShowChoosePlayersNumberModal();
                break;
            case MenuEventType.ShowEnterPlayerNameModal:
                ShowEnterPlayerNameModal();
                break;
            default:
                break;
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene(sceneName: "GameScene");
    }

    private void ShowChoosePlayersNumberModal()
    {
        choosePlayersNumberModal.ShowModal();
    }

    private void ShowEnterPlayerNameModal()
    {
        enterPlayerNameModal.ShowModal();
    }

    public void handleLoadGameButton()
    {
        if (PlayerPrefs.GetInt("gameSaved") == 1)
        {
            PlayerPrefs.SetInt("loadFromSave", 1);
            StartGame();
        }
    }

    public void CloseGame()
    {
        Application.Quit();
        PlayerPrefs.DeleteAll();
    }
}
