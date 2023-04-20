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

        var colors = new Color32[1];
        colors[0] = _renderer.material.color;
        
        var cols = texture.GetPixels32(0);
        for (int i = 0; i < cols.Length; i++)
        {
            cols[i] = colors[0];
        }
        texture.SetPixels32(cols, 0);


        texture.Apply();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
