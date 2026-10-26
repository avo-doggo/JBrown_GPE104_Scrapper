using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Enemies")]
    public List<PawnAI> Enemies = new List<PawnAI>();
    [Header("Scrap")]
    public List<CollectScrap> Scraps = new List<CollectScrap>();
    public List<CollectScrap> CollectedScrap = new List<CollectScrap>();

    [Header("Score")]
    public int score;
    public TMP_Text scoreTextUI;
    public TMP_Text scoreTextEndScreen;


    private void Awake()
    {
        // Make sure there is only one GameManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        FindEnemies();
        FindScrap();
    }

    public void FindEnemies()
    {
        // Clear the old level's enemies
        Enemies.Clear();

        // Find all enemies in the current level
        PawnAI[] foundEnemies = FindObjectsByType<PawnAI>();

        // Add them to the list
        foreach (PawnAI enemy in foundEnemies)
        {
            Enemies.Add(enemy);
        }

        Debug.Log("Enemies found: " + Enemies.Count);
    }

    public void FindScrap()
    {
        Scraps.Clear();
        CollectScrap[] foundScrap = FindObjectsByType<CollectScrap>();
        foreach (CollectScrap scrap in foundScrap)
        {
            Scraps.Add(scrap);
        }
        Debug.Log("Scrap in Level: " + Scraps.Count);
    }
    //ADD SCORE OF ITEMS
    public void AddScore(int amount)
    {
        score += amount;
        if (scoreTextUI != null)
        {
            scoreTextUI.text =  score.ToString();
        }
    }

    public void RemoveScrap(CollectScrap scrap)
    {
        if (scrap != null && Scraps.Contains(scrap))
        {
            Scraps.Remove(scrap);

            // Check for victory after an enemy has been removed.
        }
    }

    public void AddToCollected(CollectScrap collectedScrap)
    {

            CollectedScrap.Add(collectedScrap);

            Debug.Log("Collected Scrap: " + CollectedScrap.Count);

    }

}