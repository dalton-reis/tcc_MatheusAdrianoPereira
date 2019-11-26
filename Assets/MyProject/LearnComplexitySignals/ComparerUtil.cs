using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComparerUtil
{
    internal static bool Compare(MarkerAnimation marker, DrawHands handRecordered, DrawHands handTrack)
    {
        bool result = true;
        foreach (var pointCompare in marker.Compare)
        {
            //var referencePointRecordered = handRecordered.GetPoint(pointCompare.part.Substring(0, 1) + "_PALM");
            var pointCompareRecordered = handRecordered.GetPoint(pointCompare.part);

            //var referencePointTrack = handTrack.GetPoint(pointCompare.part.Substring(0, 1) + "_PALM");
            var pointCompareTrack = handTrack.GetPoint(pointCompare.part);

            var matrixTracked = handTrack.GetLocalToWorldMatrixReference(pointCompare.part.Substring(0, 1));
            var matrixRecordered = handRecordered.GetWorldToLocalMatrixReference(pointCompare.part.Substring(0, 1));

            //var distanceReferenceRecordered = (referencePointRecordered.transform.position - pointCompareRecordered.transform.position).magnitude;
            //var distanceReferenceTrack = (referencePointTrack.transform.position - pointCompareTrack.transform.position).magnitude;

            //var matrixTracked = new Matrix4x4(referencePointTrack.transform.localToWorldMatrix.GetColumn(0),
            //    referencePointTrack.transform.localToWorldMatrix.GetColumn(1),
            //    referencePointTrack.transform.localToWorldMatrix.GetColumn(2),
            //    referencePointTrack.transform.localToWorldMatrix.GetColumn(3));
            //var matrixRecordered = referencePointRecordered.transform.worldToLocalMatrix;
            ///TODO como manter a rotação na matrix de transformação
            //rotation.SetFromToRotation(new Vector3(0, 0, 0), new Vector3(1,0,0));// Set(matrixRecordered.rotation.x, matrixRecordered.rotation.y, matrixRecordered.rotation.z, matrixRecordered.rotation.w);

            var pointProjectionTrack = matrixTracked.MultiplyPoint(matrixRecordered.MultiplyPoint(pointCompareRecordered.transform.position));

            var distance = (pointProjectionTrack - pointCompareTrack.transform.position).magnitude;
            var distanceAcceptable = pointCompare.value / 5;

            //if (distanceReferenceTrack < distanceReferenceRecordered - pointCompare.value || distanceReferenceTrack > distanceReferenceRecordered + pointCompare.value)
            if (distance > distanceAcceptable || distance < -distanceAcceptable)
            //if (distance > 0.01f || distance < -0.01f)
            {
                result = false;
                if (handTrack.DrawHand(pointCompare.part.Substring(0, 1)))
                {
                    drawSphere(pointProjectionTrack, distanceAcceptable, handTrack, pointCompare.part.Substring(0, 1));
                    //drawCylinder2(pointProjectionTrack,
                    //    pointCompareTrack.transform.position,
                    //    handTrack);
                }

            }
        }
        return result;
    }

    private static void drawSphere(Vector3 position, float radius, DrawHands drawHands, string side)
    {
        var material = new Material(drawHands._material);
        if (side == "R") material.color = drawHands._colorSpheresRight;
        else material.color = drawHands._colorSpheresLeft;
        //multiply radius by 2 because the default unity sphere has a radius of 0.5 meters at scale 1.
        Graphics.DrawMesh(drawHands._sphereMesh,
                          Matrix4x4.TRS(position,
                                        Quaternion.identity,
                                        Vector3.one * radius * 2.0f * drawHands.transform.lossyScale.x),
                          material, 0,
                          null, 0, null, true);
    }

    private static void drawCylinder2(Vector3 a, Vector3 b, DrawHands drawHands)
    {
        var material = new Material(drawHands._material) { color = Color.red };
        float length = (a - b).magnitude;
        Graphics.DrawMesh(getCylinderMesh(length),
                          Matrix4x4.TRS(a,
                                        Quaternion.LookRotation(b - a),
                                        new Vector3(drawHands.transform.lossyScale.x, drawHands.transform.lossyScale.x, 1)),
                          material,
                          drawHands.gameObject.layer,
                          null, 0, null, true);
    }

    private static Dictionary<int, Mesh> _meshMap = new Dictionary<int, Mesh>();
    private static int _cylinderResolution = 12;
    private static float _cylinderRadius = 0.02f;
    protected const float CYLINDER_MESH_RESOLUTION = 0.1f;

    private static Mesh getCylinderMesh(float length)
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
