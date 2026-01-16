using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreathEffectController : MonoBehaviour
{
    [SerializeField] ParticleSystem breathEffect;

    public void PlayBreath()
    {
        breathEffect.Play();
    }

    public void StopBreath()
    {
        breathEffect.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }
}
