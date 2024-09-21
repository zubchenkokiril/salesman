using UnityEngine;
using TMPro;

public class InteractionController : MonoBehaviour
{
    public TextMeshProUGUI interactionText;
    public float interactionDistance = 3f; 
    public int itemCost = 500; 
    private GameObject currentInteractableObject;
    private CurrencyManager currencyManager; 

    void Start()
    {
        
        currencyManager = FindObjectOfType<CurrencyManager>();

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
            
            if (currencyManager.currencyAmount >= itemCost)
            {
                currencyManager.SpendCurrency(itemCost); // Тратим валюту
                Destroy(currentInteractableObject); // Удаляем объект
                interactionText.gameObject.SetActive(false); // Скрываем текст взаимодействия
            }
            else
            {
                Debug.LogWarning("Недостаточно валюты для покупки!");
            }
        }
    }

    void CheckForInteractableObjects()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                Debug.Log("Объект обнаружен: " + hit.collider.name); // Логируем объект

                if (interactionText != null)
                {
                    interactionText.gameObject.SetActive(true);
                    interactionText.text = "Нажмите E для взаимодействия";
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
