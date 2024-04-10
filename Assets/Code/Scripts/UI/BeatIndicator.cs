using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BeatIndicator : MonoBehaviour {
    private RectTransform _rectTransform;
    private Image _image;
    private Composer _musicManager;

    private float _crossingTime;
    private float _velocity;
    private int _lastMusicSegmentPos;
    private bool _hasCrossed;
    
    private double OkWindowStart, GoodWindowStart, PerfectWindowStart;
    private double OkWindowEnd, GoodWindowEnd, PerfectWindowEnd;

    private Tween _fadeTween;

    public UnityEvent<BeatIndicator> OnDestruction;
    
    private void Awake() {
        this._rectTransform = GetComponent<RectTransform>();
        this._image = GetComponent<Image>();

        OnDestruction = new UnityEvent<BeatIndicator>();
    }

    // Update is called once per frame
    private void Update() {
        var rectTransformPosition = this._rectTransform.localPosition;
        rectTransformPosition.x += this._velocity * Time.deltaTime;

        this._rectTransform.localPosition = rectTransformPosition;

        // Adjust the crossing time according to the new music time (starts back at 0) when the segment is looped
        var currTime = this._musicManager.GetMusicTimeMS();
        // NOTE: Sometimes the current position in the segment may be less than the last, so the offset is needed to 
        // avoid destroying the beat indicator before it has actually passed the marker
        if (this._lastMusicSegmentPos > currTime + 10) {
            this.UpdateCrossingTime(this._lastMusicSegmentPos + currTime);
        }
        this._lastMusicSegmentPos = currTime;

        if (!(currTime >= this._crossingTime) || this._hasCrossed) return;
        
        this._hasCrossed = true;
            
        // Once the indicator has crossed, tween the alpha values and destroy this indicator when it's faded out
        this._fadeTween = DOVirtual.Float(this._image.color.a, 0, 0.3f,
                newAlpha => {
                    var imageColor = this._image.color;
                    this._image.color = new Color(imageColor.r, imageColor.g, imageColor.b, newAlpha);
                })
            .OnComplete(() => {
                this.OnDestruction?.Invoke(this);
                Destroy(this.gameObject);
            });
    }

    public void Initialize(BeatSyncBar syncBar, Composer musicManager) {
        this._musicManager = musicManager;
        this._rectTransform.localPosition = Vector3.right * syncBar.beatOffset * syncBar.indicatorOffset;
        
        this._crossingTime = musicManager.GetCrossingTimeMS(syncBar.beatOffset);
        this.UpdateVelocity();
        
        this.OkWindowStart = this._crossingTime - (0.5f * syncBar.OkWindowMS);
        this.OkWindowEnd = this._crossingTime + (0.5f * syncBar.OkWindowMS);
        this.GoodWindowStart = this._crossingTime - (0.5f * syncBar.GoodWindowMS);
        this.GoodWindowEnd = this._crossingTime + (0.5f * syncBar.GoodWindowMS);
        this.PerfectWindowStart = this._crossingTime - (0.5f * syncBar.PerfectWindowMS);
        this.PerfectWindowEnd = this._crossingTime + (0.5f * syncBar.PerfectWindowMS);
    }

    public BeatSyncBar.EvaluationState EvaluateInput() {
        var currTime = this._musicManager.GetMusicTimeMS();

        // Will not evaluate the beat indicator if the timing is too early
        if (currTime < OkWindowStart) return BeatSyncBar.EvaluationState.Early;

        var result = BeatSyncBar.EvaluationState.Late;
        if (currTime > PerfectWindowStart && currTime < PerfectWindowEnd) {
            print("PERFECT!");
            result = BeatSyncBar.EvaluationState.Perfect;
        } else if (currTime > GoodWindowStart && currTime < GoodWindowEnd) {
            print("Good!");
            result = BeatSyncBar.EvaluationState.Good;
        } else if (currTime > OkWindowStart && currTime < OkWindowEnd) {
            print("Okay");
            result = BeatSyncBar.EvaluationState.Okay;
        } else {
            print("Late");
        }
        
        this._fadeTween.Kill();
        Destroy(this.gameObject);
        return result;
    }

    private void UpdateVelocity() {
        this._velocity = (0f - this._rectTransform.localPosition.x) / (0.001f * (this._crossingTime - this._musicManager.GetMusicTimeMS()));
    }

    private void UpdateCrossingTime(int loopTime) {
        this._crossingTime -= loopTime;
        this.OkWindowStart -= loopTime;
        this.OkWindowEnd -= loopTime;
        this.GoodWindowStart -= loopTime;
        this.GoodWindowEnd -= loopTime;
        this.PerfectWindowStart -= loopTime;
        this.PerfectWindowEnd -= loopTime;
    }
}