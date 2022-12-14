using System.Collections.Generic; 
using UnityEngine; 
using Random = UnityEngine.Random;
using System.IO;
using Newtonsoft.Json;

[System.Serializable]
public class CardData
{
    public int number;
    public string rank;
    public string element;
    public string color;  
} 
  
public class DeckData : MonoBehaviour
{ 
    [Header("Deck Settings")]
    [Space]
    public DeckSettings DeckProperty;

    [Header("Deck Card")]
    [Space]
    [SerializeField]
    private List<CardData> MyDeck = new List<CardData>();
    public int getCurrentCardCount { get { return MyDeck.Count; } }
     
    void Start()
    {
        if (File.Exists(GetFilePath(file)) == false)
        {
            CardLoad(MyDeck);
            ShuffleCards(MyDeck); 
        }

    }
     
    private void CardLoad(List<CardData> cardlist)
    { 
        for (int j = 0; j < DeckProperty.Element.Length; j++)
        {
            for (int k = 0; k < DeckProperty.Color.Length; k++)
            {
                for (int i = 1; i <= DeckProperty.NumberLength + DeckProperty.Rank.Length; i++)
                {
                    CardData card = new CardData();
                    if (i <= DeckProperty.NumberLength)
                    {
                        card.number = i;
                        card.rank = "N";
                    }
                    else
                    {
                        card.number = 0;
                        card.rank = DeckProperty.Rank[i - (DeckProperty.NumberLength + 1)];
                    }

                    card.element = DeckProperty.Element[j];
                    card.color = DeckProperty.Color[k];

                    cardlist.Add(card);

                }
            }
        }
          
    }
    private void ShuffleCards(List<CardData> cardlist)
    {  
        for (int i = 0; i < cardlist.Count; i++)
        {
            var CardData = cardlist[i];
            int randomIndex = Random.Range(i, cardlist.Count);

            cardlist[i] = cardlist[randomIndex];
            cardlist[randomIndex] = CardData;

        }
    }

    public void NewDeck()
    {
        MyDeck.Clear();
        CardLoad(MyDeck);
        ShuffleCards(MyDeck); 
    }
    public CardData DealCardOnTop()
    {
        CardData CardData = new CardData();
        CardData = MyDeck[0]; 
        return CardData;
    }
    public void RemoveCardOnTop()
    { 
        MyDeck.RemoveAt(0); 
        Save();
    }

    public DeckSettings GetDeckSettings()
    {
        return DeckProperty;
    }
     

    private string file = "DeckData.txt";  
    public void Load()
    {  
        string json = ReadFromFIle(file);
        var Convert = JsonConvert.DeserializeObject<List<CardData>>(json);
        MyDeck = Convert; 
    }
  
    public void Save()
    {
        var json = JsonConvert.SerializeObject(MyDeck,Formatting.Indented);  
        WriteToFile(file, json);
    }
  
    private void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    private string ReadFromFIle(string fileName)
    {
        string path = GetFilePath(fileName);
        if (File.Exists(path))
        { 

            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
        {
            Debug.LogWarning("File not found");
        }

        return "Success";
    }

    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }

}
