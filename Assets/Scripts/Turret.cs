using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    
    
    public Transform turretMuzzle;
    [Space]
    public float minlaunchVelocity = 500f;
    public float maxLaunchVelocity = 200f;
    public float minWaitTime = 3;
    public float maxWaitTime = 10;

    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        while(!GameManager.Instance.gameOver)
        {
            var waitTime = Random.Range(minWaitTime, maxWaitTime);
            var launchVelocity = Random.Range(minlaunchVelocity, maxLaunchVelocity);

            yield return new WaitForSeconds(waitTime);

            if (GameManager.Instance.gameOver) yield break;

            PoolManager.Instance.SpawnRandomProjectileFromPool(turretMuzzle.position,turretMuzzle.rotation)
                .GetComponent<Rigidbody>().AddRelativeForce(turretMuzzle.forward * launchVelocity, 0);

            PoolManager.Instance.SpawnParticleFromPool("CannonFog", turretMuzzle.position);

            audioSource.Play();
        }

        
    }
}
