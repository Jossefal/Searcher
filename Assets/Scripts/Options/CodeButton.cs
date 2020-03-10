using UnityEngine;

public class CodeButton : MonoBehaviour
{
    public char symbol;
    public SecretOpener secretOpener;
    
    public void EnterSymbol()
    {
        secretOpener.EnterSymbol(symbol);
    }
}
