using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Profile
{   
    public bool CanDraw()
    {
        if (LifePoint > 1 && DeckinHand.Count < LimitCardInHand)
        {
            return true;
        }
        return false;
    }
    public void DrawCardMore(CardData card)
    { 
            DeckinHand.Add(card); 
            LifePoint--; 
    }

}
