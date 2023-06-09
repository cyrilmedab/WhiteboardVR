using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;

public class Marker : MonoBehaviour
{

    [SerializeField] private Transform _markerTip;
    [SerializeField] private float _tipHeight; // Unserialize this once height confirmed
    
    [Range(1, 1000)]
    [SerializeField] private int _tipSize = 10;

    private Renderer _renderer;
    private Color[] _colors;
    // Number of pixels that will be colored dependent on _tipSize
    private int _pixelArea;
    public bool isEraser;
    
    [SerializeField] private Whiteboard _whiteboard;
    private RaycastHit _surface;
    private Vector2 _contact, _lastDrawPos;
    private bool _didDraw;
    private Quaternion _lastDrawRot;

    public float eraserHeight = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        _renderer = _markerTip.GetComponent<Renderer>();
        
        _pixelArea = Mathf.FloorToInt(Mathf.PI * Mathf.Pow(_tipSize, 2));
        _colors = Enumerable.Repeat(_renderer.material.color, _pixelArea).ToArray();

        _tipHeight = isEraser ? 0.06f : _markerTip.localScale.y; 
        //_tipHeight = _markerTip.localScale.y;

    }

    // Update is called once per frame
    void Update()
    {
        Draw();
    }

    private void Draw()
    {
        if (Physics.Raycast(_markerTip.position, _markerTip.transform.up, out _surface, _tipHeight))
        {
            Debug.Log("HITTT");
            if (!_surface.transform.CompareTag("Whiteboard")) return;
            
            //functionality for multiple whiteboards
            // too expensive?
            //_whiteboard = _surface.transform.GetComponent<Whiteboard>();

            _contact = new Vector2(_surface.textureCoord.x, _surface.textureCoord.y);
            int x = (int)(_contact.x * _whiteboard.textureSize.x - (_tipSize / 2));
            int y = (int)(_contact.y * _whiteboard.textureSize.y - (_tipSize / 2));

            if (y < 0 || y > _whiteboard.textureSize.y || y > _whiteboard.textureSize.y) return;

            if (_didDraw)
            {
                _whiteboard.texture.SetPixels(x, y, _tipSize, _tipSize, _colors);

                for (float f = 0.01f; f < 1.00f; f += 0.02f)
                {
                    var lerpX = (int)Mathf.Lerp(_lastDrawPos.x, x, f);
                    var lerpY = (int)Mathf.Lerp(_lastDrawPos.y, y, f);
                    _whiteboard.texture.SetPixels(lerpX, lerpY, _tipSize, _tipSize, _colors);
                }

                transform.rotation = _lastDrawRot;
                
                _whiteboard.texture.Apply();
            }

            _lastDrawPos = new Vector2(x, y);
            _lastDrawRot = transform.rotation;
            _didDraw = true;
            return;

        }

        _didDraw = false;

    }
}
