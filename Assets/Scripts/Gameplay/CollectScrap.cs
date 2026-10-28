using UnityEngine;

public class CollectScrap : Interactable
{
    [Header("Scrap Type")]
    public bool largeScrap;
    public bool mediumScrap;
    public bool smallScrap;

    [Header("Scrap Score")]
    public int scoreValue;

    [Header("Collection Sound")]
    public AudioSource audioSource;
    public AudioClip collectSound;

    private CollectScrap scrapToMoveToCollected;

    public override void Interact()
    {
        collectScrap();
    }

    public void collectScrap()
    {
        if (GameManager.instance == null)
        {
            return;
        }

        // Play collection sound
        if (audioSource != null && collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }

        // Remove this scrap from the active Scrap list
        GameManager.instance.RemoveScrap(this);

        // Keep this scrap in the collected list
        GameManager.instance.AddToCollected(this);

        // Add the scrap's score
        GameManager.instance.AddScore(scoreValue);

        Debug.Log("Collected scrap!");

        // Make the scrap disappear from the level
        gameObject.SetActive(false);
    }
}