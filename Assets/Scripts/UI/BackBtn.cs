using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BackBtn : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            button.onClick?.Invoke();
        }
    }

}
