using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{ 
    public Profile profile;
     
    public Image ProfileImage; 
    public TextMeshProUGUI LifePoint; 
    public TextMeshProUGUI Name; 
    public TextMeshProUGUI Elemental;
    public TextMeshProUGUI Score;
    public Transform PanelCard;
    public Transform PanelDrop;
    public Object PrefabsCard;
    public Object PrefabsBackCard;

    private Color red = Color.red;
    private Color brown = Color.green;
    private Color blue = Color.blue;
    private Color dark = Color.black;
    private Color yellow = Color.yellow;

    [SerializeField]
    private Sprite[] ElementImages;

    UnityEvent PlayTurn;
    UnityEvent Drop;

    void Start()
    {
        if(profile == null) { profile = this.GetComponent<Profile>(); } 
        var profileimage = profile.PlayerProfileImage;
        ProfileImage.sprite = profileimage;
        LifePoint.text = profile.getLifePoint.ToString();
        Name.text = profile.PlayerName;
        
        BackCard();
    }
    public void OnEnable()
    {
        PlayTurn = GameObject.Find("GameManager").GetComponent<GameManager>().PlayTurnEvent;
        PlayTurn.AddListener(OpenCard);
        Drop = GameObject.Find("GameManager").GetComponent<GameManager>().DropEvent;
        Drop.AddListener(OpenDropCard);
    }
    public void OnDisable()
    {
        PlayTurn.RemoveListener(OpenCard);
        Drop.RemoveListener(OpenDropCard); 
    }

    public void BackCard()
    { 
        for (int i = 0; i < profile.getDeckCount; i++)
        {
            Instantiate(PrefabsBackCard, PanelCard);
        }
    }
    public void OpenCard()
    {

        Debug.Log(this.gameObject.name);
        Elemental.text = profile.getElement;
        int count = PanelCard.transform.childCount;
        if( count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                Transform child = PanelCard.transform.GetChild(i);
                Debug.Log(child.name);
                Destroy(child.GameObject()); 
            }

        } 
        for (int i = 0; i < profile.getDeckCount; i++)
        {
            var color = CompareColor(profile.getDeck[i].color);
            var element = CompareElement(profile.getDeck[i].element);
            var number = profile.getDeck[i].number;
            var rank = profile.getDeck[i].rank;

            var prefab = PrefabsCard;
            prefab.GetComponent<Image>().color = color; 
            prefab.GetComponent<Transform>().Find("Element").GetComponent<Image>().sprite = element;
            
            if (number == 0)
            {
                prefab.GetComponentInChildren<TextMeshProUGUI>().text = rank;
            }
            else
                prefab.GetComponentInChildren<TextMeshProUGUI>().text = number.ToString();

            Instantiate(prefab, PanelCard);
        }
    }
    public void OpenDropCard()
    {
        LifePoint.text = profile.getLifePoint.ToString();
        Score.text = profile.getScore.ToString();

        Debug.Log(this.gameObject.name);
        int count = PanelDrop.transform.childCount;
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                Transform child = PanelDrop.transform.GetChild(i);
                Debug.Log(child.name);
                Destroy(child.GameObject());
            }

        }
        for (int i = 0; i < profile.getDropDeck.Count; i++)
        {
            var color = CompareColor(profile.getDropDeck[i].color);
            var element = CompareElement(profile.getDropDeck[i].element);
            var number = profile.getDropDeck[i].number;
            var rank = profile.getDropDeck[i].rank;

            var prefab = PrefabsCard;
            prefab.GetComponent<Image>().color = color;
            prefab.GetComponent<Transform>().Find("Element").GetComponent<Image>().sprite = element;

            if (number == 0)
            {
                prefab.GetComponentInChildren<TextMeshProUGUI>().text = rank;
            }
            else
                prefab.GetComponentInChildren<TextMeshProUGUI>().text = number.ToString();

            Instantiate(prefab, PanelDrop);
        }
    }
    private Color CompareColor(string color)
    {
        if (color == "Red")
        {
            return red;
        }else if (color == "Blue")
        {
            return blue;
        }else if (color == "Brown")
        {
            return brown;
        }else if (color == "Yellow")
        {
            return yellow;
        }else if (color == "Black")
        {
            return dark;
        } 
        return Color.white;
    }
    private Sprite CompareElement(string element)
    { 
        if (element == "Fire")
        {
            return ElementImages[0];
        }
        else if (element == "Water")
        {
            return ElementImages[1];
        }
        else if (element == "Earth")
        {
            return ElementImages[2];
        }
        else if (element == "Thunder")
        {
            return ElementImages[3];
        }
        else if (element == "Dark")
        {
            return ElementImages[4];
        }
        return null;
    }


}
