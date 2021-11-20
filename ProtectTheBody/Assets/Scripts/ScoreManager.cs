using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject scoreText;
    private int score = 0;
    void UpdateText()
    {
        scoreText.GetComponent<Text>().text = score.ToString("D4");
    }

    public void ChangeScore(int n)
    {
        score += n;
        UpdateText();
    }

    public void ResetScore()
    {
        score = 0;
        UpdateText();
    }

    public int Score()
    {
        return score;
    }
}
