using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{ 
    public Profile profile;

    [SerializeField]
    public Image ProfileImage;
    [SerializeField]
    public TextMeshProUGUI LifePoint;
    [SerializeField]
    public TextMeshProUGUI Name;
    [SerializeField]
    public TextMeshProUGUI Elemental;
     
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

    void Start()
    {
        profile = this.GetComponent<Profile>(); 
        var profileimage = profile.PlayerProfileImage;
        ProfileImage.sprite = profileimage;
        LifePoint.text = profile.getLifePoint.ToString();
        Name.text = profile.PlayerName;
        Elemental.text = profile.getElement;
        BackCard();
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
        int count = PanelCard.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform child = PanelCard.transform.GetChild(i);
            Destroy(child);
        }

        

        for (int i = 0; i < profile.getDeckCount; i++)
        {
            var color = CompareColor(profile.getDeck[i].color);
            var element = CompareElement(profile.getDeck[i].element);
            var number = profile.getDeck[i].number;
            var rank = profile.getDeck[i].rank;

            var prefab = PrefabsCard;
            prefab.GetComponent<Image>().color = color;
           // prefab.GetComponentInChildren<Image>().sprite = element;
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
