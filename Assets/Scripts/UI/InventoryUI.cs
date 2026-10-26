using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;

    public TMP_Text scrapCountText;
    public TMP_Text scrapValueText;
    public GameObject scrapInventoryPrefab;
    public Transform scrapContainer;        
    
    public float totalValue;

    private void Start()
    {
        inventoryPanel.SetActive(false);
    }

    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        UpdateInventory();
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
    }

    public void UpdateInventory()
    {
        // Remove old inventory UI
        foreach (Transform child in scrapContainer)
        {
            Destroy(child.gameObject);
        }



        foreach (CollectScrap scrap in GameManager.instance.CollectedScrap)
        {
            GameObject newItem = Instantiate(scrapInventoryPrefab,scrapContainer);

            ScrapInventoryItem item =
                newItem.GetComponent<ScrapInventoryItem>();

            item.SetScrap(scrap);

            
        }

        scrapCountText.text = GameManager.instance.CollectedScrap.Count.ToString();

        scrapValueText.text = GameManager.instance.score.ToString();
;
    }
}