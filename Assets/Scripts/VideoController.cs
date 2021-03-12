using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoController : MonoBehaviour
{
    [SerializeField]
    private AssetReferenceT<VideoClip> videoClip;

    [SerializeField]
    private Button buttonPlayVideo;

    [SerializeField]
    private Text textAssetDownloadStatus;

    private VideoPlayer vPlayer;

    private AsyncOperationHandle<VideoClip> opHandle;
    
    private void Awake()
    {
        // https://forum.unity.com/threads/how-to-play-video-from-adressable-assetbundle-on-android.685261/
        //Caching.ClearCache();
        Caching.compressionEnabled = false;

        if(buttonPlayVideo == null)
        {
            Debug.Log("Not Set Play Button");
            return;
        }
        vPlayer = GetComponent<VideoPlayer>();

        opHandle = videoClip.LoadAssetAsync<VideoClip>();
        opHandle.Completed += (_op) =>
        {
            textAssetDownloadStatus.text = "Play";

            buttonPlayVideo.onClick.AddListener(() =>
            {
                vPlayer.clip = _op.Result;
                vPlayer.Play();
            });
        };
        //opHandle.OperationException.
    }

    private void Update()
    {
        if(textAssetDownloadStatus != null)
        {
            var downloadStatus = opHandle.GetDownloadStatus();
            if( !downloadStatus.IsDone )
            {
                textAssetDownloadStatus.text = $"Loading Asset ... {(downloadStatus.Percent * 100)}";
            }
            
        }
    }
}