using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    public int skinIndex;
    public Sprite skinIcon;
    public Image selectedImage;

    private SkinsMenu skinsMenu;
    private Button buttonComponent;

    private void Awake()
    {
        buttonComponent = GetComponent<Button>();
        skinsMenu = GameObject.FindWithTag("SkinsMenu").GetComponent<SkinsMenu>();
    }

    public void ChangeSkin()
    {
        skinsMenu.ChangeSkin(this);
    }

    public void Select()
    {
        if(buttonComponent == null)
            buttonComponent = GetComponent<Button>();
        buttonComponent.interactable = false;
        selectedImage.gameObject.SetActive(true);
    }

    public void Deselect()
    {
        if(buttonComponent == null)
            buttonComponent = GetComponent<Button>();
        buttonComponent.interactable = true;
        selectedImage.gameObject.SetActive(false);
    }
}
