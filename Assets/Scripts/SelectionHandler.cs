using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionHandler : MonoBehaviour
{
    [SerializeField] TMP_Text storyText;
    [SerializeField] TMP_Text option1Text;
    [SerializeField] TMP_Text option2Text;
    [SerializeField] TMP_Text option3Text;
    [SerializeField] TMP_Text option4Text;

    List<Story> stories;
    Dictionary<string, int> attributes;
    int storyIdx = 0;

    
    void Start()
    {
        ClearPrefs();

        GenerateAttributes();

        GenerateStories();

        LoadStory(0);
    }


    void ClearPrefs()
    {
        PlayerPrefs.SetString("char_name", string.Empty);
        PlayerPrefs.SetInt("str", 0);
        PlayerPrefs.SetInt("agi", 0);
        PlayerPrefs.SetInt("int", 0);
        PlayerPrefs.SetInt("cha", 0);
    }



    void GenerateAttributes()
    {
        // strength,        str
        // agility,         agi
        // intellegence,    int
        // charisma,        cha
        attributes = new Dictionary<string, int>();
        attributes.Add("str", 0);
        attributes.Add("agi", 0);
        attributes.Add("int", 0);
        attributes.Add("cha", 0);
    }
    

    void GenerateStories()
    {
        stories = new List<Story>();

        // Story Line - 1
        Story story1 = new Story();
        story1.Text = "Yıllar önce, uzak bir diyarda doğmuştun. Baban bir...";

        story1.Selections.Add(new Selection("asildi", new Effect("cha", 1)));
        story1.Selections.Add(new Selection("savaşçıydı", new Effect("str", 1)));
        story1.Selections.Add(new Selection("avcıydı", new Effect("agi", 1)));
        story1.Selections.Add(new Selection("tüccardı", new Effect("int", 1)));

        stories.Add(story1);

        // Story Line - 2
        Story story2 = new Story();
        story2.Text = "Tüm zorluklara rağmen ailen sana iyi bir eğitim vermeye çalıştı. Neredeyse koşup yürüyebildiğin gibi dünyayı öğrenmeye çok hevesliydin. Hayatının başlarında bir...";

        story2.Selections.Add(new Selection("hizmetkardın", new Effect("cha", 2)));
        story2.Selections.Add(new Selection("çıraktın", new Effect("str", 2)));
        story2.Selections.Add(new Selection("göçebeydin", new Effect("agi", 2)));
        story2.Selections.Add(new Selection("kalfaydın", new Effect("int", 2)));

        stories.Add(story2);

        // Story Line - 3
        Story story3 = new Story();
        story3.Text = "Çocukluktan çıkıp büyüdükçe dünyayı daha iyi görmeye ve anlamaya başladın. Bazen aç bazen tok at üstünde bir hayat sürdün. Yeni maceralar seni bir diyardan ötekine götürdü. Gençlik dönemine geldiğinde sen bir...";

        story3.Selections.Add(new Selection("ozandın", new Effect("cha", 3)));
        story3.Selections.Add(new Selection("şövalyeydin", new Effect("str", 3)));
        story3.Selections.Add(new Selection("avcıydın", new Effect("agi", 3)));
        story3.Selections.Add(new Selection("öğrenciydin", new Effect("int", 3)));

        stories.Add(story3);
    }


    Story GetCurrentStory()
    {
        return stories.ElementAt(storyIdx);
    }


    void LoadStory(int idx)
    {
        storyIdx = idx;
        storyText.text = stories[idx].Text;
        option1Text.text = stories[idx].Selections.ElementAt(0).Text;
        option2Text.text = stories[idx].Selections.ElementAt(1).Text;
        option3Text.text = stories[idx].Selections.ElementAt(2).Text;
        option4Text.text = stories[idx].Selections.ElementAt(3).Text;
    }


    void LoadNextStory()
    {
        if (storyIdx < stories.Count - 1) 
        {
            storyIdx++;
            LoadStory(storyIdx);
        }
        else
        {
            // Save Prefs
            foreach (KeyValuePair<string, int> kvp in attributes)
            {
                PlayerPrefs.SetInt(kvp.Key, kvp.Value);
            }

            // Load Character build scene
            SceneManager.LoadScene(3);
        }
    }


    public void OnOptionSelected(int selectIdx)
    {
        // Apply the relevant effect
        Effect effect = GetCurrentStory().Selections.ElementAt(selectIdx).Effect;
        attributes[effect.Type] += effect.Amount;

        PrintAttributes();

        // Load the next story
        LoadNextStory();
    }


    void PrintAttributes()
    {
        foreach (KeyValuePair<string, int> kvp in attributes) 
        {
            Debug.Log(kvp.Key + ": " + kvp.Value);
        }
    }


    class Effect
    {
        public string Type {  get; set; }
        public int Amount { get; set; }

        public Effect(string type, int amount)
        {
            this.Type = type;
            this.Amount = amount;
        }

        public override string ToString()
        {
            return Type + " +" + Amount;
        }
    }


    class Selection
    {
        public string Text { get; set; }
        public Effect Effect { get; set; }

        public Selection(string text, Effect effect)
        {
            Text = text;
            Effect = effect;
        }
    }


    class Story
    {
        public string Text { get; set; }
        public List<Selection> Selections { get; set; }

        public Story()
        {
            Text = string.Empty;
            Selections = new List<Selection>();
        }
    }
}
