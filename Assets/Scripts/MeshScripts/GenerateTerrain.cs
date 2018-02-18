using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshDeformation))]
public class GenerateTerrain : MonoBehaviour{

    public void GenTerrain() {
        RandomMesh();

    }

    private void RandomMesh() {
        float minRand = -200f;
        float maxRand = 200f;
        Mesh m = GetComponent<MeshFilter>().sharedMesh;
        MeshDeformation md = GetComponent<MeshDeformation>();
        ColorVertexes cv = GetComponent<ColorVertexes>();
        //raise and lower at random
        print("GenTerr vernum = " + m.vertices.Length);
        Vector3[] v = m.vertices;
        for (int i = 0; i < v.Length; i++) {
            float force = Random.Range(minRand, maxRand);
            md.GenAddHeight(v[i], v[i] + v[i], force, 1000);           
        }
        cv.UpdateColors();
    }



}
