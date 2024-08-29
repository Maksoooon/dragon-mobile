using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticParticle : MonoBehaviour
{
    [SerializeField]
    private Transform _attractorTransform;

    private ParticleSystem _particleSystem;
    private ParticleSystem.Particle[] _particles;

    void Start()
    {
        _attractorTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;

        _particleSystem = GetComponent<ParticleSystem>();
        _particles = new ParticleSystem.Particle[_particleSystem.main.maxParticles];
    }

    void LateUpdate()
    {
        if (_particleSystem.isPlaying)
        {
            int length = _particleSystem.GetParticles(_particles);
            Vector3 attractorPosition = _attractorTransform.position;

            for (int i = 0; i < length; i++)
            {
                float remainingLifetime = _particles[i].remainingLifetime - 0.15f;
                
                if (remainingLifetime > 0)
                {
                    Vector3 directionToAttractor = (attractorPosition - _particles[i].position);
                    float distanceToAttractor = directionToAttractor.magnitude;
                    
                    // Calculate the required speed to reach the attractor at the end of the lifetime
                    Vector3 requiredVelocity = directionToAttractor / remainingLifetime;

                    // Smooth transition from current velocity to required velocity
                    _particles[i].velocity = Vector3.Lerp(_particles[i].velocity, requiredVelocity, Time.deltaTime);
                }
            }

            _particleSystem.SetParticles(_particles, length);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
