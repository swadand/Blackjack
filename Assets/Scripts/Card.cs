using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Card : MonoBehaviour
{
    private Transform playerPos;
    private Transform playerPos1;
    private Transform playerPos2;
    private Transform playerPos3;
    private int Score;
    private readonly string[] rank = { "diamond", "heart", "club", "spade" };
    // Start is called before the first frame update
    void Awake()
    {
        playerPos = GameObject.Find("playerCardholder").transform;
        playerPos1 = GameObject.Find("1").transform;
        playerPos2 = GameObject.Find("2").transform;
        playerPos3 = GameObject.Find("3").transform;
    }

    // Update is called once per frame
    void Update()
    {
        ResetTransform();
    }

    public void SetParentPos(int n)
    {
        ResetTransform();
        transform.SetParent(playerPos);
        transform.parent = null;
        switch (n)
        {
            case 1:
                transform.position = playerPos1.position;
                break;
            case 2:
                transform.position = playerPos2.position;
                break;
            case 3:
                transform.position = playerPos3.position;
                break;
        }
    }

    string GetRandomCard()
    {   
        Score = Mathf.FloorToInt(Random.Range(1, 11));
        return $"{Score}-{rank[Random.Range(0, 4)]}";
    }

    void StartCardAnim()
    {
        GetComponent<Animator>().Play("hit");
    }

    void StartOpCardAnim()
    {
        GetComponent<Animator>().Play("opHit");
    }

    public int GetScore()
    {
        return Score;
    }

    public void SetCard()
    {
        string name = GetRandomCard();
        Debug.Log(name);
        Texture2D tex = Resources.Load<Texture2D>($"cards/{name}");
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        // Clone the material to avoid modifying the original material
        Material[] materials = meshRenderer.sharedMaterials;
        Material newMaterial = new Material(Shader.Find("Simple Toon/SToon Default"));
        newMaterial.mainTexture = tex;
        newMaterial.mainTextureOffset = materials[1].mainTextureOffset;
        newMaterial.mainTextureScale = materials[1].mainTextureScale;
        materials[1] = newMaterial;
        meshRenderer.sharedMaterials = materials;

        StartCardAnim();
    }

    public void SetOpCard()
    {
        string name = GetRandomCard();
        Debug.Log(name);
        Texture2D tex = Resources.Load<Texture2D>($"cards/{name}");
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        // Clone the material to avoid modifying the original material
        Material[] materials = meshRenderer.sharedMaterials;
        Material newMaterial = new Material(Shader.Find("Simple Toon/SToon Default"));
        newMaterial.mainTexture = tex;
        newMaterial.mainTextureOffset = materials[1].mainTextureOffset;
        newMaterial.mainTextureScale = materials[1].mainTextureScale;
        materials[1] = newMaterial;
        meshRenderer.sharedMaterials = materials;

        StartOpCardAnim();
    }

    public void ResetTransform()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }
}
