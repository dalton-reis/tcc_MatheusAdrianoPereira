using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DrawHand : MonoBehaviour
{
    private const float CYLINDER_MESH_RESOLUTION = 0.1f; //in centimeters, meshes within this resolution will be re-used

    [SerializeField]
    private Color _colorSpheres = new Color(0.0f, 0.0f, 1.0f);

    [SerializeField]
    private float _jointRadius = 0.06f;

    [SerializeField]
    private int _cylinderResolution = 12;

    [SerializeField]
    private float _cylinderRadius = 0.02f;

    [SerializeField]
    private Mesh _sphereMesh;

    [SerializeField]
    private Material _material;
    private Material _sphereMatDefault;
    private Material _sphereMatRed;

    #region Property Hand
    [SerializeField]
    private Transform _fist;
    [SerializeField]
    private Transform _indexFinger;
    [SerializeField]
    private Transform _indexFingerA;
    [SerializeField]
    private Transform _indexFingerB;
    [SerializeField]
    private Transform _indexFingerC;
    [SerializeField]
    private Transform _middleFinger;
    [SerializeField]
    private Transform _middleFingerA;
    [SerializeField]
    private Transform _middleFingerB;
    [SerializeField]
    private Transform _middleFingerC;
    [SerializeField]
    private Transform _pinkyFinger;
    [SerializeField]
    private Transform _pinkyFingerA;
    [SerializeField]
    private Transform _pinkyFingerB;
    [SerializeField]
    private Transform _pinkyFingerC;
    [SerializeField]
    private Transform _ringFinger;
    [SerializeField]
    private Transform _ringFingerA;
    [SerializeField]
    private Transform _ringFingerB;
    [SerializeField]
    private Transform _ringFingerC;
    [SerializeField]
    private Transform _thumbFinger;
    [SerializeField]
    private Transform _thumbFingerA;
    [SerializeField]
    private Transform _thumbFingerB;
    #endregion


    #region Property Compare/ReflectHand
    [SerializeField]
    private bool _compareOtherHand;
    [SerializeField]
    private bool _reflectInOtherHand;

    #region Property Other Hand
    [SerializeField]
    private Transform _fistOtherHand;
    [SerializeField]
    private Transform _indexFingerOtherHand;
    [SerializeField]
    private Transform _indexFingerAOtherHand;
    [SerializeField]
    private Transform _indexFingerBOtherHand;
    [SerializeField]
    private Transform _indexFingerCOtherHand;
    [SerializeField]
    private Transform _middleFingerOtherHand;
    [SerializeField]
    private Transform _middleFingerAOtherHand;
    [SerializeField]
    private Transform _middleFingerBOtherHand;
    [SerializeField]
    private Transform _middleFingerCOtherHand;
    [SerializeField]
    private Transform _pinkyFingerOtherHand;
    [SerializeField]
    private Transform _pinkyFingerAOtherHand;
    [SerializeField]
    private Transform _pinkyFingerBOtherHand;
    [SerializeField]
    private Transform _pinkyFingerCOtherHand;
    [SerializeField]
    private Transform _ringFingerOtherHand;
    [SerializeField]
    private Transform _ringFingerAOtherHand;
    [SerializeField]
    private Transform _ringFingerBOtherHand;
    [SerializeField]
    private Transform _ringFingerCOtherHand;
    [SerializeField]
    private Transform _thumbFingerOtherHand;
    [SerializeField]
    private Transform _thumbFingerAOtherHand;
    [SerializeField]
    private Transform _thumbFingerBOtherHand;
    #endregion

    #region Property Acceptable Distance
    [SerializeField]
    private float _defaultAcceptableDistance = 0.01f;
    [SerializeField]
    private float _fistAcceptableDistance;
    [SerializeField]
    private float _indexFingerAcceptableDistance;
    [SerializeField]
    private float _indexFingerAAcceptableDistance;
    [SerializeField]
    private float _indexFingerBAcceptableDistance;
    [SerializeField]
    private float _indexFingerCAcceptableDistance;
    [SerializeField]
    private float _middleFingerAcceptableDistance;
    [SerializeField]
    private float _middleFingerAAcceptableDistance;
    [SerializeField]
    private float _middleFingerBAcceptableDistance;
    [SerializeField]
    private float _middleFingerCAcceptableDistance;
    [SerializeField]
    private float _pinkyFingercceptableDistance;
    [SerializeField]
    private float _pinkyFingerAAcceptableDistance;
    [SerializeField]
    private float _pinkyFingerBAcceptableDistance;
    [SerializeField]
    private float _pinkyFingerCAcceptableDistance;
    [SerializeField]
    private float _ringFingerAcceptableDistance;
    [SerializeField]
    private float _ringFingerAAcceptableDistance;
    [SerializeField]
    private float _ringFingerBAcceptableDistance;
    [SerializeField]
    private float _ringFingerCAcceptableDistance;
    [SerializeField]
    private float _thumbFingerAcceptableDistance;
    [SerializeField]
    private float _thumbFingerAAcceptableDistance;
    [SerializeField]
    private float _thumbFingerBAcceptableDistance;
    #endregion
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _sphereMatDefault = new Material(_material);
        _sphereMatDefault.color = _colorSpheres;
        _sphereMatRed = new Material(_material);
        _sphereMatRed.color = new Color(1.0f, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (_reflectInOtherHand) // if contains parameter point reflect draw to this point
        {
            #region Create Points ReflectedHand
            Vector3 firstPositionReflected = _fistOtherHand.position;

            Vector3 indexFingerPositionReflected = firstPositionReflected + _indexFinger.position - _fist.position;
            Vector3 indexFingerAPositionReflected = firstPositionReflected + _indexFingerA.position - _fist.position;
            Vector3 indexFingerBPositionReflected = firstPositionReflected + _indexFingerB.position - _fist.position;
            Vector3 indexFingerCPositionReflected = firstPositionReflected + _indexFingerC.position - _fist.position;

            Vector3 middleFingerPositionReflected = firstPositionReflected + _middleFinger.position - _fist.position;
            Vector3 middleFingerAPositionReflected = firstPositionReflected + _middleFingerA.position - _fist.position;
            Vector3 middleFingerBPositionReflected = firstPositionReflected + _middleFingerB.position - _fist.position;
            Vector3 middleFingerCPositionReflected = firstPositionReflected + _middleFingerC.position - _fist.position;

            Vector3 pinkyFingerPositionReflected = firstPositionReflected + _pinkyFinger.position - _fist.position;
            Vector3 pinkyFingerAPositionReflected = firstPositionReflected + _pinkyFingerA.position - _fist.position;
            Vector3 pinkyFingerBPositionReflected = firstPositionReflected + _pinkyFingerB.position - _fist.position;
            Vector3 pinkyFingerCPositionReflected = firstPositionReflected + _pinkyFingerC.position - _fist.position;

            Vector3 ringFingerPositionReflected = firstPositionReflected + _ringFinger.position - _fist.position;
            Vector3 ringFingerAPositionReflected = firstPositionReflected + _ringFingerA.position - _fist.position;
            Vector3 ringFingerBPositionReflected = firstPositionReflected + _ringFingerB.position - _fist.position;
            Vector3 ringFingerCPositionReflected = firstPositionReflected + _ringFingerC.position - _fist.position;

            Vector3 thumbFingerPositionReflected = firstPositionReflected + _thumbFinger.position - _fist.position;
            Vector3 thumbFingerAPositionReflected = firstPositionReflected + _thumbFingerA.position - _fist.position;
            Vector3 thumbFingerBPositionReflected = firstPositionReflected + _thumbFingerB.position - _fist.position;
            #endregion

            #region Draw Points ReflectedHand
            drawSphere(firstPositionReflected, _fistOtherHand, _fistAcceptableDistance);

            drawSphere(indexFingerPositionReflected, _indexFingerOtherHand, _indexFingerAcceptableDistance);
            drawSphere(indexFingerAPositionReflected, _indexFingerAOtherHand, _indexFingerAAcceptableDistance);
            drawSphere(indexFingerBPositionReflected, _indexFingerBOtherHand, _indexFingerBAcceptableDistance);
            drawSphere(indexFingerCPositionReflected, _indexFingerCOtherHand, _indexFingerCAcceptableDistance);

            drawSphere(middleFingerPositionReflected, _middleFingerOtherHand, _middleFingerAcceptableDistance);
            drawSphere(middleFingerAPositionReflected, _middleFingerAOtherHand, _middleFingerAAcceptableDistance);
            drawSphere(middleFingerBPositionReflected, _middleFingerBOtherHand, _middleFingerBAcceptableDistance);
            drawSphere(middleFingerCPositionReflected, _middleFingerCOtherHand, _middleFingerCAcceptableDistance);

            drawSphere(pinkyFingerPositionReflected, _pinkyFingerOtherHand, _pinkyFingercceptableDistance);
            drawSphere(pinkyFingerAPositionReflected, _pinkyFingerAOtherHand, _pinkyFingerAAcceptableDistance);
            drawSphere(pinkyFingerBPositionReflected, _pinkyFingerBOtherHand, _pinkyFingerBAcceptableDistance);
            drawSphere(pinkyFingerCPositionReflected, _pinkyFingerCOtherHand, _pinkyFingerCAcceptableDistance);

            drawSphere(ringFingerPositionReflected, _ringFingerOtherHand, _ringFingerAcceptableDistance);
            drawSphere(ringFingerAPositionReflected, _ringFingerAOtherHand, _ringFingerAAcceptableDistance);
            drawSphere(ringFingerBPositionReflected, _ringFingerBOtherHand, _ringFingerBAcceptableDistance);
            drawSphere(ringFingerCPositionReflected, _ringFingerCOtherHand, _ringFingerCAcceptableDistance);

            drawSphere(thumbFingerPositionReflected, _thumbFingerOtherHand, _thumbFingerAcceptableDistance);
            drawSphere(thumbFingerAPositionReflected, _thumbFingerAOtherHand, _thumbFingerAAcceptableDistance);
            drawSphere(thumbFingerBPositionReflected, _thumbFingerBOtherHand, _thumbFingerBAcceptableDistance);
            #endregion

            #region Draw Lines ReflectedHand
            drawCylinder(firstPositionReflected, indexFingerPositionReflected);
            drawCylinder(firstPositionReflected, middleFingerPositionReflected);
            drawCylinder(firstPositionReflected, pinkyFingerPositionReflected);
            drawCylinder(firstPositionReflected, ringFingerPositionReflected);
            drawCylinder(firstPositionReflected, thumbFingerPositionReflected);

            drawCylinder(indexFingerPositionReflected, indexFingerAPositionReflected);
            drawCylinder(indexFingerAPositionReflected, indexFingerBPositionReflected);
            drawCylinder(indexFingerBPositionReflected, indexFingerCPositionReflected);

            drawCylinder(middleFingerPositionReflected, middleFingerAPositionReflected);
            drawCylinder(middleFingerAPositionReflected, middleFingerBPositionReflected);
            drawCylinder(middleFingerBPositionReflected, middleFingerCPositionReflected);

            drawCylinder(pinkyFingerPositionReflected, pinkyFingerAPositionReflected);
            drawCylinder(pinkyFingerAPositionReflected, pinkyFingerBPositionReflected);
            drawCylinder(pinkyFingerBPositionReflected, pinkyFingerCPositionReflected);

            drawCylinder(ringFingerPositionReflected, ringFingerAPositionReflected);
            drawCylinder(ringFingerAPositionReflected, ringFingerBPositionReflected);
            drawCylinder(ringFingerBPositionReflected, ringFingerCPositionReflected);

            drawCylinder(thumbFingerPositionReflected, thumbFingerAPositionReflected);
            drawCylinder(thumbFingerAPositionReflected, thumbFingerBPositionReflected);
            #endregion

        }

        #region Draw Points
        drawSphere(_fist.position);

        drawSphere(_indexFinger.position);
        drawSphere(_indexFingerA.position);
        drawSphere(_indexFingerB.position);
        drawSphere(_indexFingerC.position);

        drawSphere(_middleFinger.position);
        drawSphere(_middleFingerA.position);
        drawSphere(_middleFingerB.position);
        drawSphere(_middleFingerC.position);

        drawSphere(_pinkyFinger.position);
        drawSphere(_pinkyFingerA.position);
        drawSphere(_pinkyFingerB.position);
        drawSphere(_pinkyFingerC.position);

        drawSphere(_ringFinger.position);
        drawSphere(_ringFingerA.position);
        drawSphere(_ringFingerB.position);
        drawSphere(_ringFingerC.position);

        drawSphere(_thumbFinger.position);
        drawSphere(_thumbFingerA.position);
        drawSphere(_thumbFingerB.position);
        #endregion

        #region Draw Lines
        drawCylinder(_fist.position, _indexFinger.position);
        drawCylinder(_fist.position, _middleFinger.position);
        drawCylinder(_fist.position, _pinkyFinger.position);
        drawCylinder(_fist.position, _ringFinger.position);
        drawCylinder(_fist.position, _thumbFinger.position);

        drawCylinder(_indexFinger.position, _indexFingerA.position);
        drawCylinder(_indexFingerA.position, _indexFingerB.position);
        drawCylinder(_indexFingerB.position, _indexFingerC.position);

        drawCylinder(_middleFinger.position, _middleFingerA.position);
        drawCylinder(_middleFingerA.position, _middleFingerB.position);
        drawCylinder(_middleFingerB.position, _middleFingerC.position);

        drawCylinder(_pinkyFinger.position, _pinkyFingerA.position);
        drawCylinder(_pinkyFingerA.position, _pinkyFingerB.position);
        drawCylinder(_pinkyFingerB.position, _pinkyFingerC.position);

        drawCylinder(_ringFinger.position, _ringFingerA.position);
        drawCylinder(_ringFingerA.position, _ringFingerB.position);
        drawCylinder(_ringFingerB.position, _ringFingerC.position);

        drawCylinder(_thumbFinger.position, _thumbFingerA.position);
        drawCylinder(_thumbFingerA.position, _thumbFingerB.position);
        #endregion
    }

    private void drawSphere(Vector3 position, Transform positionOtherHand, float aceptableRange)
    {
        if (_compareOtherHand)
        {
            float realAceptable = aceptableRange == 0 ? _defaultAcceptableDistance : aceptableRange;
            if (positionOtherHand != null && Vector3.Distance(position, positionOtherHand.position) > realAceptable)
            {
                drawSphere(position, _jointRadius, _sphereMatRed);
            }
            else
            {
                drawSphere(position, _jointRadius, _sphereMatDefault);
            }
        }
    }

    private void drawSphere(Vector3 position)
    {
        drawSphere(position, _jointRadius, _sphereMatDefault);
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
