using System; 
using System.Collections.Generic;
using System.Linq; 
using UnityEngine;
using UnityEngine.Events; 
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Player Player;
    public GameObject[] PlayerGroup;
    public DeckData Deck;

    public UnityEvent PlayTurnEvent;
    public UnityEvent DropEvent;
    public UnityEvent StatusEvent;
    void Start()
    {
        FindPlayerGroup();
        if (Player == null) { Player = GameObject.Find("Player").GetComponent<Player>(); }
        SetElementPlayerGroup();  
    }
    public void OnEnable()
    {
        if (PlayTurnEvent == null)
        {
            PlayTurnEvent = new UnityEvent();
        }
        if(DropEvent == null)
        {
            DropEvent = new UnityEvent();
        }
        if(StatusEvent == null)
        {
            StatusEvent = new UnityEvent();
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
        StatusEvent.Invoke();
    }
    public void PlayerScoreCal()
    {  
        List<Profile> playergroup = new List<Profile>(); 
        foreach (var player in PlayerGroup)
        {
            playergroup.Add(player.GetComponent<Profile>()) ;
            
        }
        var win = CheckWinCardOnPlayer(); 
        if (win.Count == 1)
        {
            foreach (var py in playergroup)
            {
                if (CheckWinPlayer(py) != true)
                {
                    py.onLost();
                    if (py.getLifePoint <= 0)
                    {
                        FindPlayerGroup();
                    }
                }
                else
                {
                    py.getLifePoint += playergroup.Count - 1;
                    Debug.Log(py.name + " WinCard in This Turn");
                }
            }

            StatusEvent.Invoke();
            return;
        }
       

        var pyg = playergroup.OrderByDescending(x => x.getScore).ToList(); 
        List<Profile> playerWin = new List<Profile>(); 

        float bestScore = pyg[0].getScore;
        float lifepoint = 0;
        foreach (var py in pyg)
        { 
            if(py.getScore == bestScore)
            {
                playerWin.Add(py); 
            }
            else
            {
                py.onLost();
                if(py.getLifePoint<=0)
                {
                    FindPlayerGroup();
                }
                lifepoint++;
            }
        }
     
        lifepoint = lifepoint/ playerWin.Count ;
        foreach (var py in playerWin)
        {
            py.getLifePoint += lifepoint;
            Debug.Log(py.name + " Win !!");
        }
        StatusEvent.Invoke();
    }

    private bool CheckWinPlayer(Profile player)
    {
        if (player.GetComponent<Profile>().winCard)
        {
            DeckSettings deck = new DeckSettings();
            List<bool> check = new List<bool>();
            foreach (var card in player.GetComponent<Profile>().getDropDeck)
            {
                check.Add(deck.WinCard(card));
            }
            for (int i = 0; i < check.Count-1; i++)
            {
                if (check[i] != check[i + 1])
                {
                    return true;
                }
            }

        }


        return false;
    }
    private List<Profile> CheckWinCardOnPlayer()
    {
        List<Profile> playergroup = new List<Profile>();

        foreach (var player in PlayerGroup)
        {
           if(player.GetComponent<Profile>().winCard)
            {
                DeckSettings deck = new DeckSettings();
                List<bool> check = new List<bool>();
               foreach(var card in player.GetComponent<Profile>().getDropDeck)
                {
                    check.Add(deck.WinCard(card));
                }
               for(int i = 0; i < check.Count-1; i++)
                {
                    if (check[i] != check[i+1])
                    {
                        playergroup.Add(player.GetComponent<Profile>());
                      
                    }
                }

            }

        }

        return playergroup;
    }
     
    private void FindPlayerGroup()
    {
       Array.Clear(PlayerGroup, 0, PlayerGroup.Length); 
       PlayerGroup = GameObject.FindGameObjectsWithTag("Player"); 
    }
    public void EndTurn()
    {
        foreach (var player in PlayerGroup)
        {
            player.GetComponent<Profile>().ResetProfile();
        }
        StatusEvent.Invoke();
        PlayTurnEvent.Invoke();
        DropEvent.Invoke();
    }
    public void DropCard()
    {
        foreach (var player in PlayerGroup)
        {
            player.GetComponent<Profile>().selectCard(); 
        }
        DropEvent.Invoke();
        StatusEvent.Invoke();
    }
    public void DrawCard(Player player)
    {
        if (player.CanDraw())
        {
            Player.DrawCardMore(Deck.DealCardOnTop());
            Deck.RemoveCardOnTop();
        }

        PlayTurnEvent.Invoke();
        DropEvent.Invoke();
        StatusEvent.Invoke();
    }
      
    private void DealCard(Profile player)
    {
        if(Deck.getCurrentCardCount <=20)
        {
            Deck.NewDeck();
        }
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
           
            player.GetComponent<Profile>().getElement = DeckSettings.Element[elementRandom];
        }

    }
     

}
