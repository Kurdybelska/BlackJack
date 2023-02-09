using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using System.Text;

public class GameManager : MonoBehaviour
{
 
    public Button dealButton;
    public Button hitButton;
    public Button standButton;

    public Deck deck;
    public Dealer dealer;
    public PlayersManager playersManager;
    public EndGameModal endGameModal;
    public CloseGameButton closeGameButton;
    public Player playerPrefab;

    public bool deckReady = false;


    void Start()
    {
        InstantiateFields();
        deck.CreateDeck();
        AddOnClickListeners();
        HidePlayerButtons();

        if (PlayerPrefs.GetInt("loadFromSave") == 1)
        {
            PlayerPrefs.SetInt("loadFromSave", 0);
            LoadGame();
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(sceneName: "MenuScene");
    }

    private void EndGame()
    {
        var dealerPoints = dealer.PlayTurn(deck);
        closeGameButton.HideButton();
        endGameModal.ShowModal();
    }

    private void ShowPlayerButtons()
    {
        hitButton.gameObject.SetActive(true);
        standButton.gameObject.SetActive(true);
    }

    private void HidePlayerButtons()
    {
        hitButton.gameObject.SetActive(false);
        standButton.gameObject.SetActive(false);
    }

    private void HideDealButton()
    {
        dealButton.gameObject.SetActive(false);
    }

    private void TurnOffPlayerButtons()
    {
        hitButton.interactable = false;
        standButton.interactable = false;
    }

    private void TurnOnPlayerButtons()
    {
        hitButton.interactable = true;
        standButton.interactable = true;
    }

    private void DealButtonListenerFunc()
    {
        HideDealButton();
        dealer.HandleDealButton(deck);
        playersManager.HandleDealButton(deck);
        ShowPlayerButtons();

        if(playersManager.AllPlayersPlayedTheirTurns())
        {   
            TurnOffPlayerButtons();
            EndGame();
        }
    }

    private void HitButtonListenerFunc()
    {
        TurnOffPlayerButtons();
        playersManager.HandleHitButton(deck);

        if(!playersManager.AllPlayersPlayedTheirTurns()) TurnOnPlayerButtons();
        else EndGame();
    }

    private void StandButtonListenerFunc()
    {
        TurnOffPlayerButtons();
        playersManager.HandleStandButton();

        if(!playersManager.AllPlayersPlayedTheirTurns()) TurnOnPlayerButtons();
        else EndGame();
    }

    private void AddOnClickListeners()
    {
        dealButton.onClick.AddListener(DealButtonListenerFunc);
        hitButton.onClick.AddListener(HitButtonListenerFunc);
        standButton.onClick.AddListener(StandButtonListenerFunc);
    }

    private void InstantiateFields()
    {
        deck = new Deck();
        dealer = new Dealer();
        playersManager.Init(GlobalGameContext.playersNum);
    }

    private Save CreateSaveGameObject()
    {
        sbyte currentPlayerID = playersManager.getCurrentPlayerIndex();

        List<List<int>> dealersCards = dealer.GetCardsInList();

        List<Player> players = playersManager.GetPlayersList();

        List<List<List<int>>> playersCards = new List<List<List<int>>>();
        int numberOfPlayers = playersCards.Count;
        List<string> playersNames = new List<string>();

        foreach (Player player in players)
        {
            List<List<int>> cards = player.GetCardsInList();
            playersCards.Add(cards);
            string name = player.GetName();
            playersNames.Add(name);
        }

        List<List<int>> deckCards = deck.GetCardsInList();
        int deckCardNumber = deck.GetCardsNumber();

        Save save = new Save(numberOfPlayers, currentPlayerID, playersNames, playersCards, dealersCards, deckCards, deckCardNumber);

        return save;
    }

    public void SaveGame()
    { 
        PlayerPrefs.SetInt("gameSaved", 1);
        Save save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();
    }

    private void SetupSavedGame(int numberOfPlayers, sbyte currentPlayerID, List<string> playersNames, List<List<List<int>>> playersCards, List<List<int>> dealersCards, List<List<int>> _deckCards, int _deckCardNumber)
    {

        List<Player> players = new List<Player>();
        GlobalGameContext.playersNames = playersNames;

        for (sbyte k=0; k<playersNames.Count; k++)
        {
            Player player = Instantiate(playerPrefab) as Player;
            string name = playersNames[k];
            Debug.Log(name);

            List<List<int>> cards = playersCards[k];
            bool active = false;
            Debug.Log(active);

            if (k == currentPlayerID) active = true;

            player.LoadFromSave(k, name, active, cards);
            players.Add(player);
            Debug.Log(player);

        }
        playersManager.SetPlayersList(players);

        dealer = new Dealer();
        dealer.LoadFromSave(dealersCards);

        deck = new Deck(_deckCards, _deckCardNumber);

        Debug.Log("Game loaded");
    }

    private void LoadGame()
    {

        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            Save save = (Save)bf.Deserialize(file);
            file.Close();

            SetupSavedGame(save.numberOfPlayers, save.currentPlayerID, save.playersNames, save.playersCards, save.dealersCards, save.deckCards, save.deckCardNumber);
            HideDealButton();
            ShowPlayerButtons();
        }
        else
        {
            Debug.Log("No game saved!");
        }

    }


}
