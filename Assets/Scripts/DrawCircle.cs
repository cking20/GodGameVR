using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCircle : MonoBehaviour {

    public float ThetaScale = 0.1f;
    public float radius = 20f;
    public float heightOffset = -10f;
    private int Size;
    
    private LineRenderer LineDrawer;
    private float Theta = 0f;


    void Start()
    {
        LineDrawer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        float scaleFactor = transform.parent.parent.localScale.x;
        Theta = 0f;
        Size = (int)((1f / ThetaScale) + 1f);
        LineDrawer.positionCount = (Size);
        for (int i = 0; i < Size; i++)
        {
            Theta += (2.0f * Mathf.PI * ThetaScale);
            float x = scaleFactor * radius * Mathf.Cos(Theta);
            float y = scaleFactor * radius * Mathf.Sin(Theta);
            LineDrawer.SetPosition(i, new Vector3(x, scaleFactor * heightOffset, y));
        }
    }
}
