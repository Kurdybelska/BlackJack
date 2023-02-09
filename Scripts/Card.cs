using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Card
{
    const int acePoints = 1;


    private Suit suit;
    private Figure figure;
    private int points;
    private GameObject gameObject;
    private SpriteRenderer spriteRenderer;
    private Sprite frontImg;
    private Sprite backImg;


    public Card(List<int> list)
    {
        Figure _figure = (Figure)list[0];
        Suit _suit = (Suit)list[1];

        InitializeFields(_suit, _figure);
        SetPosition(5.5f, 2);
        SetScale(1.5f);
        ShowCardBack();
    }

    public Card(Suit passedSuit, Figure passedFigure)
    {
        InitializeFields(passedSuit, passedFigure);
        SetPosition(5.5f, 2);
        SetScale(1.5f);
        ShowCardBack();
    }

    public void ShowCardFront() { spriteRenderer.sprite = frontImg; }
    public void ShowCardBack() { spriteRenderer.sprite = backImg; }

    public void SetPosition(float xCoordinate, float yCoordinate)
    {
        gameObject.transform.position = new Vector2(xCoordinate, yCoordinate);
    }
    public void SetScale(float scale)
    {
        gameObject.transform.localScale = new Vector2(scale, scale);
    }
    public void SetSortingOrder(int sortingOrder)
    {
        spriteRenderer.sortingOrder = sortingOrder;
    }

    public bool IsAce() { return figure == Figure.ACE; }
    public int GetPoints() { return points; }
    private int GetFigurePoints(Figure _figure)
    {
        switch (_figure)
        {
            case Figure.KNIGHT:
            case Figure.QUEEN:
            case Figure.KING:
                return 10;
            default:
                return (int)_figure + 1;
        }
    }

    private void InitializeBasicFields(Suit passedSuit, Figure passedFigure)
    {
        suit = passedSuit;
        figure = passedFigure;
        points = GetFigurePoints(passedFigure);
    }

    private void InitializeGameObjectFields()
    {
        gameObject = new GameObject();
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
    }

    private void IntiializeSpritesFields(Suit passedSuit, Figure passedFigure)
    {
        CardsSpritesFactory spriteFactory = new CardsSpritesFactory(passedSuit);
        frontImg = spriteFactory.GetFrontCardSprite(passedFigure);
        backImg = spriteFactory.GetBackCardSprite();
    }

    private void InitializeFields(Suit suit, Figure figure)
    {
        InitializeBasicFields(suit, figure);
        InitializeGameObjectFields();
        IntiializeSpritesFields(suit, figure);
    }

    public List<int> GetAsList()
    {
        int _figure = (int)figure;
        int _suit = (int)suit;
        List<int> list = new List<int>();
        list.Add(_figure);
        list.Add(_suit);
        return list;
    }
}
