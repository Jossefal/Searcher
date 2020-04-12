using System.Collections;
using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 649

public class UIScroller : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] protected UnityEvent onScrollingEnd = null;

    protected RectTransform rectTransform = null;

    protected virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void ScrollTo(Vector2 targetPos)
    {
        StartCoroutine(Scroller(targetPos));
    }

    protected IEnumerator Scroller(Vector2 targetPos)
    {
        while (true)
        {
            if (rectTransform.anchoredPosition != targetPos)
                rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, targetPos, speed * Time.unscaledDeltaTime);
            else
                break;

            yield return null;
        }

        OnEndScroll();
    }

    protected virtual void OnEndScroll()
    {
        onScrollingEnd.Invoke();
    }
}
