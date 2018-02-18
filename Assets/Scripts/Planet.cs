using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    public float terrainRadius;
    public float waterRadius;
    public float atmosphereRadius;
    public short heat;
    public int detailLevel;
    //halo radius = terRad +10
    //waterRad = 2* terRad
    //atmosRad aka halo.size = terRad + atomosphere
	// Use this for initialization
	void Start () {
        //GeneratePlanet(); //shoudl do this with a scene transition on zoom in
	}
	
	// Update is called once per frame
	public void UpdatePlanet() {
		
	}
    public void GeneratePlanet() {
        ColorVertexes cv = GetComponent<ColorVertexes>();
        cv.DirtLevel = terrainRadius - 3;
        cv.GrassLevel = terrainRadius + 1;
        cv.RockLevel = terrainRadius + 4;
        cv.SnowLevel = terrainRadius + 7;


        CubeSphere cs = GetComponent<CubeSphere>();
        cs.radius = terrainRadius;
        cs.gridSize = detailLevel;
        cs.Build();

        MeshDeformation md = GetComponent<MeshDeformation>();
        md.Build();

        GetComponent<GenerateTerrain>().GenTerrain();
        //water
        transform.GetChild(0).localScale = new Vector3(terrainRadius * 2, terrainRadius * 2, terrainRadius * 2);
        transform.GetChild(0).gameObject.SetActive(true);
        //atmos
        transform.GetChild(1).localScale = new Vector3((terrainRadius + atmosphereRadius) * 2 , (terrainRadius + atmosphereRadius) * 2, (terrainRadius + atmosphereRadius) * 2);
        transform.GetChild(1).gameObject.SetActive(true);
        //clouds
        //ParticleSystem.ShapeModule shapeModule = transform.GetChild(2).GetComponent<ParticleSystem>().shape;
        //shapeModule.radius = atmosphereRadius + terrainRadius;
        //ParticleSystem.MainModule mainMondule = transform.GetChild(2).GetComponent<ParticleSystem>().main;
        //mainMondule.maxParticles = (int)atmosphereRadius * 2;

        //objects
        GetComponent<PlanetObjectsHandler>().Build();
    }
}
