using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public int currencyAmount = 0;
    public CurrencyUI currencyUI; 

    void Start()
    {
        UpdateCurrencyUI();
    }

    public void AddCurrency(int amount)
    {
        currencyAmount += amount;
        UpdateCurrencyUI();
    }

    public void SpendCurrency(int amount)
    {
        if (currencyAmount >= amount)
        {
            currencyAmount -= amount;
            UpdateCurrencyUI();
        }
        else
        {
            Debug.LogWarning("Not enough currency!");
        }
    }

    void UpdateCurrencyUI()
    {
        if (currencyUI != null)
        {
            currencyUI.UpdateCurrencyDisplay();
        }
    }
    public int GetCurrencyAmount()
{
    return currencyAmount;
}
}
