using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSyncBar : MonoBehaviour {
    public Composer MusicManager;
    public PlayerController InputController;
    public GameObject BeatIndicator;
    public GameObject BeatIndicatorContainer;
    
    public enum EvaluationState {
        Early,
        Okay,
        Good,
        Perfect,
        Late
    }

    // How many beats it will take for the beat indicator to reach the target point
    public int beatOffset;
    // Determines how much space there will be between the beat indicators
    public float indicatorOffset;
    
    [Header("Window Sizes in MS")]
    public int OkWindowMS = 200;
    public int GoodWindowMS = 100;
    public int PerfectWindowMS = 50;

    private Queue<BeatIndicator> _activeIndicators = new();

    private void Start() {
        MusicManager.OnBeat.AddListener(this.OnBeat);
        InputController.OnMovementInput.AddListener(this.EvaluateInput);
    }

    private void OnBeat() {
        var beatUI = Instantiate(BeatIndicator, this.BeatIndicatorContainer.transform);
        var beatIndicator = beatUI.GetComponent<BeatIndicator>();

        beatIndicator.Initialize(this, MusicManager);
        this._activeIndicators.Enqueue(beatIndicator);
        beatIndicator.OnDestruction.AddListener(this.RemoveBeatIndicator);
    }

    private void RemoveBeatIndicator(BeatIndicator indicator) {
        if (this._activeIndicators.Peek() != indicator) {
            Debug.LogError("Indicator evaluation order has been derailed!");
        }

        this._activeIndicators.Dequeue();
    }

    private void EvaluateInput() {
        switch (this._activeIndicators.Peek().EvaluateInput()) {
            case EvaluationState.Early:
                return;
            case EvaluationState.Okay:
                // Spawn popup for "Okay"
                break;
            case EvaluationState.Good:
                // Spawn popup for "Good"
                break;
            case EvaluationState.Perfect:
                // Spawn popup for "Perfect"
                break;
            case EvaluationState.Late:
                // Spawn popup for "Late"
                break;
            default:
                break;
        }
        
        this._activeIndicators.Dequeue();
    }
}
