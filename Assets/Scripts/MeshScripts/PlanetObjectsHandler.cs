using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetObjectsHandler : MonoBehaviour {
    Mesh placementMesh;
    
    
    Vector3[] vertices;
    GameObject[] objects;
    

    float uniformScale = 1f;
    
    
    // Use this for initialization
    public void Build()
    {
        placementMesh = GetComponent<MeshFilter>().mesh;
        vertices = placementMesh.vertices;
        objects = new GameObject[vertices.Length];
    }

    


    public void UpdateObjectHeight(int i)
    {
        if (placementMesh != null) { 
            vertices = placementMesh.vertices;
            if (objects[i] != null)
                objects[i].transform.position = vertices[i];// + transform.position;

        }
    }

    public void UpdateAllObjectHeights() {
        vertices = placementMesh.vertices;
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null)
                objects[i].transform.position = vertices[i];// + transform.position;
        }
    }




    /**
     * adds one obj to the planet closest to point returns the gameobject put there
     */
    public GameObject AddSingle(Vector3 point, Vector3 towards, float size, float radius,GameObject obj) {
        point = transform.InverseTransformPoint(point);
        GameObject built = null;
        for (int i = 0; i < vertices.Length && built == null; i++)
        {
            built = AddSingleObjectToVertex(i, point, towards, size, radius, obj);
        }
        return built;
          
    }
    GameObject AddSingleObjectToVertex(int i, Vector3 point, Vector3 towards, float size, float range, GameObject obj)
    {
        
        Vector3 pointToVertex = vertices[i] - point;
        //print("point @:" + point + " vert location:" + vertices[i] + " dist:" + pointToVertex.magnitude);
        if (pointToVertex.magnitude < range)
        {
            //instanciate 
            print("house being built");
            GameObject t = Instantiate(obj, vertices[i], Quaternion.identity, transform);
            t.transform.localPosition = vertices[i];//+transform.position;

            Vector3 bodyUp = t.transform.up;
            Vector3 gravityUp = (vertices[i]).normalized;


            Quaternion targetRot = Quaternion.FromToRotation(bodyUp, gravityUp) * t.transform.rotation;
            t.transform.rotation = targetRot;
            t.transform.rotation *= Quaternion.Euler(-90, 0, Random.Range(-180, 180));


            t.transform.localScale = new Vector3(size, size, size);
            if (objects[i] != null) Destroy(objects[i]);
            objects[i] = t;
            return t;
        }
        return null;
    }

    public void AddObject(Vector3 point, Vector3 towards, float size, float radius, GameObject obj)
    {
        //Debug.DrawLine(Camera.main.transform.position, point);
        point = transform.InverseTransformPoint(point);
        
        for (int i = 0; i < vertices.Length; i++)
        {
            AddObjectToVertex(i, point, towards, size, radius, obj);
        }
    }
    void AddObjectToVertex(int i, Vector3 point, Vector3 towards, float size, float range, GameObject obj)
    {
        Vector3 pointToVertex = vertices[i] - point;
        if (pointToVertex.sqrMagnitude < range)
        {
            //instanciate 
            
            GameObject t = Instantiate(obj, vertices[i], Quaternion.identity,transform);
            t.transform.localPosition = vertices[i];//+transform.position;
            
            Vector3 bodyUp = t.transform.up;
            Vector3 gravityUp = (vertices[i]).normalized;

            t.transform.up = gravityUp;
            //Quaternion targetRot = Quaternion.FromToRotation(bodyUp, gravityUp) * t.transform.rotation;
            
            //t.transform.rotation = targetRot;
            //t.transform.rotation *= Quaternion.Euler(-90,0,Random.Range(-180,180));


            t.transform.localScale = new Vector3(size,size,size);
            if (objects[i] != null) Destroy(objects[i]);
            objects[i] = t;         
        }
    }


    public void RemoveObject(Vector3 point, Vector3 towards, float size, float radius, GameObject obj)
    {
       // Debug.DrawLine(Camera.main.transform.position, point);
        point = transform.InverseTransformPoint(point);
        for (int i = 0; i < vertices.Length; i++)
        {
            RemoveObjectFromVertex(i, point, towards, size, radius, obj);
        }
    }
    void RemoveObjectFromVertex(int i, Vector3 point, Vector3 towards, float size, float range, GameObject obj)
    {
        Vector3 pointToVertex = vertices[i] - point;
        if (pointToVertex.sqrMagnitude < range)
        {
            
            if (objects[i] != null  && (objects[i].name == string.Format("{0}(Clone)", obj.name)))
            {
                Destroy(objects[i]);
                objects[i] = null;
            }
            
        }
    }
}