using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  
public class DeckSettings : MonoBehaviour
{ 
    public int NumberLength = 10;
    public String[] Rank = { "F", "W", "E", "T", "B" };
    public String[] Element = { "Fire", "Water", "Earth", "Thunder", "Dark" };
    public String[] Color = { "Red", "Blue", "Brown", "Yellow", "Black" };
    public int GetLengthElement { get { return Element.Length; } }

    public List<CardData> CompareHighScore(Profile player)
    {
        List<CardData> list = new List<CardData>();
        float temp = 0;
        
        for (int i = 0; i < player.getDeckCount-1; i++)
        {
           var value = CompareScore(player.getElement,player.getDeck[i], player.getDeck[i + 1]);
            if (temp > value)
            {
                list.Clear();
                list.Add(player.getDeck[i]);
                list.Add(player.getDeck[i + 1]);
            } 
        } 

        return list;
    }
    private float CompareScore(string element,CardData cardA, CardData cardB)
    {
        float bonus = 0;
        if(WinCard(cardA)==true && WinCard(cardB)==true)
        {
            var score = cardA.number + cardB.number;
            bonus -= score;
            return bonus;
        }
        bonus += BonusOneCardCal(cardA);
        bonus += BonusElementWithPlayerAfterCalculate(cardA,element);
        bonus += BonusOneCardCal(cardB);
        bonus += BonusElementWithPlayerAfterCalculate(cardB,element);
        bonus += BonusTwoCardAfterCalculate(cardA, cardB);

        return bonus;
    }
    public bool WinCard(CardData card)
    {
        var cardRank = card.rank;
        var cardElement = card.element;
        var cardColor = card.color;

        foreach (var rank in Rank)
        {
            if (cardRank == rank)
            {
                foreach (var element in Element)
                {
                    if (cardElement == element)
                    {
                        foreach (var color in Color)
                        {
                            if (cardColor == color)
                            {
                                return true;

                            }
                        }

                    }

                }

            }
        }

        return false;
    }
    private float BonusOneCardCal(CardData card)
    {
        float Bonus = 0;
        var cardNumber = card.number;
        var cardRank = card.rank;
        var cardElement = card.element;
        var cardColor = card.color;

        foreach (var element in Element)
        {
            if (cardElement == element)
            {
                foreach (var color in Color)
                {
                    if (cardColor == color)
                    {
                        Bonus += cardNumber * 2;
                    }
                }

            }

        }

        foreach (var rank in Rank)
        {
            if (cardRank == rank)
            {
                foreach (var element in Element)
                {
                    if (cardElement == element)
                    {
                        var bonus = 11 - cardNumber;
                        Bonus += bonus;
                        return Bonus;
                    }

                }

            }
        }

        foreach (var rank in Rank)
        {
            if (cardRank == rank)
            {
                foreach (var color in Color)
                {
                    if (cardColor == color)
                    {
                        Bonus += cardNumber + 0.5f;
                    }
                }

            }
        }


        return Bonus;
    }

    private float BonusElementWithPlayerAfterCalculate(CardData card, string player)
    {
        if (player == card.element)
        {
            return 0.5f;
        }
        return 0;
    }


    private float BonusTwoCardAfterCalculate(CardData cardA, CardData cardB)
    {
        float bonus = 0;
        if (cardA.element == cardB.element)
        {
            bonus += 0.5f;
        }
        if (cardA.color == cardB.color)
        {
            bonus += 0.5f;
        }
        if (cardA.rank == cardB.rank && (cardA.rank != "N"))
        {
            bonus += 0.5f;
        }
        return 0;
    }
     
  
}
