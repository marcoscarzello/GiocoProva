using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VAControl : MonoBehaviour
{
    public GameObject explosion;
    RectTransform virusPanel;
    RectTransform rt;
    Color startColor = new Color(0.02352941f, 1f, 0.7254902f);
    Color endColor = new Color(0.9254902f, 0.1647059f, 0.4156863f);
    Color lerpedColor;
    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();

        InvokeRepeating(nameof(NewPos), Random.Range(1, 2), 1f);
        lerpedColor = startColor;
        gameObject.GetComponent<Image>().color = lerpedColor;

        StartCoroutine(ChangeColour());
    }

    void Update()
    {
    }


    void NewPos()
    {
        float xPos = Random.Range(0, 1920);
        float yPos = Random.Range(0, 1080);
        Vector3 newPos = new Vector3(xPos, yPos, 0f);
        rt.anchoredPosition = newPos;
    }

    public void Distruggiti()
    {
        Destroy(gameObject);
    }

    private IEnumerator ChangeColour()
    {
        float tick = 0f;
        while (gameObject.GetComponent<Image>().color != endColor)
        {
            tick += Time.deltaTime * 0.05f;
            gameObject.GetComponent<Image>().color = Color.Lerp(startColor, endColor, tick);
            yield return null;
        }
    }
}
