using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected bool wasTriggered;
    protected readonly object locker = new object();

    public void ResetTrigger()
    {
       wasTriggered = false;
    }
}
