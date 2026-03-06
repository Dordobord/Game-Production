using UnityEngine;
using UnityEngine.UI;
public class UIPageNavigation : MonoBehaviour
{
     [SerializeField]private GameObject[] pages;
     [SerializeField]private Button nextBtn;
     [SerializeField]private Button prevBtn;

     private int currentPage = 0;

    void Start()
    {
        ShowPage(0);
    }

    public void NextPage()
    {
        if (currentPage >= pages.Length - 1) return;

        currentPage++;
        ShowPage(currentPage);
    }

    public void PreviousPage()
    {
        if (currentPage <= 0 ) return;

        currentPage--;
        ShowPage(currentPage);
    }
    public void ShowPage(int index)
    {
        currentPage = index;
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == index);
        }

        UpdateButtons();
    }

    private void UpdateButtons()
    {
        if (prevBtn != null)
        {
            prevBtn.gameObject.SetActive(currentPage > 0);
        }

        if (nextBtn != null)
        {
            nextBtn.gameObject.SetActive(currentPage < pages.Length - 1);
        }
    }
}
