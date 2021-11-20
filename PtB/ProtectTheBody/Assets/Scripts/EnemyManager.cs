using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;
    public float timer = 5f;
    private float timeLeft = 0;
    private Vector3 bottomLeftScreen;
    private Vector3 topRightScreen;

    private void Start()
    {
        // Gets Screen's size
        bottomLeftScreen = Camera.main.ViewportToWorldPoint(new Vector3(0,0,0));
        topRightScreen = Camera.main.ViewportToWorldPoint(new Vector3(1,1,0));
    }

    // Update is called once per frame
    void Update()
    {
        if (timeLeft <= 0)
        {
            SpawnEnemy();
            timeLeft = timer;
        }
        timeLeft -= Time.deltaTime;
    }

    void SpawnEnemy()
    {
        int side = Random.Range(0, 4);
        float x = 0f, y = 0f;
        switch (side)
        {
            case 0:
                x = bottomLeftScreen.x;
                y = Random.Range(bottomLeftScreen.y, topRightScreen.y);
                break;
            case 1:
                x = Random.Range(bottomLeftScreen.x, topRightScreen.x);
                y = topRightScreen.y;
                break;
            case 2:
                x = topRightScreen.x;
                y = Random.Range(bottomLeftScreen.y, topRightScreen.y);
                break;
            case 3:
                x = Random.Range(bottomLeftScreen.x, topRightScreen.x);
                y = bottomLeftScreen.y;
                break;
            default:
                print("erro");
                break;
        }
        Instantiate(enemy, new Vector3(x, y, 0), Quaternion.identity);
    }
}
