using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleActivator : MonoBehaviour
{
    [SerializeField]
    ParticleSystem particleSystem;

    private void Awake()
    {
        particleSystem.Play();
    }
}
