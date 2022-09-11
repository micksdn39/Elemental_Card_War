using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : Profile
{
    public float selectCardvalue()
    {  
        DeckSettings deck = new DeckSettings();  
        var select = deck.CompareHighScore(this); 
        return select.Item2;
    }
    public override List<CardData> selectCard()
    {
        DeckSettings deck = new DeckSettings();
        var select = deck.CompareHighScore(this);
        return select.Item1;
    }


}
