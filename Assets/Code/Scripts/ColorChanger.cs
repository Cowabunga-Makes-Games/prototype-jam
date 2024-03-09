using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColorChanger : MonoBehaviour {
    public Material green, purple;
    private bool _isGreen;

    private MeshRenderer _renderer;

    private void Awake() {
        _renderer = GetComponent<MeshRenderer>();
    }

    public void Initialize(UnityEvent onBeat) {
        onBeat.AddListener(this.ChangeColor);
    }

    public void ChangeColor() {
        _renderer.material = _isGreen ? purple : green;
        _isGreen = !_isGreen;
    }
}
