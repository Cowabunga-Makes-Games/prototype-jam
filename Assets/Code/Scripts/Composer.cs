using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Composer : MonoBehaviour {
    public uint playingID;
    public ColorChanger cube;

    [SerializeField] private AK.Wwise.Event _playMusic;

    public UnityEvent OnBeat;
    
    // Start is called before the first frame update
    void Start() {
        cube.Initialize(OnBeat);
        
        playingID = _playMusic.Post(this.gameObject, 
            (uint)(AkCallbackType.AK_MusicSyncAll | AkCallbackType.AK_EnableGetMusicPlayPosition), 
            OnMusicCallback);
    }

    private void OnMusicCallback(object in_cookie, AkCallbackType in_type, AkCallbackInfo in_info) {
        
        if (!(in_info is AkMusicSyncCallbackInfo info)) return;

            //we're going to use this switchboard to fire off different events depending on wwise sends
            switch (info.musicSyncType) {
                case AkCallbackType.AK_MusicSyncBeat:
                    OnBeat?.Invoke();
                    break;
            }
    }
}
