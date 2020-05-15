using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private int startIndex;
    [SerializeField] private string[] values;
    [SerializeField] private Text text;
    [SerializeField] private Animator animator;

    private void Start()
    {
        currentIndex = Mathf.Clamp(startIndex, 0, values.Length - 1);
        text.text = values[currentIndex];
    }

    public void ChoosePrev()
    {
        animator.SetTrigger("choose");

        if(currentIndex == 0)
            currentIndex = values.Length - 1;
        else
            currentIndex--;

        text.text = values[currentIndex];
    }

    public void ChooseNext()
    {
        animator.SetTrigger("choose");
        
        if(currentIndex == values.Length - 1)
            currentIndex = 0;
        else
            currentIndex++;

        text.text = values[currentIndex];
    }
}
