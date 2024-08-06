using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour {
    //this script is attached to the background it controlls the offset of the background image to create a moving background effect
    private MeshRenderer meshRenderer;

    [SerializeField] private float parralax = 2f;

    // Start is called before the first frame update
    void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update() {
        Material material = meshRenderer.material;
        Vector2 offset = material.GetTextureOffset("_MainTex");

        //the offset is changed every frame in the x and y direction based on the position of the player
        offset.x = transform.position.x / transform.localScale.x / parralax;
        offset.y = transform.position.y / transform.localScale.y / parralax;
        material.mainTextureOffset = offset;
    }
}
