using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOrbitLine : MonoBehaviour {

    public float ThetaScale = 0.1f;
    private float radius = 20f;
    public float floor = -10f;
    private int Size;

    private LineRenderer LineDrawer;
    private float Theta = 0f;


    void Start()
    {
        LineDrawer = GetComponent<LineRenderer>();
    }

    void Update()
    {
        radius = transform.position.magnitude;

        Theta = 0f;
        Size = (int)((1f / ThetaScale) + 1f);
        LineDrawer.positionCount = (Size);
        for (int i = 0; i < Size; i++)
        {
            Theta += (2.0f * Mathf.PI * ThetaScale);
            float x = radius * Mathf.Cos(Theta);
            float y = radius * Mathf.Sin(Theta);
            LineDrawer.SetPosition(i, new Vector3(x, floor, y));
        }
    }
}
