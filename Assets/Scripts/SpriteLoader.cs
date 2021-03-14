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
    private void Awake()
    {
        if(atlasedSprite == null)
        {
            Debug.Log("not set atlased sprite!!!");
        }
        
        atlasedSprite.LoadAssetAsync<Sprite>().Completed += (_op) =>
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = _op.Result;


        };
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
