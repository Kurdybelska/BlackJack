using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Deck
{
    public List<Card> cards;
    public int cardsNumber = 208;


    public Deck(List<List<int>> _cards, int _cardsNumber)
    {
        cards = new List<Card>();
        cardsNumber = _cardsNumber;

        foreach (List<int> cardList in _cards)
        {
            Card card = new Card(cardList);
            cards.Add(card);
        }
    }

    public Deck()
    {
        CreateDeck();
    }

    public void CreateDeck()
    {
        cards = _ShuffleCards(_CreateCards());
    }

    public List<Card> GetCards() { return cards; }
    public int GetCardsNumber() { return cardsNumber; }

    public List<List<int>> GetCardsInList()
    {
        List<List<int>> list = new List<List<int>>();
        foreach (Card card in cards)
        {
            list.Add(card.GetAsList());
        }
        return list;
    }

    public Card PullCard()
    {
        cardsNumber--;
        Card card = cards[cardsNumber];
        cards.RemoveAt(cardsNumber);

        return card;
    }


    private List<Card> _CreateCards()
    {
        List<Card> cards = new List<Card>();

        // 4 decks of cards
        for (int k = 0; k < 4; k++)
        {
            // iterate over every color
            var suits = Enum.GetValues(typeof(Suit));
            var figures = Enum.GetValues(typeof(Figure));

            foreach (Suit suit in suits)
            {
                foreach (Figure figure in figures)
                {
                    Card newCard = new Card(suit, figure);
                    cards.Add(newCard);
                }
            }
        }

        return cards;
    }



    private string GetSuitePath(Suit suit)
    {
        switch (suit)
        {
            case Suit.CLUB: return "Sprites/clubs";
            case Suit.DIAMOND: return "Sprites/diamonds";
            case Suit.HEART: return "Sprites/hearts";
            case Suit.SPADE: return "Sprites/spades";
            default:
                Debug.Log("Wrong suit passed");
                return "";
        }
    }

    private List<Card> _ShuffleCards(List<Card> cards)
    {
        System.Random rd = new System.Random();
        List<Card> shuffledCards = new List<Card>();

        for (int k = 0; k < cardsNumber; k++)
        {
            int rand_num = rd.Next(0, cardsNumber-k);
            //cards[rand_num].Print();

            shuffledCards.Add(cards[rand_num]);
            cards.RemoveAt(rand_num);
        }

        return shuffledCards;
    }


}
