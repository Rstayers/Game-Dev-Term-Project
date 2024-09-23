using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHealthBar : MonoBehaviour
{
    public static UIHealthBar instance;
    [SerializeField] private GameObject heartContainer;
    [SerializeField] private List<GameObject> hearts;
    [SerializeField] Stats healthStats;
    int totalHearts;
    float currentHearts;
    HeartContainer currentContainer;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        hearts = new List<GameObject>();
    }
    public void SetUpHearts(int heartsInitial)
    {
        /*
         *  Instantiate the Player's health bar and set up linked list of hearts
         */
        hearts.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        totalHearts = heartsInitial;
        currentHearts = (float)totalHearts;
        for (int i = 0; i < totalHearts; i++)
        {
            GameObject newHeart = Instantiate(heartContainer, transform);
            hearts.Add(newHeart);
            if (currentContainer != null)
            {
                currentContainer.next = newHeart.GetComponent<HeartContainer>();
            }
            currentContainer = newHeart.GetComponent<HeartContainer> ();
        }
        currentContainer = hearts[0].GetComponent<HeartContainer>();
    }
    public void SetCurrentHealth(float health)
    {
        currentHearts = health;
        currentContainer.SetHeart(currentHearts);
    }
    public void AddHearts(float healthUp)
    {
        currentHearts += healthUp;
        if(currentHearts > totalHearts)
        {
            currentHearts = (float)totalHearts;
        }
        currentContainer.SetHeart (currentHearts);
    }

    public void RemoveHearts(float healthDown)
    {
        currentHearts -= healthDown;
        if (currentHearts < 0)
        {
            currentHearts = 0;
        }
        currentContainer.SetHeart(currentHearts);
    }
    public void AddContainer()
    {
        /*
         * Add a new container to the linked list of hearts
         */
        GameObject newHeart = Instantiate(heartContainer, transform);
        currentContainer = hearts[hearts.Count - 1].GetComponent<HeartContainer>();
        hearts.Add(newHeart);
        if(currentContainer != null)
        {
            currentContainer.next = newHeart.GetComponent<HeartContainer>();
        }

        currentContainer = hearts[0].GetComponent<HeartContainer> ();
        totalHearts++;
        currentHearts = totalHearts;
        SetCurrentHealth(currentHearts);
    }
}
