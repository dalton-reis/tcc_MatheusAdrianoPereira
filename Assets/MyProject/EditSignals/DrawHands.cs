using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawHands : MonoBehaviour
{
    protected const float CYLINDER_MESH_RESOLUTION = 0.1f; //in centimeters, meshes within this resolution will be re-used

    [SerializeField]
    protected bool _drawRight = true;
    public bool DrawRight { get { return _drawRight; } set { _drawRight = value; } }
    [SerializeField]
    protected HandPoints _handRight = null;
    [SerializeField]
    protected Color _colorSpheresRight = new Color(0.0f, 0.0f, 1.0f);

    [SerializeField]
    protected bool _drawLeft = true;
    public bool DrawLeft { get { return _drawLeft; } set { _drawLeft = value; } }
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
    public Mesh _sphereMesh;
    [SerializeField]
    public Material _material;

    //Local
    public Material _sphereMatRight;

    internal bool DrawHand(string h)
    {
        if (h == "R") return _drawRight && _handRight.gameObject.activeInHierarchy;
        else return _drawLeft && _handLeft.gameObject.activeInHierarchy;
    }

    protected Material _sphereMatLeft;

    // Start is called before the first frame update
    void Start()
    {
        _sphereMatRight = new Material(_material) { color = _colorSpheresRight };
        _sphereMatLeft = new Material(_material) { color = _colorSpheresLeft };
    }

    // Update is called once per frame
    void Update()
    {
        if (_drawRight && _handRight.gameObject.activeInHierarchy)
        {
            #region Draw Points Right Hand
            drawSphere(_sphereMatRight, _handRight.Fist, "R_FIST");

            drawSphere(_sphereMatRight, _handRight.IndexFinger, "R_INDEX");
            drawSphere(_sphereMatRight, _handRight.IndexFingerA, "R_INDEX_A");
            drawSphere(_sphereMatRight, _handRight.IndexFingerB, "R_INDEX_B");
            drawSphere(_sphereMatRight, _handRight.IndexFingerC, "R_INDEX_C");

            drawSphere(_sphereMatRight, _handRight.MiddleFinger, "R_MIDDLE");
            drawSphere(_sphereMatRight, _handRight.MiddleFingerA, "R_MIDDLE_A");
            drawSphere(_sphereMatRight, _handRight.MiddleFingerB, "R_MIDDLE_B");
            drawSphere(_sphereMatRight, _handRight.MiddleFingerC, "R_MIDDLE_C");

            drawSphere(_sphereMatRight, _handRight.PinkyFinger, "R_PINKY");
            drawSphere(_sphereMatRight, _handRight.PinkyFingerA, "R_PINKY_A");
            drawSphere(_sphereMatRight, _handRight.PinkyFingerB, "R_PINKY_B");
            drawSphere(_sphereMatRight, _handRight.PinkyFingerC, "R_PINKY_C");

            drawSphere(_sphereMatRight, _handRight.RingFinger, "R_RING");
            drawSphere(_sphereMatRight, _handRight.RingFingerA, "R_RING_A");
            drawSphere(_sphereMatRight, _handRight.RingFingerB, "R_RING_B");
            drawSphere(_sphereMatRight, _handRight.RingFingerC, "R_RING_C");

            drawSphere(_sphereMatRight, _handRight.ThumbFinger, "R_THUMB");
            drawSphere(_sphereMatRight, _handRight.ThumbFingerA, "R_THUMB_A");
            drawSphere(_sphereMatRight, _handRight.ThumbFingerB, "R_THUMB_B");
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

        if (_drawLeft && _handLeft.gameObject.activeInHierarchy)
        {
            #region Draw Points Left Hand
            drawSphere(_sphereMatLeft, _handLeft.Fist, "L_FIST");

            drawSphere(_sphereMatLeft, _handLeft.IndexFinger, "L_INDEX");
            drawSphere(_sphereMatLeft, _handLeft.IndexFingerA, "L_INDEX_A");
            drawSphere(_sphereMatLeft, _handLeft.IndexFingerB, "L_INDEX_B");
            drawSphere(_sphereMatLeft, _handLeft.IndexFingerC, "L_INDEX_C");

            drawSphere(_sphereMatLeft, _handLeft.MiddleFinger, "L_MIDDLE");
            drawSphere(_sphereMatLeft, _handLeft.MiddleFingerA, "L_MIDDLE_A");
            drawSphere(_sphereMatLeft, _handLeft.MiddleFingerB, "L_MIDDLE_B");
            drawSphere(_sphereMatLeft, _handLeft.MiddleFingerC, "L_MIDDLE_C");

            drawSphere(_sphereMatLeft, _handLeft.PinkyFinger, "L_PINKY");
            drawSphere(_sphereMatLeft, _handLeft.PinkyFingerA, "L_PINKY_A");
            drawSphere(_sphereMatLeft, _handLeft.PinkyFingerB, "L_PINKY_B");
            drawSphere(_sphereMatLeft, _handLeft.PinkyFingerC, "L_PINKY_C");

            drawSphere(_sphereMatLeft, _handLeft.RingFinger, "L_RING");
            drawSphere(_sphereMatLeft, _handLeft.RingFingerA, "L_RING_A");
            drawSphere(_sphereMatLeft, _handLeft.RingFingerB, "L_RING_B");
            drawSphere(_sphereMatLeft, _handLeft.RingFingerC, "L_RING_C");

            drawSphere(_sphereMatLeft, _handLeft.ThumbFinger, "L_THUMB");
            drawSphere(_sphereMatLeft, _handLeft.ThumbFingerA, "L_THUMB_A");
            drawSphere(_sphereMatLeft, _handLeft.ThumbFingerB, "L_THUMB_B");
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


    private void drawSphere(Material sphereMatRight, GameObject gameObject, string position)
    {
        ControlMarkers.DrawEffects(gameObject, position);

        drawSphere(sphereMatRight, gameObject.transform.position);
    }

    private void drawSphere(Material sphereMatRight, Vector3 position)
    {
        drawSphere(InverterX(position), _jointRadius, sphereMatRight);
    }

    private Vector3 InverterX(Vector3 position)
    {
        return position;
        //return new Vector3(-position.x, position.y, position.z);
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
        drawCylinder2(InverterX(a), InverterX(b));
    }

    private void drawCylinder2(Vector3 a, Vector3 b)
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

    private Matrix4x4 ReferenceLocalToWorldMatrixL = Matrix4x4.identity;
    private Matrix4x4 ReferenceLocalToWorldMatrixR = Matrix4x4.identity;
    public Matrix4x4 GetLocalToWorldMatrixReference(string side)
    {
        if (side == "R")
        {
            if (ReferenceLocalToWorldMatrixR == Matrix4x4.identity)
                return _handRight.Palm.transform.localToWorldMatrix * Matrix4x4.identity;

            return ReferenceLocalToWorldMatrixR;
        }
        else
        {
            if (ReferenceLocalToWorldMatrixL == Matrix4x4.identity)
                return _handLeft.Palm.transform.localToWorldMatrix * Matrix4x4.identity;

            return ReferenceLocalToWorldMatrixL;
        }
    }

    private Matrix4x4 ReferenceWorldToLocalMatrixL = Matrix4x4.identity;
    private Matrix4x4 ReferenceWorldToLocalMatrixR = Matrix4x4.identity;
    public Matrix4x4 GetWorldToLocalMatrixReference(string side)
    {
        if (side == "R")
        {
            if (ReferenceWorldToLocalMatrixR == Matrix4x4.identity)
                return _handRight.Palm.transform.worldToLocalMatrix * Matrix4x4.identity;

            return ReferenceWorldToLocalMatrixR;
        }
        else
        {
            if (ReferenceWorldToLocalMatrixL == Matrix4x4.identity)
                return _handLeft.Palm.transform.worldToLocalMatrix * Matrix4x4.identity;

            return ReferenceWorldToLocalMatrixL;
        }
    }

    public void SaveMtrixReference()
    {
        if (ReferenceLocalToWorldMatrixR == Matrix4x4.identity)
            ReferenceLocalToWorldMatrixR = _handRight.Palm.transform.localToWorldMatrix * Matrix4x4.identity;

        if (ReferenceLocalToWorldMatrixL == Matrix4x4.identity)
            ReferenceLocalToWorldMatrixL = _handLeft.Palm.transform.localToWorldMatrix * Matrix4x4.identity;

        if (ReferenceWorldToLocalMatrixR == Matrix4x4.identity)
            ReferenceWorldToLocalMatrixR = _handRight.Palm.transform.worldToLocalMatrix * Matrix4x4.identity;

        if (ReferenceWorldToLocalMatrixL == Matrix4x4.identity)
            ReferenceWorldToLocalMatrixL = _handLeft.Palm.transform.worldToLocalMatrix * Matrix4x4.identity;
    }

    public void ClearReferencePoint()
    {
        ReferenceLocalToWorldMatrixL = Matrix4x4.identity;
        ReferenceLocalToWorldMatrixR = Matrix4x4.identity;
        ReferenceWorldToLocalMatrixL = Matrix4x4.identity;
        ReferenceWorldToLocalMatrixR = Matrix4x4.identity;
    }

    public GameObject GetPoint(string position)
    {
        switch (position)
        {
            case "R_FIST":
                return _handRight.Fist;
            case "R_PALM":
                return _handRight.Palm;
            case "R_INDEX":
                return _handRight.IndexFinger;
            case "R_INDEX_A":
                return _handRight.IndexFingerA;
            case "R_INDEX_B":
                return _handRight.IndexFingerB;
            case "R_INDEX_C":
                return _handRight.IndexFingerC;
            case "R_MIDDLE":
                return _handRight.MiddleFinger;
            case "R_MIDDLE_A":
                return _handRight.MiddleFingerA;
            case "R_MIDDLE_B":
                return _handRight.MiddleFingerB;
            case "R_MIDDLE_C":
                return _handRight.MiddleFingerC;
            case "R_PINKY":
                return _handRight.PinkyFinger;
            case "R_PINKY_A":
                return _handRight.PinkyFingerA;
            case "R_PINKY_B":
                return _handRight.PinkyFingerB;
            case "R_PINKY_C":
                return _handRight.PinkyFingerC;
            case "R_RING":
                return _handRight.RingFinger;
            case "R_RING_A":
                return _handRight.RingFingerA;
            case "R_RING_B":
                return _handRight.RingFingerB;
            case "R_RING_C":
                return _handRight.RingFingerC;
            case "R_THUMB":
                return _handRight.ThumbFinger;
            case "R_THUMB_A":
                return _handRight.ThumbFingerA;
            case "R_THUMB_B":
                return _handRight.ThumbFingerB;
            case "L_FIST":
                return _handLeft.Fist;
            case "L_PALM":
                return _handLeft.Palm;
            case "L_INDEX":
                return _handLeft.IndexFinger;
            case "L_INDEX_A":
                return _handLeft.IndexFingerA;
            case "L_INDEX_B":
                return _handLeft.IndexFingerB;
            case "L_INDEX_C":
                return _handLeft.IndexFingerC;
            case "L_MIDDLE":
                return _handLeft.MiddleFinger;
            case "L_MIDDLE_A":
                return _handLeft.MiddleFingerA;
            case "L_MIDDLE_B":
                return _handLeft.MiddleFingerB;
            case "L_MIDDLE_C":
                return _handLeft.MiddleFingerC;
            case "L_PINKY":
                return _handLeft.PinkyFinger;
            case "L_PINKY_A":
                return _handLeft.PinkyFingerA;
            case "L_PINKY_B":
                return _handLeft.PinkyFingerB;
            case "L_PINKY_C":
                return _handLeft.PinkyFingerC;
            case "L_RING":
                return _handLeft.RingFinger;
            case "L_RING_A":
                return _handLeft.RingFingerA;
            case "L_RING_B":
                return _handLeft.RingFingerB;
            case "L_RING_C":
                return _handLeft.RingFingerC;
            case "L_THUMB":
                return _handLeft.ThumbFinger;
            case "L_THUMB_A":
                return _handLeft.ThumbFingerA;
            case "L_THUMB_B":
                return _handLeft.ThumbFingerB;

            default:
                return null;
        }
    }
}