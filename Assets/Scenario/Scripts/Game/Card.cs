using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image frontImage;
    [SerializeField] private Image backImage;
    [SerializeField] private Button button;
    
    public int CardValue { get; private set; }
    public bool IsMatched { get; private set; }
    
    public void Initialize(int value, Sprite frontSprite, System.Action<Card> onClick)
    {
        CardValue = value;
        frontImage.sprite = frontSprite;
        button.onClick.AddListener(() => onClick(this));
        FlipClosed();
    }

    public void FlipOpen()
    {
        frontImage.gameObject.SetActive(true);
        backImage.gameObject.SetActive(false);
    }

    public void FlipClosed()
    {
        frontImage.gameObject.SetActive(false);
        backImage.gameObject.SetActive(true);
    }

    public void MarkAsMatched()
    {
        IsMatched = true;
        button.interactable = false;
        // Эффект совпадения
        var color = frontImage.color;
        color.a = .5f;
        frontImage.material.color = color;

    }
}