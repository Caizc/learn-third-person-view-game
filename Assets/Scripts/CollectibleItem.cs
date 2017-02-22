using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Item collected: " + itemName);

        Managers.Inventory.AddItem(itemName);

        Destroy(this.gameObject);
    }
}
