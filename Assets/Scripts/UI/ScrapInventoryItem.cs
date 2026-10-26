using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScrapInventoryItem : MonoBehaviour
{
    public Image scrapImage;
    public TMP_Text scrapName;
    public TMP_Text scrapValue;

    [Header("Scrap Sprites")]
    public Sprite largeScrapSprite;
    public Sprite mediumScrapSprite;
    public Sprite smallScrapSprite;

    public void SetScrap(CollectScrap scrap)
    {
        Debug.Log("Creating inventory item for: " + GetScrapType(scrap));

        scrapName.text = GetScrapType(scrap);
        scrapValue.text = "Value: " + scrap.scoreValue;

        SetScrapImage(scrap);
    }

    private string GetScrapType(CollectScrap scrap)
    {
        if (scrap.largeScrap)
        {
            return "Large Scrap";
        }

        if (scrap.mediumScrap)
        {
            return "Medium Scrap";
        }

        if (scrap.smallScrap)
        {
            return "Small Scrap";
        }

        return "Scrap";
    }
    private void SetScrapImage(CollectScrap scrap)
    {
        if (scrap.largeScrap)
        {
            scrapImage.sprite = largeScrapSprite;
        }
        else if (scrap.mediumScrap)
        {
            scrapImage.sprite = mediumScrapSprite;
        }
        else if (scrap.smallScrap)
        {
            scrapImage.sprite = smallScrapSprite;
        }
    }
}