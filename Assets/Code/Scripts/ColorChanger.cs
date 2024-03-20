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

    private void ChangeColor() {
        if (_isGreen) {
            _renderer.material = purple;
        } else {
            _renderer.material = green;
        }

        _isGreen = !_isGreen;
    }
}
