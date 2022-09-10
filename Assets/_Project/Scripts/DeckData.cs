using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

[System.Serializable]
public class CardData
{
    public int number;
    public string rank;
    public string element;
    public string color; 
    public int bonus;
    
}
  
public class DeckData : MonoBehaviour
{
    [Header("Deck Settings")]
    [Space]
    [SerializeField]
    private int NumberLength = 10;
    [SerializeField]
    private String[] Rank = { "F", "W", "E", "T", "B" };
    [SerializeField]
    private String[] Element = { "Fire", "Water", "Earth", "Thunder", "Dark" };
    [SerializeField]
    private String[] Color = { "Red", "Blue", "Brown", "Yellow", "Black" };

    [Header("Deck Card")]
    [Space]
    [SerializeField]
    private List<CardData> AllCard = new List<CardData>();

    // Start is called before the first frame update
    void Start()
    {
        CardLoad(AllCard);
    }

    // Update is called once per frame
    void Update()
    {

    } 
    public void CardLoad(List<CardData> cardlist)
    { 
        for (int j = 0; j < Element.Length; j++)
        {
            for (int k = 0; k < Color.Length; k++)
            {
                for (int i = 1; i <= NumberLength + Rank.Length; i++)
                {
                    CardData card = new CardData();
                    if (i <= NumberLength)
                    {
                        card.number = i;
                        card.rank = "N";
                    }
                    else
                    {
                        card.number = 0;
                        card.rank = Rank[i - (NumberLength + 1)];
                    }

                    card.element = Element[j];
                    card.color = Color[k];

                    cardlist.Add(card);

                }
            }
        }

        
         

    }





}
