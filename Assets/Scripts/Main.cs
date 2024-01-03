using System;
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
    private GameObject playerSpawned1 = null;
    private GameObject opSpawned1 = null;
    public TMP_Text scoreText;
    public TMP_Text opScoreText;
    private float timeInterval;
    // private bool gameJustStarted = false;
    private bool playerStand = false;
    private bool gameOver = false;

    public int playerScore = 0;
    public int opScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "0";
        opScoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScore > 21 && !playerStand)
        {
            if ((Time.time - timeInterval) > 1)
            {
                Debug.Log("Lost!");
                gameOver = true;
                scoreText.text = "You Lost!";
                DestroyAllCards();
            }
        }

        if (opScore > playerScore && opScore < 22)
        {
            gameOver = true;
            playerStand = false;
            scoreText.text = "You Lost!";
            DestroyAllCards();
        }

        if (opScore > 21)
        {
            gameOver = true;
            playerStand = false;
            scoreText.text = "You Won!";
            DestroyAllCards();
        }
    }

    public void Stand()
    {
        Debug.Log("Stand call.");
        playerStand = true;
        StartCoroutine(OpTurn());
        Debug.Log("Stand end.");
    }

    IEnumerator OpTurn()
    {
        while (opScore < playerScore && !gameOver)
        {
            if (opScore >= 15 && opScore <= 21)
            {
                if (OpLogic())
                {
                    OpponentHit();
                    Debug.Log("risk taken");
                    //return;
                }
                else
                {
                    scoreText.text = "You Won!";
                    Debug.Log("Won!");
                    gameOver = true;
                    playerStand = false;
                    DestroyAllCards();
                }
            }
            OpponentHit();
            Debug.Log("opHit");
            yield return new WaitForSeconds(1.5f);
        }
        DestroyAllCards();
    }

    bool OpLogic()
    {
        int n = UnityEngine.Random.Range(1, 11);
        return n switch
        {
            1 or 2 or 3 or 4 or 5 => true,
            6 or 7 or 8 or 9 or 10 => false,
            _ => true,
        };
    }

    /*private void gameOver()
    {
        DestroyAllCards();
    }
    */

    private void DestroyAllCards()
    {
        playerScore = 0;
        opScore = 0;
        Destroy(playerSpawned1);
        Destroy(opSpawned1);
    }

    void Hit()
    {
        gameOver = false;
        scoreText.text = playerScore.ToString();
        opScoreText.text = opScore.ToString();
        if ((Time.time - timeInterval) > 1)
        {
            timeInterval = Time.time;
            var card = Instantiate(cardPrefab, deckTop.transform.position, deckTop.transform.rotation);
            Card cardScript = card.GetComponent<Card>();
            cardScript.SetCard();
            playerScore += cardScript.GetScore();
            scoreText.text = playerScore.ToString();

            StartCoroutine(ReorderCards(card));
        }
    }

    void OpponentHit()
    {
        if ((Time.time - timeInterval) > 1)
        {
            timeInterval = Time.time;
            var card = Instantiate(cardPrefab, deckTop.transform.position, deckTop.transform.rotation);
            Card cardScript = card.GetComponent<Card>();
            cardScript.SetOpCard();
            opScore += cardScript.GetScore();
            opScoreText.text = opScore.ToString();

            StartCoroutine(ReorderOpCards(card));
        }
    }

    IEnumerator ReorderOpCards(GameObject card)
    {
        yield return new WaitForSeconds(1);

        if (opSpawned1 != null) Destroy(opSpawned1);
        opSpawned1 = card;
    }

    IEnumerator ReorderCards(GameObject card)
    {
        yield return new WaitForSeconds(1);

        if (playerSpawned1 != null) Destroy(playerSpawned1);
        playerSpawned1 = card;
    }
}
