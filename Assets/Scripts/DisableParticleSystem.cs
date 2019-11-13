using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableParticleSystem : MonoBehaviour
{
    void OnEnable()
    {
        ParticleSystem system = gameObject.GetComponent<ParticleSystem>();
        StartCoroutine(DisableAfterTime(system.main.duration + 0.2f));
    }
    
    IEnumerator DisableAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
