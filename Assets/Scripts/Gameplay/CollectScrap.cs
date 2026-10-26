using Unity.VisualScripting;
using UnityEngine;

public class CollectScrap : Interactable
{
    [Header("Scrap Type")]
    public bool largeScrap;
    public bool mediumScrap;
    public bool smallScrap;

    [Header("Scrap Score")]
    public int scoreValue;

    private CollectScrap scrapToMoveToCollected;
    public override void Interact()
    {
        collectScrap();
    }

    public void collectScrap()
    {
        GameManager.instance.AddScore(scoreValue);
        if (GameManager.instance!=null)
        {
            if (GameManager.instance == null)
            {
                return;
            }

            // Remove this scrap from the level's Scrap list
            GameManager.instance.RemoveScrap(this);

            // Add this scrap to the collected list
            GameManager.instance.AddToCollected(this);

            // Add the scrap's score
            GameManager.instance.AddScore(scoreValue);

            Debug.Log("Collected scrap!");

            

        }


    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
