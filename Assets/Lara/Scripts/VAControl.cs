using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class VAControl : MonoBehaviour
{
    public GameObject explosion;
    RectTransform virusPanel;
    RectTransform rt;
    Color startColor = new Color(0.02352941f, 1f, 0.7254902f);
    Color endColor = new Color(0.9254902f, 0.1647059f, 0.4156863f);
    Color lerpedColor;

    [SerializeField] private VirusCounter _counter;
    [SerializeField] private UIntegrityBar _uibar;
     

    void Start()
    {
        _counter = GameObject.Find("counter").GetComponent<VirusCounter>();

        _uibar = GameObject.Find("UIntegrity bar").GetComponent<UIntegrityBar>();

        rt = gameObject.GetComponent<RectTransform>();
        InvokeRepeating(nameof(NewPos), 0f, Random.Range(1f, 5f));
        lerpedColor = startColor;
        gameObject.GetComponent<Image>().color = lerpedColor;

        StartCoroutine(ChangeColour());     //pian piano diventa da verde a rosso (quando sarà rosso esploderà- devi ucciderlo prima che accada) 
    }

    void Update()
    {
        if(gameObject.GetComponent<Image>().color == endColor)  //se un virus diventa rosso 
        {
            
            _uibar.SetDamage(10); //danno di -10% alla integrity bar
            Destroy(gameObject); //si autodistrugge
                                  //QUI dovrà anche attivarsi l'esplosione
        }
    }


    void NewPos()
    {
        float xPos = Random.Range(0, Screen.width);
        float yPos = Random.Range(0, Screen.height);

        Vector3 newPos = new Vector3(xPos, yPos, 0f);

        transform.DOMove(newPos, 5);

        //rt.anchoredPosition = newPos;
    }

    public void Distruggiti()
    {
        if (_counter.GetKilledNum() != 30)  //se non ne ha ancora uccisi 30 allora aggiorna il counter 
        {
            _counter.OneMoreKilled();
        }
        else
        {   //arrivi qui se ne hai uccisi quanti dovevi (30)

            VirusAttack.spawnAllowed = false;   //smette di spawnare 
            _counter.ResetKilledCounter();      //resetta il counter 
            Destroy(gameObject);                //cancella i viruss
        }

        Destroy(gameObject);
    }

    private IEnumerator ChangeColour()
    {
        float tick = 0f;
        while (gameObject.GetComponent<Image>().color != endColor)
        {
            tick += Time.deltaTime * 0.14f;
            gameObject.GetComponent<Image>().color = Color.Lerp(startColor, endColor, tick);
            yield return null;
        }
    }
}
