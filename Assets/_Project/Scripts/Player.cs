using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Profile
{   
    public void DrawCardMore(CardData card)
    {
        if(LifePoint > 1 && DeckinHand.Count < LimitCardInHand)
        {
            DeckinHand.Add(card);
            LifePoint--;
        } 
    }

}
