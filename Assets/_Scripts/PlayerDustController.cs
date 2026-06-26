using UnityEngine;

public class PlayerDustController : MonoBehaviour
{
    public ParticleSystem dustParticles; 
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.1f || Mathf.Abs(rb.velocity.z) > 0.1f)
        {
            if (!dustParticles.isEmitting)
            {
                dustParticles.Play();
            }
        }
        else
        {
            if (dustParticles.isEmitting)
            {
                dustParticles.Stop();
            }
        }
    }
}