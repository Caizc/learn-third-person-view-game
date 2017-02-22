using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField]
    private Vector3 dPos;

    private bool _open;

    public void Operate()
    {
        if (_open)
        {
            Vector3 pos = this.transform.position - dPos;
            this.transform.position = pos;
        }
        else
        {
            Vector3 pos = this.transform.position + dPos;
            this.transform.position = pos;
        }

        _open = !_open;
    }

    public void Activate()
    {
        if (!_open)
        {
            Vector3 pos = this.transform.position + dPos;
            this.transform.position = pos;
            _open = true;
        }
    }

    public void Deactivate()
    {
        if (_open)
        {
            Vector3 pos = this.transform.position - dPos;
            this.transform.position = pos;
            _open = false;
        }
    }
}
