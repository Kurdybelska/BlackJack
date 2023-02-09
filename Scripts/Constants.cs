public enum FigureSpriteIdConstants
{
    SPRITE_ID_OFFSET_FOR_NORMAL_CARDS = 1,
    ACE_SPRITE_ID = 9,
    KNIGT_SPRITE_ID = 10,
    KING_SPRITE_ID = 11,
    QUEEN_SPRITE_ID = 12
}

public enum Suit
{
    HEART = 0,
    SPADE = 1,    // dzwonek
    CLUB = 2,     // trefl
    DIAMOND = 3
}

public enum Figure : sbyte
{
    ACE = 0,
    TWO = 1,
    THREE = 2,
    FOUR = 3,
    FIVE = 4,
    SIX = 5,
    SEVEN = 6,
    EIGHT = 7,
    NINE = 8,
    TEN = 9,
    KNIGHT = 10,
    QUEEN = 11,
    KING = 12
}

public enum Move
{
    DEAL,
    STAND
}


public enum MoveResult
{
    BLACKJACK,   // player has exactly 21 points and won
    BUST,        // player has ore than 21 points and lost
    CONTINUE     // player has less than 21 points and can continue playing
}