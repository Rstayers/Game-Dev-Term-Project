using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;
using System.Linq;

public class JournalManager : MonoBehaviour
{
    [Header("UI Properties")]
    public GameObject journalUI;

    private List<JournalPage> discoveredPages = new List<JournalPage>();
    private List<JournalPage> pages = new List<JournalPage>();
    private JournalPage leftPage, rightPage;
    private bool isJournalOpen = false;
    

    private bool canChange = true;
    bool turning = false;
    private void Start()
    {
        pages = GetComponentsInChildren<JournalPage>(true).ToList();
        UpdatePages();
        if (discoveredPages.Count > 0)
        {
            rightPage = discoveredPages[0];
            leftPage = rightPage.prev;
            Debug.Log(rightPage);
            Debug.Log(rightPage.prev);

        }

    }
    private void UpdatePages()
    {
        discoveredPages = new List<JournalPage>();
        //add enabled pages to discovered
        for (int i = 0; i < pages.Count; i++)
        {

            if (pages[i].discovered)
            {
                discoveredPages.Add(pages[i]);
                pages[i].gameObject.SetActive(true);
            }
            else
                pages[i].gameObject.SetActive(false);
        }
        discoveredPages.Reverse();
        //set up linked list for discovered pages
        for (int i = 0; i < discoveredPages.Count; i++)
        {
            if (i < discoveredPages.Count - 1)
                discoveredPages[i].next = discoveredPages[i + 1];
            if (i > 0)
                discoveredPages[i].prev = discoveredPages[i - 1];
        }
    }
    public void ToggleJournal(InputAction.CallbackContext ctx)
    {

        if (!ctx.performed || discoveredPages.Count == 0)
            return;
        if (isJournalOpen)
        {
            CloseJournal();
        }
        else
        {
            OpenJournal();
        }
    }

    public void ChangePage(InputAction.CallbackContext ctx)
    {
        if (!isJournalOpen) return;
        Vector3 direction = ctx.ReadValue<Vector2>();
        if (direction == Vector3.zero)
        {
            canChange = true;
            return;
        }
        if (!canChange || turning)
            return;
        canChange = false;
        if (direction.x < 0)
        {
            // Switch left (previous pages)
            
            FlipLeft();
            
        }
        else if (direction.x > 0)
        {
            // Switch right (next pages)
          
            FlipRight();
            
        }
    }

    public void DiscoverPage(JournalPage page)
    {
        if (!discoveredPages.Contains(page))
        {
            for (int i = 0; i < pages.Count; i++)
            {
                if (pages[i].pages == page.pages)
                {
                    pages[i].discovered = true;
                    break;
                }
            }
            UpdatePages();
            rightPage = page;
            leftPage = page.prev;
            OpenJournal();
            foreach(var _page in discoveredPages)
            {
                if (_page == rightPage)
                    return;
                Vector3 targetRotRight = new Vector3(0, -180, 0) + _page.transform.eulerAngles;
                _page.transform.eulerAngles = targetRotRight;
            }
            
            // Play anim and audio
        }
    }

    private void OpenJournal()
    {
        isJournalOpen = true;
        Debug.Log("open");
        journalUI.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.Flash);
    }

    private void CloseJournal()
    {
        isJournalOpen = false;
        journalUI.transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.Flash);
    }

   

    private void FlipLeft()
    {
        if (leftPage != null)
        {
            turning = true;
            Vector3 targetRotRight = new Vector3(0, -180, 0) + leftPage.transform.eulerAngles;
            leftPage.transform.DORotate(targetRotRight, 1f).OnComplete(()=> { 
                turning = false;
                
            });
            rightPage = leftPage;
            
            leftPage = leftPage.prev;

        }
    }

    private void FlipRight()
    {
       if(rightPage != null)
       {
            turning = true;
            Vector3 targetRotLeft = new Vector3(0, 180, 0) + rightPage.transform.eulerAngles;

            rightPage.transform.DORotate(targetRotLeft, 1f).OnComplete(() => {
                turning = false;
            });
            var tmp = rightPage.prev;
            leftPage = rightPage;
            rightPage = rightPage.next;
            UpdatePages();
        }
    }
}
