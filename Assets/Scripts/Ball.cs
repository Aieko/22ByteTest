using System.Collections;
using UnityEngine;

public class Ball : Projectile
{

    [SerializeField] private float _secondsBeforeDeactivation = 5;

    private void OnCollisionEnter(Collision collision)
    {
        if (wasTriggered) return;

        var tag = collision.transform.tag;

        if (tag == "Player")
        {
            wasTriggered = true;
            GameManager.Instance.AddToProgress();
            GameManager.Instance.PlaySound(Sounds.BallWasCatched);

            StartCoroutine(SetUnactive());
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gates")
        {
            GameManager.Instance.AddToProgress(-10);
            GameManager.Instance.PlaySound(Sounds.BallWasMissed);

            gameObject.SetActive(false);
        }
    }

    private IEnumerator SetUnactive()
    {
        yield return new WaitForSeconds(_secondsBeforeDeactivation);

        gameObject.SetActive(false);
    }

}
