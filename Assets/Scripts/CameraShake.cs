using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera cvCam;
    CinemachineBasicMultiChannelPerlin NoiseAmplitude;

    void Awake()
    {
        cvCam = GetComponent<CinemachineVirtualCamera>();
        NoiseAmplitude = cvCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        NoiseAmplitude.m_AmplitudeGain = magnitude;

        yield return new WaitForSeconds(duration);

        while(NoiseAmplitude.m_AmplitudeGain < magnitude / 10)
        {
            NoiseAmplitude.m_AmplitudeGain = Mathf.Lerp(magnitude, 0, Time.deltaTime / duration);

            yield return null;
        }
        NoiseAmplitude.m_AmplitudeGain = 0;
    }
}
