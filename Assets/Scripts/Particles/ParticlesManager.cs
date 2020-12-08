using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesManager : MonoBehaviour
{
    // Particles manager is used to play the effect and destroy when it is finished (selected time)
    // This way, we can use more particles effects

    private ParticleSystem particles;

    public float time;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
        particles.Play();

        Invoke(nameof(DestroyGO), time);
    }

    private void DestroyGO()
    {
        Destroy(this.gameObject);
    }

}
