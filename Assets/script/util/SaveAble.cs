using UnityEngine;

public class SaveAble : MonoBehaviour
{
    [SerializeField]
    private string itemName = null; // Unique name for the item

    public string ItemName => itemName;

    // Generate a unique name for the item
    public void GenerateUniqueName()
    {
        // Use a fixed prefix followed by a randomly generated suffix
        string prefix = gameObject.name + "_";
        string suffix = Random.Range(1000, 10000).ToString(); // Generate a random number as suffix
        itemName = prefix + suffix;
    }

    // Reset method is called when the script is added to a GameObject or reset from the Inspector window
    void Reset()
    {
        GenerateUniqueName();
    }
}
