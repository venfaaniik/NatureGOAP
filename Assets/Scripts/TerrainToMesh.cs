using UnityEngine;
using UnityEditor;


public class TerrainToMesh : MonoBehaviour
{

    /// <summary>
    /// Creates mesh from terrain data
    /// </summary>
    /// <param name="sampleSize">how many squares you want on row/line</param>
    /// <param name="width">how wide is the mesh</param>
    public static Mesh CreateMeshFromTerrainData(int sampleSize, int width, TerrainData d)
    {
        if (sampleSize != 0 && width != 0 && d)
        {

            Mesh mesh = new Mesh();
            Vector3[] vertices = MakeVerticesGrid(sampleSize, width);
            float[] heightMap = getHeightMap(sampleSize, d, width);

            HeightMapToGrid(heightMap, vertices);

            mesh = BuildMeshFromGrid(vertices, sampleSize);
            //MakeVerticesGrid(vertices, sampleSize, squareLength);
            mesh.RecalculateNormals();

            return mesh;
        }
        else
        {
            Debug.LogError("sampleSize or width is 0 or TerrainData is null");
            return null;
        }
    }


    /// <summary>
    /// Creates flat 3D grid from sample size and mesh width
    /// </summary>
    /// <param name="sampleSize">how many squares in a row/line</param>
    /// <param name="width">width of the mesh</param>
    /// <returns></returns>
    public static Vector3[] MakeVerticesGrid(int sampleSize, int width)
    {
        Vector3[] vertices = new Vector3[(sampleSize + 1) * (sampleSize + 1)];
        int sqLength = width / sampleSize;
        int vertexCounter = 0;
        //Assign x and z values for vertices[] to make flat grid
        //starting from lowest row and going up
        for (int z = 0; z <= sampleSize; z++)
        {
            for (int x = 0; x <= sampleSize; x++)
            {
                vertices[vertexCounter].x = x * sqLength;
                vertices[vertexCounter].z = z * sqLength;
                vertexCounter++;
            }
        }

        return vertices;
    }


    /// <summary>
    /// Assigns HeightMap for vertices grid
    /// </summary>
    /// <param name="heightMap">float[] heightMap</param>
    /// <param name="vertices">Vector3[] vertices</param>
    public static void HeightMapToGrid(float[] heightMap, Vector3[] vertices)
    {
        //Assign values from heights to vertices.y coordinate
        if (heightMap.Length == vertices.Length)
        {
            for (int i = 0; i < heightMap.Length; i++)
            {
                vertices[i].y = heightMap[i];
                //Debug.Log("(" + vertices[i].x + "," + vertices[i].y + "," + vertices[i].z + ")");
            }
        }
        else
        {
            Debug.Log("Arrays not same size");
        }
    }

    /// <summary>
    /// Returns float[] heigtMap from TerrainData
    /// </summary>
    /// <param name="sampleSize">How many samples we take per line</param>
    /// <param name="d">TerrainData we want the heightMap from</param>
    /// <returns></returns>
    public static float[] getHeightMap(int sampleSize, TerrainData d, int width)
    {
        int calcPoints = (sampleSize + 1) * (sampleSize + 1);
        float[] heightMap = new float[calcPoints];
        float textureWidth = d.heightmapWidth;
        float sampleLength = (float)textureWidth / sampleSize;
        float widthPercent = 1000 / width;

        int pointCounter = 0;
        for (int y = 0; y <= sampleSize; y++)
        {
            for (int x = 0; x <= sampleSize; x++)
            {
                heightMap[pointCounter] = d.GetHeight((int)(x * sampleLength), (int)(y * sampleLength)) / widthPercent;
                pointCounter++;
            }
        }


        return heightMap;
    }


    /// <summary>
    /// Returns a mesh that is made from vertices and sampleSize
    /// </summary>
    /// <param name="vertices">Vector3 array of vertices</param>
    /// <param name="sampleSize">How many squares do we have in a line</param>
    public static Mesh BuildMeshFromGrid(Vector3[] vertices, int sampleSize)
    {
        Mesh m = new Mesh();
        int[] triangles = new int[sampleSize * sampleSize * 6]; //Every square is composed of 2 triangles and each triangle is made from 3 lines
        Vector2[] uvs = new Vector2[vertices.Length];

        SetUVMap(uvs, vertices);
        SetTriangles(triangles, sampleSize);

        m.vertices = vertices;
        m.uv = uvs;
        m.triangles = triangles;

        //Debug
        return m;
    }

    /// <summary>
    /// Assigns uv coordinates from vertices
    /// </summary>
    /// <param name="uv">Vector2 array of uv</param>
    /// <param name="vertices">Vector3 array of vertices</param>
    static void SetUVMap(Vector2[] uv, Vector3[] vertices)
    {
        //Set uv coordinates to be same as vertices x and z coordinates
        for (int i = 0; i < vertices.Length; i++)
        {
            uv[i].x = vertices[i].x;
            uv[i].y = vertices[i].z;
        }
    }

    /// <summary>
    /// Assigns all line values that make the triangles
    /// </summary>
    /// <param name="triangles">int array for triangles</param>
    /// <param name="sampleSize">how many squares do we have in a line</param>
    static void SetTriangles(int[] triangles, int sampleSize)
    {
        int triangleSideCounter = 0;
        int lineCounter = 0;
        //Loop through all squares in grid, using low left corner as reference point
        for (int sqCounter = 0; sqCounter < ((sampleSize + 1) * (sampleSize + 1) - (sampleSize + 1)); sqCounter++)
        {
            //Ignore last vertex in X axle
            if (sqCounter != sampleSize + ((sampleSize + 1) * lineCounter))
            {
                //Every square is divided in to 2 triangles
                for (int i = 0; i < 2; i++)
                {
                    //Every triangle has 3 sides
                    for (int j = 0; j < 3; j++)
                    {
                        if (j == 0)
                        {
                            triangles[triangleSideCounter] = sqCounter + i;
                        }
                        else if (j == 1)
                        {
                            triangles[triangleSideCounter] = sqCounter + sampleSize + 1;
                        }
                        else
                        {
                            triangles[triangleSideCounter] = (sqCounter + 1 + (i * (sampleSize + 1)));
                        }
                        triangleSideCounter++;
                    }
                }
            }
            else //if we are on last vertex of line, we change line
            {
                if (triangleSideCounter % (sampleSize * 6) == 0)
                {
                    lineCounter++;
                }
            }
        }
    }

    /// <summary>
    /// Creates mesh file in assets directory
    /// </summary>
    /// <param name="m">Name of the file</param>
    /// <param name="path">Path for the save location</param>
    public static void CreateMesh(Mesh m, string path)
    {
        AssetDatabase.CreateAsset(m, "Assets/" + path + ".mesh");
    }


}
