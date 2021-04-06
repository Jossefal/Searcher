using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class SnapScrollIndicator : MonoBehaviour
{
    [SerializeField] private SnapScroll snapScroll;
    [SerializeField] private Image[] images;
    [SerializeField] private Color normalColor;
    [SerializeField] private Color highlightedColor;

    private void Start()
    {
        snapScroll.onObjectHighlight += UpdateImagesState;
    }

    private void OnEnable()
    {
        UpdateImagesState(snapScroll.indexOfHighlightedObject);
    }

    private void UpdateImagesState(int highlightedIndex)
    {
        for (int i = 0; i < images.Length; i++)
        {
            if (i == highlightedIndex)
                images[i].color = highlightedColor;
            else
                images[i].color = normalColor;
        }
    }
}
