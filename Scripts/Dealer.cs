using System;
using System.Collections.Generic;
using UnityEngine;

public class Dealer
{
    const sbyte stopThreshold = 17;
    const sbyte valueOfFirstAceBeforeBust = 11;

    float lastCardXOffset = -3.0f;
    float cardsYOffset = 3.0f;

    List<Card> cards = new List<Card>();
    int points = 0;
    bool stand = false;
    bool wasAceAlreadyPulled = false;
    bool wasFirstCardPulled = false;

    public int GetPoints() { return points; }
    public void SetPoints(int _points) { points = _points; }
    public void SetCards(List<Card> _cards) { cards = _cards; }

    public int PlayTurn(Deck deck)
    {
        ShowFirstCard();
        while (!stand)
        {
           TakeCard(deck);
        }
        SaveDealerPointsToGlobalContext();
        return points;
    }

    public void LoadFromSave(List<List<int>> _cards)
    {

        foreach (List<int> cardList in _cards)
        {
            Card card = new Card(cardList);

            points += getPointsOfPulledCard(card);
            DisplayNewCard(card);
            cards.Add(card);

            wasFirstCardPulled = true;
        }

    }

    public void HandleDealButton(Deck deck)
    {
        TakeCard(deck);
        TakeCard(deck);
    }

    public List<Card> GetCards()
    {
        return cards;
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

    private void SaveDealerPointsToGlobalContext()
    {
        GlobalGameContext.dealerPoints = GetPoints();
    }

    private void TakeCard(Deck deck)
    {
        if (points > stopThreshold)
        {
            stand = true;
            return;
        }

        Card card = deck.PullCard();
        points += getPointsOfPulledCard(card);
        DisplayNewCard(card);
        cards.Add(card);

        wasFirstCardPulled = true;
    }
    
    private void DisplayNewCard(Card card)
    {
        lastCardXOffset += 0.5f;
        card.SetPosition(lastCardXOffset, cardsYOffset);
        card.SetSortingOrder(cards.Count);

        if (wasFirstCardPulled)
        {
            card.ShowCardFront();
        }
    }

    private int getPointsOfPulledCard(Card card)
    {
        if (card.IsAce())
        {
            return getPointsOfPulledAce();
        }
        else return card.GetPoints();
    }

    private int getPointsOfPulledAce()
    {
        int pointsOfPulledAce = 1;

        // Dealer's first aces is 11, unless it busts.
        if (!wasAceAlreadyPulled && points + valueOfFirstAceBeforeBust <= 21)
        {
            pointsOfPulledAce = valueOfFirstAceBeforeBust;
        }

        wasAceAlreadyPulled = true;
        return pointsOfPulledAce;
    }

    private void ShowFirstCard()
    {
        cards[0].ShowCardFront();
    }
}