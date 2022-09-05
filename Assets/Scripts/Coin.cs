using UnityEngine;

public class Coin : Projectile
{
    private void OnCollisionEnter(Collision collision)
    {

        if (wasTriggered) return;

        if (collision.transform.tag == "Player")
        {
            wasTriggered = true;
            GameManager.Instance.AddCoin();
            GameManager.Instance.PlaySound(Sounds.CoinPickup);

            gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gates")
        {
            gameObject.SetActive(false);
        }
    }
}
