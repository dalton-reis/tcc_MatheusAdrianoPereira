using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereFloatDraw : MonoBehaviour
{
    public Mesh SphereMesh;
    public Material MaterialSphere;
    public Color ColorSphere;
    public Camera CameraScene;
    public bool Draw;
    public Vector3 Position;
    public float Radius;


    private Material _materialSphere;


    // Start is called before the first frame update
    void Start()
    {
        _materialSphere = new Material(MaterialSphere) { color = ColorSphere };
    }

    // Update is called once per frame
    void Update()
    {
        if (Draw) drawSphere();
    }

    private void drawSphere()
    {
        //multiply radius by 2 because the default unity sphere has a radius of 0.5 meters at scale 1.
        //Graphics.DrawMesh(SphereMesh,
        //                  Matrix4x4.TRS(Position,
        //                                Quaternion.identity,
        //                                Vector3.one * Radius * 2.0f * transform.lossyScale.x),
        //                  _materialSphere, 5,
        //                  null, 0, null, true);
    }
}
