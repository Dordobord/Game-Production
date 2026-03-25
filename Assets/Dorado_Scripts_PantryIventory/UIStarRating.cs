using UnityEngine;
using UnityEngine.UI;

public class UIStarRating : MonoBehaviour
{
    [SerializeField]private Image[] starImages;
    
    public void SetStarRating(int rating)
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            starImages[i].gameObject.SetActive(i < rating);
        }
    }
}
