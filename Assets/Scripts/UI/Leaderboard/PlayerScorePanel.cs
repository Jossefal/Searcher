﻿using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class PlayerScorePanel : MonoBehaviour
{
    public string score
    {
        get
        {
            return scoreText.text;
        }
        set
        {
            scoreText.text = value;
        }
    }

    public string playerName
    {
        get
        {
            return playerNameText.text;
        }
        set
        {
            playerNameText.text = value;
        }
    }

    [SerializeField] private Text scoreText;
    [SerializeField] private Text playerNameText;
}