using UnityEngine;
using UnityEngine.Events;

public class ScrollObject : UIScroller
{
    public Vector2 openedPos;
    public Vector2 closedPos;
    public bool isOpening;

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
