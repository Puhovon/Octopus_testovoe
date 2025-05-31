using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Naninovel;

public class MemoryGameController : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform gridContainer;
    [SerializeField] private Sprite[] cardSprites;
    [SerializeField] private Sprite cardBack;
    [SerializeField] private int totalPairs;

    private List<Card> cards = new List<Card>();
    private Card firstSelected;
    private Card secondSelected;
    private int pairsFound;
    private bool inputEnabled = true;

    public event System.Action OnGameCompleted;

    public void StartGame()
    {
        pairsFound = 0;
        CreateBoard();
    }

    private void CreateBoard()
    {
        foreach (Transform child in gridContainer) Destroy(child.gameObject);
        cards.Clear();
        
        var selectedSprites = new List<Sprite>();
        for (int i = 0; i < totalPairs; i++)
        {
            selectedSprites.Add(cardSprites[i % cardSprites.Length]);
            selectedSprites.Add(cardSprites[i % cardSprites.Length]);
        }
        
        for (int i = 0; i < selectedSprites.Count; i++)
        {
            int randomIndex = Random.Range(i, selectedSprites.Count);
            (selectedSprites[i], selectedSprites[randomIndex]) = 
                (selectedSprites[randomIndex], selectedSprites[i]);
        }
        
        for (int i = 0; i < selectedSprites.Count; i++)
        {
            var cardGO = Instantiate(cardPrefab, gridContainer);
            var card = cardGO.GetComponent<Card>();
            card.Initialize(selectedSprites[i], cardBack, OnCardClicked);
            cards.Add(card);
        }
    }

    private void OnCardClicked(Card card)
    {
        if (!inputEnabled || card.IsFlipped || card == firstSelected) 
            return;

        card.Flip();
        
        if (firstSelected == null)
        {
            firstSelected = card;
        }
        else
        {
            secondSelected = card;
            inputEnabled = false;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(1f);
        
        if (firstSelected.FaceSprite == secondSelected.FaceSprite)
        {
            pairsFound++;
            if (pairsFound >= totalPairs)
            {
                OnGameCompleted?.Invoke();
            }
        }
        else
        {
            firstSelected.Flip();
            secondSelected.Flip();
        }
        
        firstSelected = null;
        secondSelected = null;
        inputEnabled = true;
    }
}

