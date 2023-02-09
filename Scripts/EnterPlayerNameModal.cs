using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterPlayerNameModal : MonoBehaviour
{
    private enum ProcessingPlayerNameResult
    {
        DidNotProcessAnyPlayerNamePreviously,
        PlayerSuccessfullyAdded,
        PlayerAlreadyExist
    };

    public MenuManager menuManager;

    public TMP_InputField inputField;
    public TextMeshProUGUI inputFieldPlaceholder;
    public TextMeshProUGUI nextButtonText;

    void Start() {
        PrepareModalFields();
    }

    public void ShowModal()
    {
        this.gameObject.SetActive(true);
        PrepareModalFields();
        ClearAllPreviouslyColectedPlayersNames();
    }

    public void HideModal()
    {
        GlobalGameContext.playersNames.Clear();
        this.gameObject.SetActive(false);
    }

    public void OnNextButtonClicked()
    {
        var processingResult = ProcessCurrentPlayerName();

        if (AllPlayersWereProcessed()) StartGame();
        else PrepareModalFields(processingResult);
    }

    private void StartGame()
    {
        menuManager.ReceiveEvent(MenuEventType.GoToGame);
    }

    private void PrepareModalFields(ProcessingPlayerNameResult processingPlayerNameResult = ProcessingPlayerNameResult.DidNotProcessAnyPlayerNamePreviously)
    {
        PrepareInputField(processingPlayerNameResult);
        PrepareNextButton();
    }

    private void PrepareInputField(ProcessingPlayerNameResult processingPreviousPlayerResult)
    {
        string currentPlayerNum = (GetProcessedPlayersNum() + 1).ToString();
        inputField.text = "";

        inputFieldPlaceholder.text = processingPreviousPlayerResult == ProcessingPlayerNameResult.PlayerAlreadyExist
            ? "Wprowadzony gracz już istnieje, wprowadź nazwę gracza #" + currentPlayerNum
            : "Wprowadź nazwę gracza #" + currentPlayerNum;
    }

    private void PrepareNextButton()
    {
        nextButtonText.text = IsProcessingLastPlayer() ? "Start gry" : "Dalej";
    }

    private int GetProcessedPlayersNum()
    {
        return GlobalGameContext.playersNames.Count;
    }

    private bool AllPlayersWereProcessed()
    {
        return GetProcessedPlayersNum() == GlobalGameContext.playersNum;
    }

    private bool IsProcessingLastPlayer()
    {
        return GetProcessedPlayersNum() + 1 == GlobalGameContext.playersNum;
    }

    private void ClearAllPreviouslyColectedPlayersNames()
    {
        GlobalGameContext.playersNames.Clear();
    }

    private ProcessingPlayerNameResult ProcessCurrentPlayerName()
    {
        string playerName = inputField.text;
        bool playerNameAlreadExist = GlobalGameContext.playersNames.Contains(playerName);

        if (!playerNameAlreadExist) GlobalGameContext.playersNames.Add(playerName);

        return playerNameAlreadExist
            ? ProcessingPlayerNameResult.PlayerAlreadyExist
            : ProcessingPlayerNameResult.PlayerSuccessfullyAdded;
    }
}
