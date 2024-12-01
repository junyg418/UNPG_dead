using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapBoundary : MonoBehaviour
{
    public Tilemap tilemap;
    private Bounds _bounds;

    public float leftX;
    public float rightX;
    
    void Awake()
    {
        _bounds = tilemap.localBounds;
        
        leftX = _bounds.min.x;
        rightX = _bounds.max.x;
    }
}
