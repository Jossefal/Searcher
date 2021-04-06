using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

#pragma warning disable 649

public class TextChooser : MonoBehaviour
{
    public int currentIndex { get; private set; }
    public string currentValue
    {
        get
        {
            return values[currentIndex];
        }
    }
    public bool isEmptyValue { get; private set; }

    [SerializeField] private int emptyValueIndex;
    [SerializeField] private string[] values;
    [SerializeField] private Text prevText;
    [SerializeField] private Text text;
    [SerializeField] private Text nextText;
    [SerializeField] private Animator animator;
    [SerializeField] private UnityEvent onValueChanged;

    private void Start()
    {
        currentIndex = Mathf.Clamp(emptyValueIndex, 0, values.Length - 1);
        text.text = values[currentIndex];

        if(currentIndex == emptyValueIndex)
            isEmptyValue = true;

        prevText.text = currentIndex > 0 ? values[currentIndex - 1] : values[values.Length - 1];
        nextText.text = currentIndex < values.Length - 1 ? values[currentIndex + 1] : values[0];
    }

    public void ChoosePrev()
    {
        animator.SetTrigger("choose");

        if(currentIndex == 0)
            currentIndex = values.Length - 1;
        else
            currentIndex--;

        text.text = values[currentIndex];
        prevText.text = currentIndex > 0 ? values[currentIndex - 1] : values[values.Length - 1];
        nextText.text = currentIndex < values.Length - 1 ? values[currentIndex + 1] : values[0];

        if(currentIndex == emptyValueIndex)
            isEmptyValue = true;
        else
            isEmptyValue = false;
        
        onValueChanged.Invoke();
    }

    public void ChooseNext()
    {
        animator.SetTrigger("choose");
        
        if(currentIndex == values.Length - 1)
            currentIndex = 0;
        else
            currentIndex++;

        text.text = values[currentIndex];
        prevText.text = currentIndex > 0 ? values[currentIndex - 1] : values[values.Length - 1];
        nextText.text = currentIndex < values.Length - 1 ? values[currentIndex + 1] : values[0];
        
        if(currentIndex == emptyValueIndex)
            isEmptyValue = true;
        else
            isEmptyValue = false;
        
        onValueChanged.Invoke();
    }
}
