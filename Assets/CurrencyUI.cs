using UnityEngine;
using TMPro; 

public class CurrencyUI : MonoBehaviour
{
    public TextMeshProUGUI currencyText; 
    private CurrencyManager currencyManager;

    void Start()
    {
        currencyManager = FindObjectOfType<CurrencyManager>();
        UpdateCurrencyDisplay();
    }

    public void UpdateCurrencyDisplay()
    {
        if (currencyManager != null)
        {
            currencyText.text = currencyManager.currencyAmount.ToString() + "$";
        }
    }
}
