using UnityEngine;
using UnityEngine.SceneManagement;

public class BorderManager : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
