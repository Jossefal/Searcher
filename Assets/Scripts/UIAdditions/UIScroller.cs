using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class UIScroller : MonoBehaviour
{
    public float speed;

    protected RectTransform rectTransform = null;

    [SerializeField] protected UnityEvent onScrollingEnd = null;

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
