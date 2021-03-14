using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

//[RequireComponent(typeof(Image))]
public class LoadImageFromAddressables : MonoBehaviour
{
    [SerializeField]
    private AssetReferenceSprite referenceSprite;

    //private Image image;
    //private Image
    private void Awake()
    {

        if(referenceSprite != null)
        {
            referenceSprite.LoadAssetAsync<Sprite>().Completed += (_op) =>
            {
                var image = GetComponent<Image>();
                image.sprite = _op.Result;
            };
        }
    }


    //// Start is called before the first frame update
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
