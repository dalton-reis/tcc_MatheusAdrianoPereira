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
    private Material _sphereMat;

    //[SerializeField]
    //private Transform _arm;
    [SerializeField]
    private Transform _fistNormalize;
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

    // Start is called before the first frame update
    void Start()
    {
        _sphereMat = new Material(_material);
        _sphereMat.color = _colorSpheres;
    }

    // Update is called once per frame
    void Update()
    {
        if(_fistNormalize != null)
        {
            /*
Matrix4x4 matrix = _fist.localToWorldMatrix * _fistNormalize.worldToLocalMatrix.transpose;
drawSphere(matrix.MultiplyPoint(_fist.localPosition));

drawSphere(matrix.MultiplyPoint(_indexFinger.localPosition));
drawSphere(matrix.MultiplyPoint(_indexFingerA.localPosition));
drawSphere(matrix.MultiplyPoint(_indexFingerB.localPosition));
drawSphere(matrix.MultiplyPoint(_indexFingerC.localPosition));

drawSphere(matrix.MultiplyPoint(_middleFinger.localPosition));
drawSphere(matrix.MultiplyPoint(_middleFingerA.localPosition));
drawSphere(matrix.MultiplyPoint(_middleFingerB.localPosition));
drawSphere(matrix.MultiplyPoint(_middleFingerC.localPosition));

drawSphere(matrix.MultiplyPoint(_pinkyFinger.localPosition));
drawSphere(matrix.MultiplyPoint(_pinkyFingerA.localPosition));
drawSphere(matrix.MultiplyPoint(_pinkyFingerB.localPosition));
drawSphere(matrix.MultiplyPoint(_pinkyFingerC.localPosition));

drawSphere(matrix.MultiplyPoint(_ringFinger.localPosition));
drawSphere(matrix.MultiplyPoint(_ringFingerA.localPosition));
drawSphere(matrix.MultiplyPoint(_ringFingerB.localPosition));
drawSphere(matrix.MultiplyPoint(_ringFingerC.localPosition));

drawSphere(matrix.MultiplyPoint(_thumbFinger.localPosition));
drawSphere(matrix.MultiplyPoint(_thumbFingerA.localPosition));
drawSphere(matrix.MultiplyPoint(_thumbFingerB.localPosition));*/
        }

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


        //drawCylinder(_arm.position, _fist.position);

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

        /*Ligacoes.ForEach(it =>
        {
            drawCylinder(transform.position, it.position);
            drawSphere(it.position, _jointRadius);
            });
        drawSphere(transform.position, _jointRadius);*/
    }

    private void drawSphere(Vector3 position)
    {
        drawSphere(position, _jointRadius);
    }

    private void drawSphere(Vector3 position, float radius)
    {
        //multiply radius by 2 because the default unity sphere has a radius of 0.5 meters at scale 1.
        Graphics.DrawMesh(_sphereMesh,
                          Matrix4x4.TRS(position,
                                        Quaternion.identity,
                                        Vector3.one * radius * 2.0f * transform.lossyScale.x),
                          _sphereMat, 0,
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
