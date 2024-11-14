using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; 
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int currencyCount = 0;
    [SerializeField] private TextMeshProUGUI currencyText;

    private void Start()
    {
        currencyText.text = currencyCount.ToString();
    }
    public void UpdateCurrency(int currency)
    {
        currencyCount += currency;
        if (currencyCount < 0)
        {
            currencyCount = 0;
        }
        currencyText.text = currencyCount.ToString();
    }
}
