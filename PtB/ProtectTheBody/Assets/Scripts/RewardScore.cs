using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardScore : MonoBehaviour
{
    public LeanTweenType ease;
    public float lifeTime;
    private float timePass = 0f;
    
    void Start()
    {
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), .8f).setEase(ease);
    }
    
    void Update()
    {
        timePass += Time.deltaTime;
        if (timePass >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    public void Setup(Vector3 enemyPos, int score)
    {
        Vector2 worldPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        Vector2 canvasSize = new Vector2(this.transform.parent.GetComponent<RectTransform>().rect.width,
                                         this.transform.parent.GetComponent<RectTransform>().rect.height);

        float updatedPosX = enemyPos.x * (canvasSize.x / 2) / worldPos.x;
        float updatedPosY = enemyPos.y * (canvasSize.y / 2) / worldPos.y;

        Vector3 updatedPos = new Vector3(updatedPosX, updatedPosY, 0);

        GetComponent<RectTransform>().anchoredPosition = updatedPos;
        GetComponent<Text>().text = score.ToString();
    }
}
