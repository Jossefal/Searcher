using UnityEngine;

#pragma warning disable 649

public class CodeButton : MonoBehaviour
{
    [SerializeField] private char symbol;
    [SerializeField] private SecretOpener secretOpener;

    public void EnterSymbol()
    {
        secretOpener.EnterSymbol(symbol);
    }
}
