using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whiteboard : MonoBehaviour
{
    public Texture2D texture;
    public Vector2 textureSize = new Vector2(2048, 2048);

    private Renderer _renderer;
    
    // Start is called before the first frame update
    void Start()
    {
        texture = new Texture2D((int)textureSize.x, (int)textureSize.y);
        _renderer = GetComponent<Renderer>();
        _renderer.material.mainTexture = texture;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
