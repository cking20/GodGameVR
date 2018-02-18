using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleGrav : MonoBehaviour {
    public Transform center;
    public ParticleSystem partSys;
    private ParticleSystem.Particle[] particles;

	// Use this for initialization
	void Start () {
        partSys.GetParticles(particles);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
