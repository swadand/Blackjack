using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private GameObject deckTop;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private bool Hit = false;
    private GameObject Spawned1 = null;
    private GameObject Spawned2 = null;
    private GameObject Spawned3 = null;
    public TMP_Text scoreText;

    public int playerscore = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Hit)
        {
            Setup();
            Hit = false;
        }
    }


    void Setup()
    {
        var card = Instantiate(cardPrefab, deckTop.transform.position, deckTop.transform.rotation);
        Card cardScript = card.GetComponent<Card>();
        cardScript.SetCard();
        playerscore += cardScript.GetScore();
        scoreText.text = playerscore.ToString();

        StartCoroutine(ReorderCards(card));
    }

    IEnumerator ReorderCards(GameObject card)
    {   
        yield return new WaitForSeconds(1);

        if (Spawned1 != null)
        {
            if (Spawned2 != null)
            {
                if (Spawned3 != null)
                {
                    Destroy(Spawned3);
                }
                //Spawned2.GetComponent<Card>().SetParentPos(3);
                //Spawned3 = Spawned2;
            }
            //Spawned1.GetComponent<Card>().SetParentPos(2);
            //Spawned2 = Spawned1;
            Destroy(Spawned2);
        }
        Spawned1 = card;
        Spawned1.GetComponent<Card>().SetParentPos(1);
    }
}
