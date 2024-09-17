using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile3D : MonoBehaviour
{
    [SerializeField] private Color _baseColor, _offsetColor, _highlightColor;
    [SerializeField] private GameObject _highlight;
    [SerializeField] private MeshRenderer meshRenderer;
    private Color color;
    private Color originalColor;
    private Material material;
    private void Start()
    {
        meshRenderer = this.GetComponent<MeshRenderer>();
        
    }

    public void Init(bool isOffset)
    {
        material = meshRenderer.material;
        color = material.color;
        color.a = 1f;
        material.color = color;
        material.color = isOffset ? _offsetColor : _baseColor;
        originalColor = color;
    }

    void OnMouseEnter()
    {
        _highlight.SetActive(true);
        material.color = _highlightColor;
    }

    void OnMouseExit()
    {
        _highlight.SetActive(false);
    }
 
}
