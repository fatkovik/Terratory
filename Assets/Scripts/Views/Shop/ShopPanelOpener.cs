using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ShopPanelOpener : MonoBehaviour
{
    private int toggleButtonOpenPosition = -925;
    private int layoutOpenPosition = -1200;

    private int toggleButtonClosedPosition;
    private int layoutClosedPosition;

    [SerializeField] private HorizontalLayoutGroup shopButtonsLayout;
    [SerializeField] private Button shopToggleButton;

    bool isOpen = false;

    public void OnEnable()
    {
        this.toggleButtonClosedPosition = toggleButtonOpenPosition - 200;
        this.layoutClosedPosition = layoutOpenPosition - 200;
    }

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

        shopButtonsLayout.transform.DOLocalMoveY(layoutClosedPosition, 0.5f);
        shopToggleButton.transform.DOLocalMoveY(toggleButtonClosedPosition, 0.5f);

        this.isOpen = false;
    }

    public void OpenPanel()
    {
        if (shopButtonsLayout == null && shopToggleButton == null)
        {
            return;
        }

        shopButtonsLayout.transform.DOLocalMoveY(layoutOpenPosition, 0.5f);
        shopToggleButton.transform.DOLocalMoveY(toggleButtonOpenPosition, 0.5f);

        this.isOpen = true;
    }
}
