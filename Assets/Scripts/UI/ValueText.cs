using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

public class ValueText : MonoBehaviour
{
    public enum ValueVariant
    {
        Record,
        LivesCount,
        DiamondsCount
    }

    [SerializeField] private Text text;
    [SerializeField] private ValueVariant valueVariant;

    private void Awake()
    {
        DataManager.onDataChanged += Show;
    }

    private void OnEnable()
    {
        Show();
    }

    public void Show()
    {
        if (text == null)
            return;

        switch (valueVariant)
        {
            case ValueVariant.Record:
                text.text = Converter.ConvertToString(DataManager.record.GetValue());
                break;
            case ValueVariant.LivesCount:
                text.text = Converter.ConvertToString(DataManager.livesCount.GetValue());
                break;
            case ValueVariant.DiamondsCount:
                text.text = Converter.ConvertToString(DataManager.diamondsCount.GetValue());
                break;
        }
    }

    private void OnDestroy()
    {
        DataManager.onDataChanged -= Show;
    }
}
