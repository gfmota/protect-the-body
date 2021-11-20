using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Body : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject gameOverScreen;
    public GameObject healthText;
    public Color HP3;
    public Color HP2;
    public Color HP1;
    private int healthPoints = 3;

    void UpdateText()
    {
        healthText.GetComponent<Text>().text = healthPoints.ToString();
        switch (healthPoints)
        {
            case 3:
                healthText.GetComponent<Text>().color = HP3;
                break;
            case 2:
                healthText.GetComponent<Text>().color = HP2;
                break;
            case 1:
                healthText.GetComponent<Text>().color = HP1;
                break;
            case 0:
                GameOver();
                break;
            default:
                break;
        }
    }

    void ChangeHealth(int n)
    {
        healthPoints += n;
        UpdateText();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            ChangeHealth(-1);
            if(healthPoints > 0) mainCamera.GetComponent<CameraShake>().Shake(.15f, .4f);
            Destroy(collision.gameObject);
        }
    }

    void GameOver()
    {
        gameOverScreen.SetActive(true);
        gameOverScreen.GetComponentInChildren<Text>().text = "Your score" + '\n' + GetComponent<ScoreManager>().Score().ToString("D4");
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }
        gameOverScreen.SetActive(false);
        healthPoints = 3;
        UpdateText();
        GetComponent<ScoreManager>().ResetScore();
        Time.timeScale = 1f;
    }
}
