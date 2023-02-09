using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameModal : MonoBehaviour
{
    public GameManager gameManager;
    public PlayersManager playersManager;
    public PlayerInfoBox playerInfoBoxPrefab;

    void Start()
    {
        HideModal();
    }

    public void ShowModal()
    {
        this.gameObject.SetActive(true);
        WriteDealerInfo();
        WritePlayersInfo();
    }

    public void HideModal()
    {
        this.gameObject.SetActive(false);        
    }

    public void OnBackToMenuButtonClicked()
    {
        gameManager.GoToMenu();
    }

    private void WriteDealerInfo()
    {
        int dealerPoints = GlobalGameContext.dealerPoints;
        AddPlayerInfoBox("Krupier", dealerPoints);
    }

    private void WritePlayersInfo()
    {
        var players = playersManager.GetPlayersList();

        foreach (var player in players)
        {
            var playerName = player.GetName();
            var points = player.GetPoints();
            if (IsPlayerWinner(points)) {
                playerName = "WIN: " + playerName;
            } else {
                playerName = "LOSE: " + playerName;
            }
            AddPlayerInfoBox(playerName, points);
        }
    }

    private void AddPlayerInfoBox(string playerName, int playerPoints)
    {
        PlayerInfoBox infoBox = Instantiate(playerInfoBoxPrefab) as PlayerInfoBox;
        infoBox.Init(playerName, playerPoints);
    }

    private bool IsPlayerWinner(int points)
    {
        int dealerPoints = GlobalGameContext.dealerPoints;

        if (points > 21) return false;
        else if (dealerPoints > 21) return true;
        else if (points >= dealerPoints) return true;
        else return false;
    }

}
