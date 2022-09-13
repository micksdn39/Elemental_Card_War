using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Profile : MonoBehaviour
{
    [Header("Player Settings")]
    [Space]
    public string PlayerName;
    public Sprite PlayerProfileImage;  
    public int LimitCardFirstDeal;
    public int LimitCardInHand;

    [Header("Player Stats")]
    [Space]
    [SerializeField]
    protected float Score;
    public float getScore { get { return Score; } set { Score = value; } }
    [SerializeField]
    protected float LifePoint;
    public float getLifePoint { get { return LifePoint; } set { LifePoint = value; } }
    [SerializeField]
    protected string Element;
    public string getElement { get { return Element; } set { Element = value; } }
    [SerializeField]
    protected List<CardData> DeckinHand = new List<CardData>();
    public int getDeckCount { get { return DeckinHand.Count; } }
    public List<CardData> getDeck { get { return DeckinHand; } }

    [SerializeField]
    protected List<CardData> SelectDropDeck = new List<CardData>();
    public List<CardData> getDropDeck { get { return SelectDropDeck; } }

    public bool winCard = false;
    public void addCard(CardData card)
    {  
            DeckinHand.Add(card); 
    } 
    public void onLost()
    {
        LifePoint -= 1;
        if(LifePoint<=0)
        {
            ResetProfile();
            this.tag = "Lost";
            Debug.Log("Player " + name + " Exit Game");
        }
    }

    public void selectCardScore()
    {
        DeckSettings deck = new DeckSettings();

      //  Score = deck.CompareScore(Element, SelectDropDeck[0], SelectDropDeck[1]); 
    }

    public void ResetProfile()
    {
        Score = 0;
        DeckinHand.Clear();
        SelectDropDeck.Clear();
    }
    public void selectCard()
    { 
        SelectDropDeck = MySelect(2);
        selectCardScore();
    }

    private List<CardData> MySelect(int Card)
    {
        var select = new List<CardData>();
        if (CheckWinCardList().Count >= Card && (CheckWinCardList().Count == (DeckinHand.Count -1)))
        { 
            for (int i = 0; i < DeckinHand.Count; i++)
            {
                select.Add(DeckinHand[i]);
                if (select.Count >= Card)
                {
                    break;
                }
            }
            winCard = true;
            return select;
        }
        
        if (CheckWinCardList().Count > 1)
        { 
            for (int j = 0; j < DeckinHand.Count; j++)
            {
                if (CheckWinCard(DeckinHand[j]) == true)
                {
                    select.Add(DeckinHand[j]); 
                }
            }
            for (int i = 0; i < Card - 1; i++)
            {
                for (int j = 0; j < DeckinHand.Count; j++)
                {
                    if (CheckWinCard(DeckinHand[i]) == false)
                    {
                        select.Add(DeckinHand[i]);
                        break;
                    }
                }
            }
            winCard = true;
            return select;
        }
        if (CheckWinCardList().Count == 1)
        { 
            for (int j = 0; j < DeckinHand.Count; j++)
            {
                if (CheckWinCard(DeckinHand[j]) == true)
                {
                    select.Add(DeckinHand[j]);
                    break;
                }
            }
            for (int i = 0; i < DeckinHand.Count; i++)
            {
                if (CheckWinCard(DeckinHand[i]) == false)
                {
                    select.Add(DeckinHand[i]);
                }
                if(select.Count >= Card)
                {
                    break;
                }
                    
            }
            winCard = true;
            return select; 
        }

        DeckSettings deck = new DeckSettings();
        select = deck.CompareHighScore(this).Item1;
        Score = deck.CompareHighScore(this).Item2; 
        return select;
    }


    private List<CardData> CheckWinCardList()
    {
        DeckSettings deck = new DeckSettings();

        var WinCard = new List<CardData>();

        foreach (CardData card in DeckinHand)
        {
            if (deck.WinCard(card) == true)
                WinCard.Add(card);
        }

        return WinCard;
    }
    private bool CheckWinCard(CardData card)
    {
        DeckSettings deck = new DeckSettings();
        if (deck.WinCard(card) == true)
        { return true; }

        return false;
    }
}
