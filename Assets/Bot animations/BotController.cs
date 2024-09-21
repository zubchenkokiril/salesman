using UnityEngine;
using TMPro;

public class BotController : MonoBehaviour
{
    public TextMeshProUGUI interactionText; 
    public float interactionDistance = 3f; 
    private GameObject currentInteractableObject;
    public CurrencyManager currencyManager; 
    public int coffeePrice = 5; 

   
    public delegate void BotDestroyed();
    public event BotDestroyed OnBotDestroyedEvent;

    void Start()
    {
        
        if (interactionText != null)
        {
            interactionText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        CheckForInteractableObjects();

        if (Input.GetKeyDown(KeyCode.E) && currentInteractableObject != null)
        {
            if (currentInteractableObject.CompareTag("Bot"))
            {
                Debug.Log("Bot bought coffee. Adding money to player.");

                if (currencyManager != null)
                {
                    currencyManager.AddCurrency(coffeePrice); 
                }

                Destroy(currentInteractableObject);

                OnBotDestroyedEvent?.Invoke();

                interactionText.gameObject.SetActive(false);
            }
        }
    }

    void CheckForInteractableObjects()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("Interactable") || hit.collider.CompareTag("Bot"))
            {
                Debug.Log("Object detected: " + hit.collider.name); 

                if (interactionText != null)
                {
                    interactionText.gameObject.SetActive(true);
                    interactionText.text = "Press E to sell coffee";
                }

                currentInteractableObject = hit.collider.gameObject;
            }
        }
        else
        {
            if (interactionText != null)
            {
                interactionText.gameObject.SetActive(false);
            }
            currentInteractableObject = null;
        }
    }
}
