using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatSyncBar : MonoBehaviour {
    public Composer MusicManager;
    public GameObject BeatIndicator;
    public GameObject BeatIndicatorContainer;

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
    }

    private void OnBeat() {
        var beatUI = Instantiate(BeatIndicator, this.BeatIndicatorContainer.transform);
        var beatIndicator = beatUI.GetComponent<BeatIndicator>();

        beatIndicator.Initialize(this, MusicManager);
        this._activeIndicators.Enqueue(beatIndicator);
    }
}
