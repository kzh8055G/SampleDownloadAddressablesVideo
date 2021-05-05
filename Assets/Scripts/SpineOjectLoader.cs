using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

[Serializable]
public struct BindButtonLoadPrefabInfo
{
    public Button button;
    public AssetReferenceGameObject assetRef;
    public ELoadMethodType loadMethodType;

    public bool Valid => button != null && ( assetRef != null && assetRef.RuntimeKeyIsValid());    
}

public enum ELoadMethodType
{
    Sync,
    AsyncWithTask,
    AsyncWithUniTask,
    AsyncWithAsyncOperation,
    AsyncWithCompleteEvent,
}

public class SpineOjectLoader : MonoBehaviour
{
    [SerializeField]
    private List<BindButtonLoadPrefabInfo> bindInfos;

    private void _RegisterLoadMethod(BindButtonLoadPrefabInfo info)
    {
        if( info.Valid)
        {
            info.button.onClick.AddListener(async () =>
            {
                switch (info.loadMethodType)
                {
                    case ELoadMethodType.Sync:
                        {
                            var instance = info.assetRef.Instantiate();
                            break;
                        }
                    case ELoadMethodType.AsyncWithAsyncOperation:
                        {
                            var opHandle = info.assetRef.InstantiateAsync();
                            await opHandle;
                            var instance = opHandle.Result;

                            break;
                        }
                    case ELoadMethodType.AsyncWithTask:
                        {
                            var opHandle = info.assetRef.InstantiateAsync().Task;
                            await opHandle;

                            var instance = opHandle.Result;

                            break;
                        }
                    case ELoadMethodType.AsyncWithUniTask:
                        {
                            var instance = await info.assetRef.InstantiateAsync().ToUniTask();

                            break;
                        }
                    case ELoadMethodType.AsyncWithCompleteEvent:
                        {
                            info.assetRef.InstantiateAsync().Completed += (_op) =>
                            {
                                if(_op.IsDone)
                                {


                                }
                            };
                            break;
                        }
                }
            });
        }
    }

    void Start()
    {
        //var loadMethods =
        //    new List<Func<AssetReferenceGameObject, GameObject>>()
        //{
        //        (_) =>
        //        {
        //            var opHandle = _.InstantiateAsync();
        //            await opHandle;

        //            return new GameObject();
        //        },
        //};
        //_LoadAsync();
        //foreach(var info in bindInfos)
        //{
        //    if(info.Valid)
        //    {
        //        info.button.onClick.AddListener( ()=>
        //        {
        //            var opHandle = info.assetRef.InstantiateAsync();
        //            opHandle.

        //        });

        //    }
        //}

        foreach(var info in bindInfos)
        {
            _RegisterLoadMethod(info);
        }
    }

    //private async void _Load1stPrefabAsync()
    //{

    //}

    //private async void _Load1stPrefabAsync()
    //{


    //}
    //private async void _Load1stPrefabAsync()
    //{


    //}
    //private async void _Load1stPrefabAsync()
    //{

    //}

}
