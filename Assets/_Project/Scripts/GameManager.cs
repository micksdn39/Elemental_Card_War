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
        DealCard(PlayerGroup[0].GetComponent<Profile>());
 
    }
    public void DrawCard()
    {
        Player.DrawCardMore(Deck.DealCardOnTop()); 
    }
      
    private void DealCard(Profile player)
    {  
        player.addCard(Deck.DealCardOnTop());
      
    }

    private void SetElementPlayerGroup()
    { 
        foreach (var player in PlayerGroup)
        {
            var DeckSettings = Deck.GetDeckSettings();
            var elementRandom = Random.Range(0, DeckSettings.GetLengthElement);
            Debug.Log(elementRandom);
            player.GetComponent<Profile>()
                .setElement(DeckSettings.Element[elementRandom]);
        }

    }
     

}
