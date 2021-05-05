using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteLoader : MonoBehaviour
{
    [SerializeField]
    public AssetReferenceAtlasedSprite atlasedSprite;

    //private SpriteRenderer spriteRenderer;
    private async void Awake()
    {
        if(atlasedSprite == null)
        {
            Debug.Log("not set atlased sprite!!!");
        }

        var opHandle = atlasedSprite.LoadAssetAsync<Sprite>();
        await opHandle;

        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = opHandle.Result;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
