using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public int numberOfPlayers = 0;
    public sbyte currentPlayerID = 0;

    public List<string> playersNames;
    public List<List<List<int>>> playersCards;
    public List<List<int>> dealersCards;
    public List<List<int>> deckCards;
    public int deckCardNumber = 0;


    public Save(int _numberOfPlayers, sbyte _currentPlayerID, List<string> _playersNames, 
        List<List<List<int>>> _playersCards, List<List<int>> _dealersCards, List<List<int>> _deckCards, int _deckCardNumber)
    {
        numberOfPlayers = _numberOfPlayers;
        currentPlayerID = _currentPlayerID;
        playersNames = _playersNames;
        playersCards = _playersCards;
        dealersCards = _dealersCards;
        deckCards = _deckCards;
        deckCardNumber = _deckCardNumber;
    }

}
