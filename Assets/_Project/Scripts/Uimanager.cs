using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
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
    UnityEvent Status;

    void Start()
    {
        if (profile == null) { profile = this.GetComponent<Profile>(); }
        var profileimage = profile.PlayerProfileImage;
        ProfileImage.sprite = profileimage;
        LifePoint.text = profile.getLifePoint.ToString();
        Name.text = profile.PlayerName;
         
    }
    public void OnEnable()
    {
        PlayTurn = GameObject.Find("GameManager").GetComponent<GameManager>().PlayTurnEvent;
        PlayTurn.AddListener(OpenCard);
        Drop = GameObject.Find("GameManager").GetComponent<GameManager>().DropEvent;
        Drop.AddListener(OpenDropCard);
        Status = GameObject.Find("GameManager").GetComponent<GameManager>().StatusEvent;
        Status.AddListener(UpdateStatus);
    }
    public void OnDisable()
    {
        PlayTurn.RemoveListener(OpenCard);
        Drop.RemoveListener(OpenDropCard);
        Status.RemoveListener(UpdateStatus);
    }
 
    private void UpdateStatus()
    {
        LifePoint.text = profile.getLifePoint.ToString();
        Score.text = profile.getScore.ToString(); 
    }
    public void OpenCard()
    { 
        Elemental.text = profile.getElement;

        DestroyChildOBject(PanelCard);
        InstandCardInHand(profile,PanelCard);
    }
    public void OpenDropCard()
    {
        DestroyChildOBject(PanelDrop);
        InstandCardDrop(profile, PanelDrop);
    }
    private void InstandCardInHand(Profile player, Transform panel)
    {
        for (int i = 0; i < player.getDeckCount; i++)
        {
            var color = CompareColor(player.getDeck[i].color);
            var element = CompareElement(player.getDeck[i].element);
            var number = player.getDeck[i].number;
            var rank = player.getDeck[i].rank;

            var prefab = PrefabsCard;
            prefab.GetComponent<Image>().color = color;
            prefab.GetComponent<Transform>().Find("Element").GetComponent<Image>().sprite = element;

            if (number == 0)
            {
                prefab.GetComponentInChildren<TextMeshProUGUI>().text = rank;
            }
            else
                prefab.GetComponentInChildren<TextMeshProUGUI>().text = number.ToString();

            Instantiate(prefab, panel);
        }

    }
    private void InstandCardDrop(Profile player,Transform panel)
    {
        for (int i = 0; i < player.getDropDeck.Count; i++)
        {
            var color = CompareColor(player.getDropDeck[i].color);
            var element = CompareElement(player.getDropDeck[i].element);
            var number = player.getDropDeck[i].number;
            var rank = player.getDropDeck[i].rank;

            var prefab = PrefabsCard;
            prefab.GetComponent<Image>().color = color;
            prefab.GetComponent<Transform>().Find("Element").GetComponent<Image>().sprite = element;

            if (number == 0)
            {
                prefab.GetComponentInChildren<TextMeshProUGUI>().text = rank;
            }
            else
                prefab.GetComponentInChildren<TextMeshProUGUI>().text = number.ToString();

            Instantiate(prefab, panel);
        }

    }
    private void DestroyChildOBject(Transform objectTrans)
    {
        int count = objectTrans.transform.childCount;
        if (count > 0)
        {
            for (int i = 0; i < count; i++)
            {
                Transform child = objectTrans.transform.GetChild(i);
                Destroy(child.GameObject());
            }

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
