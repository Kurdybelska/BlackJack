using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public GameObject arrowPrefab;
    public PlayerInfoModal playerInfoModalPrefab;

    private List<Card> cards = new List<Card>();
    private GameObject arrow;
    private PlayerInfoModal playerInfoModal;

    private string nameOfThePlayer;
    private int points = 0;
    private int pointsWithAceCountedAs11 = 0;
    private bool finishedTurn = false;
    private bool wasAceAlreadyPulled = false;

    private const float playerOffsetMultiplier = 4;
    private const float firstCardOffset = -8.0f;
    private const float eachCardOffsetToPreviousOne = 0.5f;
    private float lastCardXOffset = -5.0f;
    private float cardsYOffset = -0.3f;

    public void Init(sbyte playerNum, string playerName)
    {
        InitPlayerName(playerName);
        InitLastCardXOffset(playerNum);
        InitArrow();
        InitPlayerInfoModal();
        SetNotActive();
    }

    public void LoadFromSave(sbyte playerNum, string playerName, bool active, List<List<int>> _cards)
    {
        Init(playerNum, playerName);

        foreach (List<int> cardList in _cards)
        {
            Card card = new Card(cardList);

            cards.Add(card);
            DisplayNewCard(card);
            AddCardPoints(card);
            DecideIfTurnIsFinished();
            UpdatePointsOnPlayerInfoModal();
        }

        if (active) SetActive();

    }

    public string GetName() { return nameOfThePlayer; }
    public int GetPoints() { return points; }
    public void SetPoints(int _points) { points = _points; }
    public void SetCards(List<Card> _cards) { cards = _cards; }

    public bool HasFinishedTurn() { return finishedTurn; }

    public void SetActive()
    {
        arrow.SetActive(true);
        playerInfoModal.ShowModal();
    }

    public void SetNotActive()
    {
        arrow.SetActive(false);
        playerInfoModal.HideModal();
    }

    public void TakeCard(Deck deck)
    {
        Card card = deck.PullCard();
        cards.Add(card);

        DisplayNewCard(card);
        AddCardPoints(card);
        DecideIfTurnIsFinished();
        UpdatePointsOnPlayerInfoModal();
    }

    public void StandClicked()
    {
        FinishTurn();
    }

    public List<List<int>> GetCardsInList()
    {
        List<List<int>> list = new List<List<int>>();
        foreach (Card card in cards)
        {
            list.Add(card.GetAsList());
        }
        return list;
    }

    public List<Card> GetCards()
    {
        return cards;
    }

    private void AddCardPoints(Card card)
    {
        if (card.IsAce() && !wasAceAlreadyPulled)
        {
            pointsWithAceCountedAs11 = points + 11;
            wasAceAlreadyPulled = true;
        }
        else
        {
            pointsWithAceCountedAs11 += card.GetPoints();
        }

        points += card.GetPoints();
    }

    private void FinishTurn()
    {
        finishedTurn = true;
        if (pointsWithAceCountedAs11 <= 21) points = pointsWithAceCountedAs11;    
    }

    private void DecideIfTurnIsFinished()
    {
        if (pointsWithAceCountedAs11 > 21) pointsWithAceCountedAs11 = points;
        else if (pointsWithAceCountedAs11 == 21) points = pointsWithAceCountedAs11;

        if (points >= 21) FinishTurn();
    }

    private void DisplayNewCard(Card card)
    {
        card.SetPosition(lastCardXOffset, cardsYOffset);
        card.SetSortingOrder(cards.Count);
        card.ShowCardFront();
        lastCardXOffset += eachCardOffsetToPreviousOne;
    }

    private void UpdatePointsOnPlayerInfoModal()
    {
        var pointsThatShouldBeOnModal = pointsWithAceCountedAs11 > 21 ? points : pointsWithAceCountedAs11;
        playerInfoModal.SetPlayerPoints(pointsThatShouldBeOnModal);
    }

    private void InitArrow()
    {
        arrow = Instantiate(arrowPrefab) as GameObject;
        arrow.transform.position = new Vector2(lastCardXOffset + 0.5f, cardsYOffset + 0.5f);
    }

    private void InitPlayerInfoModal()
    {
        playerInfoModal = Instantiate(playerInfoModalPrefab) as PlayerInfoModal;
        playerInfoModal.SetPlayerName(nameOfThePlayer);
        playerInfoModal.SetPlayerPoints(0);
    }

    private void InitPlayerName(string playerName)
    {
        nameOfThePlayer = playerName;
    }

    private void InitLastCardXOffset(sbyte playerNum)
    {
        lastCardXOffset = playerNum * playerOffsetMultiplier + firstCardOffset;
    }
}
