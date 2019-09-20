using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawHands : MonoBehaviour
{
    protected const float CYLINDER_MESH_RESOLUTION = 0.1f; //in centimeters, meshes within this resolution will be re-used

    [SerializeField]
    protected bool _drawRight = true;
    [SerializeField]
    protected HandPoints _handRight = null;
    [SerializeField]
    protected Color _colorSpheresRight = new Color(0.0f, 0.0f, 1.0f);

    [SerializeField]
    protected bool _drawLeft = true;
    [SerializeField]
    protected HandPoints _handLeft = null;
    [SerializeField]
    protected Color _colorSpheresLeft = new Color(0.0f, 0.0f, 1.0f);

    [SerializeField]
    protected float _jointRadius = 0.06f;
    [SerializeField]
    protected int _cylinderResolution = 12;
    [SerializeField]
    protected float _cylinderRadius = 0.02f;

    [SerializeField]
    protected Mesh _sphereMesh;
    [SerializeField]
    protected Material _material;

    //Local
    protected Material _sphereMatRight;
    protected Material _sphereMatLeft;

    // Start is called before the first frame update
    void Start()
    {
        if (_drawRight) _sphereMatRight = new Material(_material) { color = _colorSpheresRight };
        if (_drawLeft) _sphereMatLeft = new Material(_material) { color = _colorSpheresLeft };
    }

    // Update is called once per frame
    void Update()
    {
        if (_drawRight)
        {
            #region Draw Points Right Hand
            drawSphere(_sphereMatRight, _handRight.Fist.transform.position);

            drawSphere(_sphereMatRight, _handRight.IndexFinger.transform.position);
            drawSphere(_sphereMatRight, _handRight.IndexFingerA.transform.position);
            drawSphere(_sphereMatRight, _handRight.IndexFingerB.transform.position);
            drawSphere(_sphereMatRight, _handRight.IndexFingerC.transform.position);

            drawSphere(_sphereMatRight, _handRight.MiddleFinger.transform.position);
            drawSphere(_sphereMatRight, _handRight.MiddleFingerA.transform.position);
            drawSphere(_sphereMatRight, _handRight.MiddleFingerB.transform.position);
            drawSphere(_sphereMatRight, _handRight.MiddleFingerC.transform.position);

            drawSphere(_sphereMatRight, _handRight.PinkyFinger.transform.position);
            drawSphere(_sphereMatRight, _handRight.PinkyFingerA.transform.position);
            drawSphere(_sphereMatRight, _handRight.PinkyFingerB.transform.position);
            drawSphere(_sphereMatRight, _handRight.PinkyFingerC.transform.position);

            drawSphere(_sphereMatRight, _handRight.RingFinger.transform.position);
            drawSphere(_sphereMatRight, _handRight.RingFingerA.transform.position);
            drawSphere(_sphereMatRight, _handRight.RingFingerB.transform.position);
            drawSphere(_sphereMatRight, _handRight.RingFingerC.transform.position);

            drawSphere(_sphereMatRight, _handRight.ThumbFinger.transform.position);
            drawSphere(_sphereMatRight, _handRight.ThumbFingerA.transform.position);
            drawSphere(_sphereMatRight, _handRight.ThumbFingerB.transform.position);
            #endregion

            #region Draw Lines Right Hand
            drawCylinder(_handRight.Fist.transform.position, _handRight.IndexFinger.transform.position);
            drawCylinder(_handRight.Fist.transform.position, _handRight.MiddleFinger.transform.position);
            drawCylinder(_handRight.Fist.transform.position, _handRight.PinkyFinger.transform.position);
            drawCylinder(_handRight.Fist.transform.position, _handRight.RingFinger.transform.position);
            drawCylinder(_handRight.Fist.transform.position, _handRight.ThumbFinger.transform.position);

            drawCylinder(_handRight.IndexFinger.transform.position, _handRight.IndexFingerA.transform.position);
            drawCylinder(_handRight.IndexFingerA.transform.position, _handRight.IndexFingerB.transform.position);
            drawCylinder(_handRight.IndexFingerB.transform.position, _handRight.IndexFingerC.transform.position);

            drawCylinder(_handRight.MiddleFinger.transform.position, _handRight.MiddleFingerA.transform.position);
            drawCylinder(_handRight.MiddleFingerA.transform.position, _handRight.MiddleFingerB.transform.position);
            drawCylinder(_handRight.MiddleFingerB.transform.position, _handRight.MiddleFingerC.transform.position);

            drawCylinder(_handRight.PinkyFinger.transform.position, _handRight.PinkyFingerA.transform.position);
            drawCylinder(_handRight.PinkyFingerA.transform.position, _handRight.PinkyFingerB.transform.position);
            drawCylinder(_handRight.PinkyFingerB.transform.position, _handRight.PinkyFingerC.transform.position);

            drawCylinder(_handRight.RingFinger.transform.position, _handRight.RingFingerA.transform.position);
            drawCylinder(_handRight.RingFingerA.transform.position, _handRight.RingFingerB.transform.position);
            drawCylinder(_handRight.RingFingerB.transform.position, _handRight.RingFingerC.transform.position);

            drawCylinder(_handRight.ThumbFinger.transform.position, _handRight.ThumbFingerA.transform.position);
            drawCylinder(_handRight.ThumbFingerA.transform.position, _handRight.ThumbFingerB.transform.position);
            #endregion
        }

        if (_drawLeft)
        {
            #region Draw Points Left Hand
            drawSphere(_sphereMatLeft, _handLeft.Fist.transform.position);

            drawSphere(_sphereMatLeft, _handLeft.IndexFinger.transform.position);
            drawSphere(_sphereMatLeft, _handLeft.IndexFingerA.transform.position);
            drawSphere(_sphereMatLeft, _handLeft.IndexFingerB.transform.position);
            drawSphere(_sphereMatLeft, _handLeft.IndexFingerC.transform.position);

            drawSphere(_sphereMatLeft, _handLeft.MiddleFinger.transform.position);
            drawSphere(_sphereMatLeft, _handLeft.MiddleFingerA.transform.position);
            drawSphere(_sphereMatLeft, _handLeft.MiddleFingerB.transform.position);
            drawSphere(_sphereMatLeft, _handLeft.MiddleFingerC.transform.position);

            drawSphere(_sphereMatLeft, _handLeft.PinkyFinger.transform.position);
            drawSphere(_sphereMatLeft, _handLeft.PinkyFingerA.transform.position);
            drawSphere(_sphereMatLeft, _handLeft.PinkyFingerB.transform.position);
            drawSphere(_sphereMatLeft, _handLeft.PinkyFingerC.transform.position);

            drawSphere(_sphereMatLeft, _handLeft.RingFinger.transform.position);
            drawSphere(_sphereMatLeft, _handLeft.RingFingerA.transform.position);
            drawSphere(_sphereMatLeft, _handLeft.RingFingerB.transform.position);
            drawSphere(_sphereMatLeft, _handLeft.RingFingerC.transform.position);

            drawSphere(_sphereMatLeft, _handLeft.ThumbFinger.transform.position);
            drawSphere(_sphereMatLeft, _handLeft.ThumbFingerA.transform.position);
            drawSphere(_sphereMatLeft, _handLeft.ThumbFingerB.transform.position);
            #endregion

            #region Draw Lines Left Hand
            drawCylinder(_handLeft.Fist.transform.position, _handLeft.IndexFinger.transform.position);
            drawCylinder(_handLeft.Fist.transform.position, _handLeft.MiddleFinger.transform.position);
            drawCylinder(_handLeft.Fist.transform.position, _handLeft.PinkyFinger.transform.position);
            drawCylinder(_handLeft.Fist.transform.position, _handLeft.RingFinger.transform.position);
            drawCylinder(_handLeft.Fist.transform.position, _handLeft.ThumbFinger.transform.position);

            drawCylinder(_handLeft.IndexFinger.transform.position, _handLeft.IndexFingerA.transform.position);
            drawCylinder(_handLeft.IndexFingerA.transform.position, _handLeft.IndexFingerB.transform.position);
            drawCylinder(_handLeft.IndexFingerB.transform.position, _handLeft.IndexFingerC.transform.position);

            drawCylinder(_handLeft.MiddleFinger.transform.position, _handLeft.MiddleFingerA.transform.position);
            drawCylinder(_handLeft.MiddleFingerA.transform.position, _handLeft.MiddleFingerB.transform.position);
            drawCylinder(_handLeft.MiddleFingerB.transform.position, _handLeft.MiddleFingerC.transform.position);

            drawCylinder(_handLeft.PinkyFinger.transform.position, _handLeft.PinkyFingerA.transform.position);
            drawCylinder(_handLeft.PinkyFingerA.transform.position, _handLeft.PinkyFingerB.transform.position);
            drawCylinder(_handLeft.PinkyFingerB.transform.position, _handLeft.PinkyFingerC.transform.position);

            drawCylinder(_handLeft.RingFinger.transform.position, _handLeft.RingFingerA.transform.position);
            drawCylinder(_handLeft.RingFingerA.transform.position, _handLeft.RingFingerB.transform.position);
            drawCylinder(_handLeft.RingFingerB.transform.position, _handLeft.RingFingerC.transform.position);

            drawCylinder(_handLeft.ThumbFinger.transform.position, _handLeft.ThumbFingerA.transform.position);
            drawCylinder(_handLeft.ThumbFingerA.transform.position, _handLeft.ThumbFingerB.transform.position);
            #endregion
        }
    }

    private void drawSphere(Material sphereMatRight, Vector3 position)
    {
        drawSphere(position, _jointRadius, sphereMatRight);
    }

    private void drawSphere(Vector3 position, float radius, Material sphereMat)
    {
        //multiply radius by 2 because the default unity sphere has a radius of 0.5 meters at scale 1.
        Graphics.DrawMesh(_sphereMesh,
                          Matrix4x4.TRS(position,
                                        Quaternion.identity,
                                        Vector3.one * radius * 2.0f * transform.lossyScale.x),
                          sphereMat, 0,
                          null, 0, null, true);
    }

    private void drawCylinder(Vector3 a, Vector3 b)
    {
        float length = (a - b).magnitude;
        Graphics.DrawMesh(getCylinderMesh(length),
                          Matrix4x4.TRS(a,
                                        Quaternion.LookRotation(b - a),
                                        new Vector3(transform.lossyScale.x, transform.lossyScale.x, 1)),
                          _material,
                          gameObject.layer,
                          null, 0, null, true);
    }

    private Dictionary<int, Mesh> _meshMap = new Dictionary<int, Mesh>();

    private Mesh getCylinderMesh(float length)
    {
        int lengthKey = Mathf.RoundToInt(length * 100 / CYLINDER_MESH_RESOLUTION);
        Mesh mesh;
        if (_meshMap.TryGetValue(lengthKey, out mesh))
        {
            return mesh;
        }
        mesh = new Mesh();
        mesh.name = "GeneratedCylinder";
        mesh.hideFlags = HideFlags.DontSave;
        List<Vector3> verts = new List<Vector3>();
        List<Color> colors = new List<Color>();
        List<int> tris = new List<int>();
        Vector3 p0 = Vector3.zero;
        Vector3 p1 = Vector3.forward * length;
        for (int i = 0; i < _cylinderResolution; i++)
        {
            float angle = (Mathf.PI * 2.0f * i) / _cylinderResolution;
            float dx = _cylinderRadius * Mathf.Cos(angle);
            float dy = _cylinderRadius * Mathf.Sin(angle);
            Vector3 spoke = new Vector3(dx, dy, 0);
            verts.Add(p0 + spoke);
            verts.Add(p1 + spoke);
            colors.Add(Color.white);
            colors.Add(Color.white);
            int triStart = verts.Count;
            int triCap = _cylinderResolution * 2;
            tris.Add((triStart + 0) % triCap);
            tris.Add((triStart + 2) % triCap);
            tris.Add((triStart + 1) % triCap);
            tris.Add((triStart + 2) % triCap);
            tris.Add((triStart + 3) % triCap);
            tris.Add((triStart + 1) % triCap);
        }
        mesh.SetVertices(verts);
        mesh.SetIndices(tris.ToArray(), MeshTopology.Triangles, 0);
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.UploadMeshData(true);
        _meshMap[lengthKey] = mesh;
        return mesh;
    }
}
