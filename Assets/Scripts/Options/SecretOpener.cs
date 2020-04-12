using UnityEngine;

#pragma warning disable 649

public class SecretOpener : MonoBehaviour
{
    [SerializeField] private string code;
    [SerializeField] private GameObject secretObject;

    private int currentSymbolIndex = 0;

    public void EnterSymbol(char symbol)
    {
        if (code.Length == 0)
        {
            Open();
        }
        else if (symbol == code[currentSymbolIndex])
        {
            currentSymbolIndex++;
            if (currentSymbolIndex == code.Length)
                Open();
        }
        else
            Restart();
    }

    public void Restart()
    {
        currentSymbolIndex = 0;
    }

    private void Open()
    {
        secretObject.SetActive(true);
        Restart();
    }
}
