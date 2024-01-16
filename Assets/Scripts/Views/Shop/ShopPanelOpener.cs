using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopPanelOpener : MonoBehaviour
{
    [SerializeField] private HorizontalLayoutGroup shopButtonsLayout;
    [SerializeField] private Button shopToggleButton;

    bool isOpen = false;

    public void TogglePanel()
    {
        if (!isOpen)
        {
            OpenPanel();
            return;
        }
        ClosePanel();
    }

    public void ClosePanel()
    {
        if (shopButtonsLayout == null && shopToggleButton == null)
        {
            return;
        }

        shopButtonsLayout.transform.DOLocalMoveY(-1500, 0.5f);
        shopToggleButton.transform.DOLocalMoveY(-1150, 0.5f);

        this.isOpen = false;
    }

    public void OpenPanel()
    {
        if (shopButtonsLayout == null && shopToggleButton == null)
        {
            return;
        }

        shopButtonsLayout.transform.DOLocalMoveY(-1200, 0.5f);
        shopToggleButton.transform.DOLocalMoveY(-900, 0.5f);

        this.isOpen = true;
    }
}
