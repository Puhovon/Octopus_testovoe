using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Naninovel;

public class MemoryGameController : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private Card cardPrefab;
    [SerializeField] private Sprite[] cardSprites;
    [SerializeField] private int pairsCount = 8;
    
    private Card firstSelected;
    private Card secondSelected;
    private bool inputEnabled = true;
    private UniTaskCompletionSource<bool> completionSource;

    public async UniTask<bool> PlayGameAsync()
    {
        completionSource = new UniTaskCompletionSource<bool>();
        GenerateCards();
        UniTask.WaitUntil(() => completionSource.Task.Result);
        return await completionSource.Task;
    }

    private void GenerateCards()
    {
        List<int> values = new List<int>();
        for (int i = 0; i < pairsCount; i++)
        {
            values.Add(i);
            values.Add(i);
        }
        
        for (int i = 0; i < values.Count; i++)
        {
            int temp = values[i];
            int r = UnityEngine.Random.Range(i, values.Count);
            values[i] = values[r];
            values[r] = temp;
        }

        for (int i = 0; i < values.Count; i++)
        {
            Card card = Instantiate(cardPrefab, grid.transform);
            card.Initialize(values[i], cardSprites[values[i]], OnCardClicked);
        }
    }

    private async void OnCardClicked(Card card)
    {
        if (!inputEnabled || card == firstSelected || card.IsMatched) return;
        
        card.FlipOpen();
        
        if (firstSelected == null)
        {
            firstSelected = card;
        }
        else
        {
            secondSelected = card;
            inputEnabled = false;
            
            if (firstSelected.CardValue == secondSelected.CardValue)
            {
                firstSelected.MarkAsMatched();
                secondSelected.MarkAsMatched();
                
                if (CheckGameComplete())
                {
                    await UniTask.Delay(1000);
                    completionSource.TrySetResult(true);
                }
            }
            else
            {
                // Карточки не совпали
                await UniTask.Delay(1000);
                firstSelected.FlipClosed();
                secondSelected.FlipClosed();
            }
            
            firstSelected = null;
            secondSelected = null;
            inputEnabled = true;
        }
    }

    private bool CheckGameComplete()
    {
        string s = "";
        foreach (Transform child in grid.transform)
        {
            Card card = child.GetComponent<Card>();
            s += $"{card.CardValue} is {card.IsMatched}\n";
            if (!card.IsMatched) return false;
        }
        print(s);
        return true;
    }
}