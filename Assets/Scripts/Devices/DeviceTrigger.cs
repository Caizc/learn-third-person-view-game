using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject[] targets;

    public bool requireKey;

    void OnTriggerEnter(Collider other)
    {
        if (requireKey && "key" != Managers.Inventory.equippedItem)
        {
            return;
        }

        foreach (GameObject target in targets)
        {
            target.SendMessage("Activate");
        }
    }

    void OnTriggerExit(Collider other)
    {
        foreach (GameObject target in targets)
        {
            target.SendMessage("Deactivate");
        }
    }
}
