 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorVertexes : MonoBehaviour
{
    public Color WaterColour = new Color(25f, 59f, 141f);
    public float DirtLevel = 1f;
    public Color DirtColour = new Color(94f, 81f, 8f);
    public float GrassLevel = 10f;
    public Color GrassColour = new Color(68f, 174f, 117f);
    public float RockLevel = 20f;
    public Color RockColour = new Color(165f, 165f, 165f);
    public float SnowLevel = 30f;
    public Color SnowColor = new Color(255f, 255f, 255f);
    Mesh mesh; 
    Color[] colors;
 
    //should run when needed or at most once a second
    public void UpdateColors()
    {
        mesh = this.GetComponent<MeshFilter>().mesh;
        colors = new Color[mesh.colors.Length];
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            colors[i] = VertexColor(Vector3.Distance(transform.position, mesh.vertices[i]));
        }
        mesh.colors = colors;
        //mesh.RecalculateNormals();
        this.GetComponent<MeshFilter>().sharedMesh = null;
        this.GetComponent<MeshFilter>().sharedMesh = mesh;

    }

    public Color VertexColor(float distance)
    {
        if (distance < DirtLevel)
            return WaterColour;
        if (distance < GrassLevel)
            return DirtColour;
        if (distance < RockLevel)
            return GrassColour;
        if (distance < SnowLevel)
            return RockColour;
        return SnowColor;


    }
}

