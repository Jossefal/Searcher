using UnityEngine;

public class SecretOpener : MonoBehaviour
{
    public string code;
    public GameObject secretObject;

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
