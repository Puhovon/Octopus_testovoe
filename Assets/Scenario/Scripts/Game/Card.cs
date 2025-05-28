using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image faceImage;
    [SerializeField] private Image backImage;
    [SerializeField] private Button button;
    
    public Sprite FaceSprite { get; private set; }
    public bool IsFlipped { get; private set; }
    
    public void Initialize(Sprite face, Sprite back, System.Action<Card> onClick)
    {
        FaceSprite = face;
        faceImage.sprite = face;
        backImage.sprite = back;
        button.onClick.AddListener(() => onClick(this));
        IsFlipped = false;
        backImage.gameObject.SetActive(true);
    }

    public void Flip()
    {
        IsFlipped = !IsFlipped;
        faceImage.gameObject.SetActive(IsFlipped);
        backImage.gameObject.SetActive(!IsFlipped);
    }
}