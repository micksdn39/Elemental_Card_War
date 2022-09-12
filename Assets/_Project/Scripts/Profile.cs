using System.Collections;
using System.Collections.Generic;
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
    protected float LifePoint;
    public float getLifePoint { get { return LifePoint; } set { LifePoint = value; } }
    [SerializeField]
    protected string Element;
    public string getElement { get { return Element; } set { Element = value; } }
    [SerializeField]
    protected List<CardData> DeckinHand = new List<CardData>();
    public int getDeckCount { get { return DeckinHand.Count; } }
    public List<CardData> getDeck { get { return DeckinHand; } }
      
    public void addCard(CardData card)
    {  
            DeckinHand.Add(card); 
    }
    
    public virtual List<CardData> selectCard()
    { 
        return null;
    }
}
