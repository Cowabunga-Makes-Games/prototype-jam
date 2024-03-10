using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Composer : MonoBehaviour {
    public uint playingID;
    
    // ----------------- Input -------------------
    public float SecondsPerBeat { get; private set; }
    
    // ------------- Music Events ----------------
    [SerializeField] private AK.Wwise.Event _playMusic;

    // ------------- Unity Events ----------------
    public UnityEvent OnBeat;
    public UnityEvent OnBar;
    public UnityEvent OnGrid;
    
    void Start() {
        playingID = _playMusic.Post(this.gameObject, 
            (uint)(AkCallbackType.AK_MusicSyncAll | AkCallbackType.AK_EnableGetMusicPlayPosition), 
            OnMusicCallback);
    }

    private void OnMusicCallback(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info) {
        
        if (in_info is not AkMusicSyncCallbackInfo musicData) return;
        
        // Switch for AkCallbackType; all types can be found below:
        // https://www.audiokinetic.com/en/library/edge/?source=SDK&id=_ak_callback_8h_a948c083ff18dc4c8dfe1d32cb0eb6732.html
        switch (musicData.musicSyncType) {
            case AkCallbackType.AK_MusicSyncUserCue:
                // CustomCues(_musicInfo.userCueName, _musicInfo);
                // TODO: Currently, we don't have any custom cues set up yet, but when we do they'll be handled here
                
                break;
            
            case AkCallbackType.AK_MusicSyncBeat:
                this.OnBeat?.Invoke();
                break;
            
            case AkCallbackType.AK_MusicSyncBar:
                SecondsPerBeat = musicData.segmentInfo_fBeatDuration;
                // print("Seconds Per Beat: " + secondsPerBeat);

                this.OnBar.Invoke();
                break;

            case AkCallbackType.AK_MusicSyncGrid:
                this.OnGrid.Invoke();
                break;
            
            default:
                break;
        }
    }
    
    public int GetMusicTimeMS() {
        var segmentInfo = new AkSegmentInfo();
        AkSoundEngine.GetPlayingSegmentInfo(playingID, segmentInfo, true);

        // print(segmentInfo.iCurrentPosition);
        return segmentInfo.iCurrentPosition;
    }
    
    public int GetLoopedMusicTimeMS() {
        var segmentInfo = new AkSegmentInfo();
        AkSoundEngine.GetPlayingSegmentInfo(playingID, segmentInfo, true);
        
        return segmentInfo.iCurrentPosition;
    }
    
    // Determine when the beat indicator should cross the line
    public int GetCrossingTimeMS(int beatOffset) {
        var segmentInfo = new AkSegmentInfo();
        AkSoundEngine.GetPlayingSegmentInfo(playingID, segmentInfo, true);

        return segmentInfo.iCurrentPosition + Mathf.RoundToInt(1000 * SecondsPerBeat * beatOffset);
    }
}
