using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour
{
    public Player playerPrefab;

    private const sbyte maxNumOfPlayers = 4;
    private sbyte currentPlayerIndex = 0;
    private bool allPlayersPlayedTheirTurns = false;
    private List<Player> players = new List<Player>();

    public List<Player> GetPlayersList() { return players; }

    public void SetPlayersList(List<Player> _players) { players = _players; }

    public bool AllPlayersPlayedTheirTurns() { return allPlayersPlayedTheirTurns; }

    public void Init(sbyte numOfPlayers)
    {
        numOfPlayers = GetCorrectNumOfPlayers(numOfPlayers);
        AddPlayers(numOfPlayers);
    }

    public void HandleDealButton(Deck deck)
    {
        if (allPlayersPlayedTheirTurns) return;

        foreach (Player player in players)
        {
            player.TakeCard(deck);
            player.TakeCard(deck);
        }

        SetFirstPlayerAsActive();
        ChangeCurrentPlayerIfNeeded();
    }

    public void HandleHitButton(Deck deck)
    {
        if (allPlayersPlayedTheirTurns) return;
        
        var currentPlayer = GetCurrentPlayer();
        currentPlayer.TakeCard(deck);

        ChangeCurrentPlayerIfNeeded();
    }

    public void HandleStandButton()
    {
        if (allPlayersPlayedTheirTurns) return;

        var currentPlayer = GetCurrentPlayer();
        currentPlayer.StandClicked();

        ChangeCurrentPlayerIfNeeded();
    }

    public sbyte getCurrentPlayerIndex()
    {
        return currentPlayerIndex;
    }

    private void AddPlayer(sbyte playerNum)
    {
        Player newPlayer = Instantiate(playerPrefab) as Player;
        string playerName = GlobalGameContext.playersNames[playerNum];

        newPlayer.Init(playerNum, playerName);
        players.Add(newPlayer);
    }

    private void AddPlayers(sbyte numOfPlayers)
    {
        for (sbyte playerNum = 0; playerNum < numOfPlayers; ++playerNum) AddPlayer(playerNum);
    }

    private sbyte GetCorrectNumOfPlayers(sbyte numOfPlayers)
    {
        return numOfPlayers > maxNumOfPlayers ? maxNumOfPlayers : numOfPlayers;
    }

    private Player GetCurrentPlayer()
    {
        ChangeCurrentPlayerIfNeeded();
        return players[currentPlayerIndex];
    }

    private void ChangeCurrentPlayerIfNeeded()
    {
        if (!AllPlayersPlayedTheirTurns() && players[currentPlayerIndex].HasFinishedTurn())
        {
            ChangeCurrentPlayerIndex();
            ChangeCurrentPlayerIfNeeded();
        }
    }

    private void SetFirstPlayerAsActive()
    {
        players[0].SetActive();
    }

    private void ChangeCurrentPlayerIndex()
    {
        players[currentPlayerIndex].SetNotActive();
        currentPlayerIndex++;

        if(currentPlayerIndex >= players.Count) allPlayersPlayedTheirTurns = true;
        else players[currentPlayerIndex].SetActive();
    }
}