using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class BotInteraction : MonoBehaviour
{
    public CurrencyManager currencyManager; 
    public int rewardAmount = 10; 
    public string dialogueText = "Привет! Вот тебе немного валюты."; 

    public GameObject dialoguePanel; 
    public TextMeshProUGUI dialogueTextComponent; 

    private bool hasGivenReward = false; 

    void Start()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasGivenReward) 
        {
            ShowDialogue();
            GiveReward();
            hasGivenReward = true; 
        }
    }

    void Update()
    {
        if (dialoguePanel.activeSelf && Input.GetKeyDown(KeyCode.Return)) 
        {
            HideDialogue();
        }
    }

    void ShowDialogue()
    {
        if (dialoguePanel != null && dialogueTextComponent != null)
        {
            dialogueTextComponent.text = dialogueText;
            dialoguePanel.SetActive(true); 
        }
    }

    void HideDialogue()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false); 
        }
    }

    void GiveReward()
    {
        if (currencyManager != null)
        {
            currencyManager.AddCurrency(rewardAmount); 
        }
    }
}
