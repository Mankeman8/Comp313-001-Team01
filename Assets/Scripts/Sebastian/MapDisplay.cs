using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    //Reference to the plain
    public Renderer textureRender;
    //Reference to the mesh data that we'll need to create the mesh
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    
    public void DrawTexture(Texture2D texture)
    {
        //preview map inside the editor
        textureRender.sharedMaterial.mainTexture = texture;
        //set size of the plane
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }
    public void DrawMesh(MeshData meshData, Texture2D texture)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}
