using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharController : MonoBehaviour
{

    [SerializeField] TMP_Text strengthText;
    [SerializeField] TMP_Text agilityText;
    [SerializeField] TMP_Text intelligenceText;
    [SerializeField] TMP_Text charismaText;
    [SerializeField] TMP_Text skillPointsText;
    [SerializeField] TMP_InputField playerNameInput;

    [SerializeField] Sprite[] charImages;
    [SerializeField] Image charImg;
    int charImgIndex = 0;
    int skillPoints = 20;
    int strPoints, agiPoints, intPoints, chaPoints;
    string playerName = string.Empty;


    void Start()
    {
        charImg.sprite = charImages[0];

        LoadPrefs();

        strengthText.text = strPoints.ToString();
        agilityText.text = agiPoints.ToString();
        intelligenceText.text = intPoints.ToString();
        charismaText.text = chaPoints.ToString();
        skillPointsText.text = skillPoints.ToString();
        playerNameInput.text = playerName;
    }


    // Just faking
    public void ChangeCharImage()
    {
        if (charImgIndex == 0)
            charImgIndex = 1;
        else
            charImgIndex = 0;

        charImg.sprite = charImages[charImgIndex];
    }


    void LoadPrefs()
    {
        playerName = PlayerPrefs.GetString("char_name", string.Empty);
        strPoints = PlayerPrefs.GetInt("str", 0);
        agiPoints = PlayerPrefs.GetInt("agi", 0);
        intPoints = PlayerPrefs.GetInt("int", 0);
        chaPoints = PlayerPrefs.GetInt("cha", 0);
    }


    public void Increment(string attribute)
    {
        if (skillPoints > 0) 
        {
            switch (attribute) 
            {
                case "str":
                    strPoints++;
                    strengthText.text = strPoints.ToString();
                    break;
                case "agi":
                    agiPoints++;
                    agilityText.text = agiPoints.ToString();
                    break;
                case "int":
                    intPoints++;
                    intelligenceText.text = intPoints.ToString();
                    break;
                case "cha":
                    chaPoints++;
                    charismaText.text = chaPoints.ToString();
                    break;
            }
            skillPoints--;
            skillPointsText.text = skillPoints.ToString();
        }
    }


    public void CreateButton()
    {
        if (playerName.Trim().Length > 0) 
        {
            PlayerPrefs.SetString("char_name", playerName.Trim());
        }

        PlayerPrefs.SetInt("str", strPoints);
        PlayerPrefs.SetInt("agi", agiPoints);
        PlayerPrefs.SetInt("int", intPoints);
        PlayerPrefs.SetInt("cha", chaPoints);

        SceneManager.LoadScene(0);
    }

    public void InputTextValueChanged(string value)
    {
        playerName = value;
    }
}
