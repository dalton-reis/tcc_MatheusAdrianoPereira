using UnityEngine;

public class EffectPoint : MonoBehaviour
{

    public bool ActiveTrail = false;
    public bool ActiveFlag = false;
    public Material DefaultMaterial;
    public Mesh SphereMesh;
    //public float JointRadius = 0.06f;
    //public int CylinderResolution = 12;
    //public float CylinderRadius = 0.02f;

    private Material _sphereMat;
    private float SizeFlag;
    private TrailRenderer Trail;
    private bool LastActiveTrail = false;
    // Start is called before the first frame update
    void Start()
    {
        Trail = gameObject.AddComponent<TrailRenderer>();
        Trail.enabled = false;
        Trail.time = 1;
        Trail.generateLightingData = true;

        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0.0f, 0.2f);
        curve.AddKey(.8f, .2f);
        curve.AddKey(1f, 0f);
        Trail.startWidth = 0.4f;
        Trail.widthCurve = curve;
        var mat = new Material(DefaultMaterial);
        mat.color = new Color(Color.yellow.r, Color.yellow.g, Color.yellow.b, 0.8f);
        Trail.material = mat;

        _sphereMat = new Material(DefaultMaterial);
        _sphereMat.color = new Color(1, 1, 1, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (LastActiveTrail != ActiveTrail)
        {
            Trail.enabled = ActiveTrail;
            if (ActiveTrail) Trail.Clear();
        }
        LastActiveTrail = ActiveTrail;


        if (ActiveFlag)
        {
            SizeFlag += Time.deltaTime /2f;
            DrawSphere(SizeFlag);
            if (SizeFlag > 0.3) SizeFlag = 0;
        }
        else
        {
            SizeFlag = 0;
        }
    }

    private void DrawSphere(float radius)
    {
        //multiply radius by 2 because the default unity sphere has a radius of 0.5 meters at scale 1.
        Graphics.DrawMesh(SphereMesh,
                          Matrix4x4.TRS(transform.position,
                                        Quaternion.identity,
                                        Vector3.one * radius * 2.0f * transform.lossyScale.x),
                          _sphereMat, 0,
                          null, 0, null, true);
    }
}
