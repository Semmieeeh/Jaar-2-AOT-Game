using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    public GameObject weaponTip, weaponBase, trailMesh;
    public int trailFrameLenth;
    public Mesh mesh;
    public Vector3[] vertices;
    public int[] triangles;
    public int frameCount;
    public const int NUM_VERTICES = 12;
    public Vector3 previousTipPos;
    public Vector3 previousBasePos;
    void Start()
    {
        mesh = new Mesh();
        trailMesh.GetComponent<MeshFilter>().mesh = mesh;

        vertices = new Vector3[trailFrameLenth * NUM_VERTICES];
        triangles = new int[vertices.Length];
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(frameCount == (trailFrameLenth * NUM_VERTICES))
        {
            frameCount = 0;
        }
        vertices[frameCount] = base.transform.position;
        vertices[frameCount + 1] = weaponTip.transform.position;
        vertices[frameCount + 2] = previousTipPos;

        vertices[frameCount + 3] = weaponBase.transform.position;
        vertices[frameCount + 4] = previousTipPos;
        vertices[frameCount + 5] = weaponTip.transform.position;

        vertices[frameCount + 6] = previousTipPos;
        vertices[frameCount + 7] = weaponBase.transform.position;
        vertices[frameCount + 8] = previousBasePos;

        vertices[frameCount + 9] = previousTipPos;
        vertices[frameCount + 10] = previousBasePos;
        vertices[frameCount + 11] = weaponBase.transform.position;


        triangles[frameCount] = frameCount ;
        triangles[frameCount + 1] = frameCount + 1;
        triangles[frameCount + 2] = frameCount + 2;
        triangles[frameCount + 3] = frameCount + 3;
        triangles[frameCount + 4] = frameCount + 4;
        triangles[frameCount + 5] = frameCount + 5;
        triangles[frameCount + 6] = frameCount + 6;
            
        triangles[frameCount + 7] = frameCount + 7;
        triangles[frameCount + 8] = frameCount + 8;
        triangles[frameCount + 9] = frameCount + 9;
        triangles[frameCount + 10] = frameCount + 10;
        triangles[frameCount + 11] = frameCount + 11;

        mesh.vertices = vertices;
        mesh.triangles = triangles;


        previousBasePos = weaponBase.transform.position;
        previousTipPos = weaponTip.transform.position;







    }
}
