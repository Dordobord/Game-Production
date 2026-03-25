using Unity.VisualScripting;
using UnityEngine;
public class UICookbook : MonoBehaviour
{
    [SerializeField]private GameObject panel;
    [SerializeField]private GameObject[] pages;
    [SerializeField]private GameObject nextBtn;
    [SerializeField]private GameObject backBtn;

    private int currentPage = 0;
    private bool isOpen = false;
    public bool IsOpen => isOpen;

    private void Awake()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    public void OpenCookbook()
    {
        if (panel == null || pages == null || pages.Length == 0)
            return;

        isOpen = true;
        panel.SetActive(true);

        currentPage = 0;
        ShowPage(currentPage);
        UpdateButtons();
    }

    public void CloseCookbook()
    {
        if (panel == null)
            return;

        isOpen = false;
        panel.SetActive(false);
    }

    private void UpdateButtons()
    {
        backBtn.SetActive(currentPage > 0);
        nextBtn.SetActive(currentPage < pages.Length - 1);
    }

    public void NextPage()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            ShowPage(currentPage);
            UpdateButtons();
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            ShowPage(currentPage);
            UpdateButtons();
        }
    }

    public void ShowPage(int index)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == index);
        }
    }
}