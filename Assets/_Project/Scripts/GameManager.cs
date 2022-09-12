using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class GameManager : MonoBehaviour
{
    public Player Player;
    public GameObject[] PlayerGroup;
    public DeckData Deck; 
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<Player>();
        PlayerGroup = GameObject.FindGameObjectsWithTag("Player");
        SetElementPlayerGroup();
    } 

    public void PlayTurn()
    {
        var cardfirstdeal = 0;
        foreach (var player in PlayerGroup)
        {
            var highCardcandeal = player.GetComponent<Profile>().LimitCardFirstDeal;
            if (cardfirstdeal < highCardcandeal)
                cardfirstdeal = highCardcandeal;
        }

        for (int i = 0; i < cardfirstdeal; i++)
        {
            foreach (var player in PlayerGroup)
            {
                DealCard(player.GetComponent<Profile>());
            }
        }
    }
    public void DrawCard(Player player)
    {
        if (player.CanDraw())
        {
            Player.DrawCardMore(Deck.DealCardOnTop());
            Deck.RemoveCardOnTop();
        }
    }
      
    private void DealCard(Profile player)
    {
        if (player.LimitCardInHand > player.getDeckCount)
        { 
            player.addCard(Deck.DealCardOnTop());
            Deck.RemoveCardOnTop();
        }
         
    }

    private void SetElementPlayerGroup()
    { 
        foreach (var player in PlayerGroup)
        {
            var DeckSettings = Deck.GetDeckSettings();
            var elementRandom = Random.Range(0, DeckSettings.GetLengthElement);
            Debug.Log(elementRandom);
            player.GetComponent<Profile>().getElement = DeckSettings.Element[elementRandom];
        }

    }
     

}
