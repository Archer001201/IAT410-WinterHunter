using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControler : MonoBehaviour
{
    public ParticleSystem vfx;

    private void Awake() {
        vfx.Stop();
    }
    private void OnTriggerEnter() {
        vfx.Start();
    }

    private void OnTriggerExit() {
        vfx.Stop();
    }
}
