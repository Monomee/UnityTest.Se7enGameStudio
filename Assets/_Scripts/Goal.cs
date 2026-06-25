using System.Collections;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] private GameObject confettiExplosion;
    private GameObject goalExplosion;
    private ParticleSystem particle;

    [SerializeField] private GameObject ballOb;
    private void Awake()
    {
        goalExplosion = Instantiate(confettiExplosion);
        goalExplosion.SetActive(false);
        particle = goalExplosion.GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;

        ballOb = other.gameObject;
        StartCoroutine(WaitCoroutine());
    }
    private IEnumerator WaitCoroutine()
    {
        goalExplosion.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        goalExplosion.SetActive(true);
        particle.Play();

        yield return new WaitForSeconds(0.5f);

        CameraController.instance.DetachBallFromCamera();

        TrailRenderer trail = ballOb.GetComponent<TrailRenderer>();
        trail.enabled = false;
        Rigidbody rb = ballOb.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true; 
        }

        ballOb.transform.position = BallController.instance.spawnPoint.position;

        if (rb != null) rb.isKinematic = false;
        if (!trail.enabled) trail.enabled = true;

        yield return new WaitForSeconds(2f);

        CameraController.instance.ChangeCameraBackToPlayer();
    }
}
