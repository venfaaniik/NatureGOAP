using UnityEngine;
using System.Collections;



public class WaterPhysics : MonoBehaviour
{

    Vector3 waveSource1 = new Vector3(2.0f, 0.0f, 2.0f);
    public float waveFrequency = 0.0f;
    public float waveHeight = 0.0f;
    public float waveLength = 0.0f;

    Mesh mesh;
    Vector3[] verts;

    void Start()
    {
        Camera.main.depthTextureMode |= DepthTextureMode.Depth;
        MeshFilter mf = GetComponent<MeshFilter>();
        makeMeshLowPoly(mf);

    }

    MeshFilter makeMeshLowPoly(MeshFilter mf)
    {
        mesh = mf.sharedMesh;
        Vector3[] oldVerts = mesh.vertices;
        int[] triangles = mesh.triangles;
        Vector3[] vertices = new Vector3[triangles.Length];
        for (int i = 0; i < triangles.Length; i++)
        {
            vertices[i] = oldVerts[triangles[i]];
            triangles[i] = i;
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        verts = mesh.vertices;
        return mf;
    }


    void Update()
    {
        CalcWave();
    }

    void CalcWave()
    {
        for (int i = 0; i < verts.Length; i++)
        {
            Vector3 v = verts[i];
            v.y = 0.0f;
            float dist = Vector3.Distance(v, waveSource1);
            dist = (dist % waveLength) / waveLength;
            v.y = waveHeight * Mathf.Sin(Time.time * Mathf.PI * 2.0f * waveFrequency
            + (Mathf.PI * 2.0f * dist));
            verts[i] = v;
        }
        mesh.vertices = verts;
        mesh.RecalculateNormals();
        mesh.MarkDynamic();

        GetComponent<MeshFilter>().mesh = mesh;
    }
}