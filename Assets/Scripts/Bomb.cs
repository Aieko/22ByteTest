using UnityEngine;

public class Bomb : Projectile
{
    private void OnCollisionEnter(Collision collision)
    {
        if (wasTriggered) return;

        if (collision.transform.tag == "Player")
        {
            wasTriggered = true;
            GameManager.Instance.AddToProgress(-20);
            GameManager.Instance.PlaySound(Sounds.BombWasCatched);

            PoolManager.Instance.SpawnParticleFromPool("BombExplosion", transform.position);

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
