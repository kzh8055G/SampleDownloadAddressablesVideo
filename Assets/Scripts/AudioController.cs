using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AssetReferenceT<AudioClip> audioClipSample;
    
    [SerializeField]
    private Button buttonPlay;

    [SerializeField]
    private Text textAssetDownloadStatus;

    [SerializeField]
    private float delaySecLoadAsset;

    private AsyncOperationHandle<AudioClip> opHandle;
    private void Awake()
    {
        if(buttonPlay == null || textAssetDownloadStatus == null)
        {
            Debug.Log("Audio Controller is no Ready!!!");
            return;
        }
        
        if( audioClipSample == null)
        {
            Debug.Log("Audio Clip is no Ready!!!");
            return;
        }
        StartCoroutine(_CoroutineLoadAsset(delaySecLoadAsset));

    }
    private IEnumerator _CoroutineLoadAsset(float _delaySec)
    {
        textAssetDownloadStatus.text = "Wait";
        
        yield return new WaitForSeconds(_delaySec);

        opHandle = audioClipSample.LoadAssetAsync<AudioClip>();
        opHandle.Completed += (_op) =>
        {
            textAssetDownloadStatus.text = "Play";

            var audioSource = GetComponent<AudioSource>();
            audioSource.clip = _op.Result;
            buttonPlay.onClick.AddListener(() =>
            {
                audioSource.Play();
            });
        };
    }

    private void Update()
    {
        if (opHandle.IsValid() && textAssetDownloadStatus != null)
        {
            var downloadStatus = opHandle.GetDownloadStatus();
            if (!downloadStatus.IsDone)
            {
                textAssetDownloadStatus.text = $"Loading Asset ... {(downloadStatus.Percent * 100)}";
            }

        }
    }
    // Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
