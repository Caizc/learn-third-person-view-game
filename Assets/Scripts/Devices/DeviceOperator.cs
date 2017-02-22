using UnityEngine;

public class DeviceOperator : MonoBehaviour
{
    public float radius = 1.5f;

    void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);

            foreach (Collider hitCollider in hitColliders)
            {
                Vector3 direction = hitCollider.transform.position - this.transform.position;

                if (Vector3.Dot(this.transform.forward, direction) > 0.5f)
                {
                    hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
