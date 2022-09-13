using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public Player Player;
    public GameObject[] PlayerGroup;
    public DeckData Deck; 

    public UnityEvent PlayTurnEvent;
    void Start()
    {
        if (PlayerGroup == null) { PlayerGroup = GameObject.FindGameObjectsWithTag("Player"); }
        if (Player == null) { Player = GameObject.Find("Player").GetComponent<Player>(); }
        SetElementPlayerGroup();  
    }
    public void OnEnable()
    {
        if (PlayTurnEvent == null)
        {
            PlayTurnEvent = new UnityEvent();
        }
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
        PlayTurnEvent.Invoke();
    }
    public void DrawCard(Player player)
    {
        if (player.CanDraw())
        {
            Player.DrawCardMore(Deck.DealCardOnTop());
            Deck.RemoveCardOnTop();
        }
        PlayTurnEvent.Invoke();
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
