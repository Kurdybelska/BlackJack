using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesConstants
{
    public static string CardBacksSheetPath = "Sprites/card_backs";
    public static string ClubsSuitSheetPath = "Sprites/clubs";
    public static string DiamondsSuitSheetPath = "Sprites/diamonds";
    public static string HeartsSuitSheetPath = "Sprites/hearts";
    public static string SpadesSuitSheetPath = "Sprites/spades";
    public static string ArrowSpritePath = "Sprites/arrow";
}

public class CardsSpritesFactory
{

    private Sprite[] frontSpritesSheet;
    private Sprite[] backSpritesSheet;

    public CardsSpritesFactory(Suit suit)
    {
        string suitPath = GetSuitePath(suit);
        frontSpritesSheet = Resources.LoadAll<Sprite>(suitPath);
        backSpritesSheet = Resources.LoadAll<Sprite>(SpritesConstants.CardBacksSheetPath);
    }

    public Sprite GetBackCardSprite()
    {
        return backSpritesSheet[0];
    }

    public Sprite GetFrontCardSprite(Figure figure)
    {
        var spriteId = GetSpriteId(figure);
        return frontSpritesSheet[spriteId];
    }

    private int GetSpriteId(Figure figure)
    {
        switch (figure)
        {
            case Figure.ACE:
                return (int)FigureSpriteIdConstants.ACE_SPRITE_ID;
            case Figure.KNIGHT:
                return (int)FigureSpriteIdConstants.KNIGT_SPRITE_ID;
            case Figure.KING:
                return (int)FigureSpriteIdConstants.KING_SPRITE_ID;
            case Figure.QUEEN:
                return (int)FigureSpriteIdConstants.QUEEN_SPRITE_ID;
            default:
                return (int)figure - (int)FigureSpriteIdConstants.SPRITE_ID_OFFSET_FOR_NORMAL_CARDS;
        }
    }

    private string GetSuitePath(Suit suit)
    {
        switch (suit)
        {
            case Suit.CLUB: return SpritesConstants.ClubsSuitSheetPath;
            case Suit.DIAMOND: return SpritesConstants.DiamondsSuitSheetPath;
            case Suit.HEART: return SpritesConstants.HeartsSuitSheetPath;
            case Suit.SPADE: return SpritesConstants.SpadesSuitSheetPath;
            default:
                Debug.Log("Wrong suit passed");
                return "";
        }
    }
}

public class ArrowSpriteFactory
{
    private Sprite arrowSprite;

    public ArrowSpriteFactory()
    {
        arrowSprite = Resources.Load<Sprite>(SpritesConstants.ArrowSpritePath);
    }

    public Sprite GetArrowSprite()
    {
        return arrowSprite;
    }
}