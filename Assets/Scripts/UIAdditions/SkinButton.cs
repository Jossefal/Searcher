using UnityEngine;
using UnityEngine.UI;

public class SkinButton : MonoBehaviour
{
    public int skinIndex;
    public SkinsContainer skinsContainer;
    public SkinsMenu skinsMenu;
    public Image selectedImage;

    private Button buttonComponent;

    private void Awake()
    {
        buttonComponent = GetComponent<Button>();
        GetComponent<Image>().sprite = skinsContainer.GetSkin(skinIndex);
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
