using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]

public class MeshDeformation : MonoBehaviour {
	Mesh deformingMesh;
	MeshCollider colliderMesh;
    ColorVertexes cVtex;
    PlanetObjectsHandler poh;
	Vector3[] originalVerices;
	Vector3[] displacedVertices;
	Vector3[] vertexVelocities;
    Color[] updatingColors;
	float uniformScale = 1f;
    
	public float springForce = 20f;
	public float damping = 5f;
    public float maxHeight = 10;
    private bool built = false;
    private bool deforming = false;
	// Use this for initialization
	void Start () {
        
	}
    public void Build()
    {
        poh = GetComponent<PlanetObjectsHandler>();
        cVtex = GetComponent<ColorVertexes>();
        deformingMesh = GetComponent<MeshFilter>().mesh;
        updatingColors = deformingMesh.colors;
        originalVerices = deformingMesh.vertices;
        displacedVertices = new Vector3[originalVerices.Length];
        for (int i = 0; i < originalVerices.Length; i++)
        {
            displacedVertices[i] = originalVerices[i];
        }
        vertexVelocities = new Vector3[originalVerices.Length];
        colliderMesh = GetComponent<MeshCollider>();
        
        built = true;
        //InvokeRepeating("DoAnUpdate", 0f, 0.05f);
        
    }

    void DoAnUpdate()
    {
        if (built)
        {
            uniformScale = transform.localScale.x;
            bool doneFlag = false;
            for (int i = 0; i < vertexVelocities.Length; i++)
            {
                doneFlag &= UpdateVertex(i);
            }
            deformingMesh.colors = updatingColors;
            deformingMesh.vertices = displacedVertices;
            deformingMesh.RecalculateBounds();
            colliderMesh.sharedMesh = null;
            colliderMesh.sharedMesh = deformingMesh;
            deforming = !doneFlag;
        }
        
    }

    void Update()
    {
        if (deforming)
            DoAnUpdate();
    }

    bool UpdateVertex (int i){
		Vector3 velocity = vertexVelocities [i];
        if (displacedVertices[i] == originalVerices[i])
            return true;
		Vector3 disp = displacedVertices [i] - originalVerices [i];
		velocity -= disp * springForce * Time.deltaTime;
		velocity *= 1f - damping * Time.deltaTime;
		vertexVelocities [i] = velocity;
		displacedVertices [i] += velocity * Time.deltaTime;
        return false;
	}


	public void AddDeformingForce (Vector3 point, float force) {
		Debug.DrawLine(Camera.main.transform.position, point);
		point = transform.InverseTransformPoint (point);
		for (int i = 0; i < displacedVertices.Length; i++) {
			AddForceToVertex(i, point, force);
		}
        deforming = true;
	}

	void AddForceToVertex (int i, Vector3 point, float force) {
		Vector3 pointToVertex = transform.TransformPoint(displacedVertices[i]) - transform.TransformPoint(point);
		pointToVertex *= uniformScale;
		float attenuatiedForce = force / (1f + pointToVertex.sqrMagnitude);
		float velocity = attenuatiedForce * Time.deltaTime;
		vertexVelocities [i] += pointToVertex.normalized * velocity;
	}




    public void GenAddHeight(Vector3 point, Vector3 towards, float force, float radius)
    {
        for (int i = 0; i < displacedVertices.Length; i++)
        {
            AddHeightToVertex(i, point, towards, force, radius);
        }
        deforming = true;
    }

    public void AddHeight (Vector3 point, Vector3 towards,float force, float radius) {
		
		point = transform.InverseTransformPoint (point);
		for (int i = 0; i < displacedVertices.Length; i++) {
			AddHeightToVertex(i, point, towards, force, radius);
		}
        deforming = true;
	}
    void AddHeightToVertex(int i, Vector3 point, Vector3 towards, float force, float range) {
        Vector3 pointToVertex = displacedVertices[i] - point;
        updatingColors[i] = cVtex.VertexColor(originalVerices[i].magnitude);
        if (pointToVertex.sqrMagnitude < range) { 
            pointToVertex *= uniformScale;
            float attenuatiedForce = force / (2f + pointToVertex.sqrMagnitude);
            float velocity = attenuatiedForce * Time.deltaTime;
            float testDist = (originalVerices[i] + point.normalized * velocity).sqrMagnitude;
           
            originalVerices[i] += point.normalized * (velocity % maxHeight);

            
            //Update Object heights
            if (poh != null)
                poh.UpdateObjectHeight(i);
        }
	}
}
