using UnityEngine;
using UnityEngine.Events;

#pragma warning disable 649

public class ScrollObject : UIScroller
{
    [SerializeField] private Vector2 openedPos;
    [SerializeField] private Vector2 closedPos;
    [SerializeField] private bool isOpening;
    [SerializeField] protected UnityEvent onOpenEnd = null;
    [SerializeField] protected UnityEvent onCloseEnd = null;

    protected override void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public virtual void Open()
    {
        if (!isOpening)
        {
            isOpening = true;
            ScrollTo(openedPos);
        }
    }

    public virtual void Close()
    {
        if (isOpening)
        {
            isOpening = false;
            ScrollTo(closedPos);
        }
    }

    public virtual void OpenDircetly()
    {
        if (!isOpening)
        {
            isOpening = true;
            rectTransform.anchoredPosition = openedPos;
            OnEndScroll();
        }
    }

    public virtual void CloseDircetly()
    {
        if (isOpening)
        {
            isOpening = false;
            rectTransform.anchoredPosition = closedPos;
            OnEndScroll();
        }
    }

    protected override void OnEndScroll()
    {
        if (isOpening)
            onOpenEnd.Invoke();
        else
            onCloseEnd.Invoke();
    }
}
