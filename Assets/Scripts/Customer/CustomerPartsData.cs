using UnityEngine;

[CreateAssetMenu(fileName = "CustomerPartsData", menuName = "Scriptable Objects/CustomerPartsData")]
public class CustomerPartsData : ScriptableObject
{
    public Sprite[] hair;
    public Sprite[] body;
    public Sprite[] shirt;
    public Sprite[] pants;
    public Sprite[] overalls;
}
