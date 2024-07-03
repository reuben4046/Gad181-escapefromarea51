using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenMissileIndicator : MonoBehaviour
{
    //list of the missiles
    public List<GameObject> Targets = new List<GameObject>();
    //marker that shows where the missiles are
    public GameObject indicatorPrefab; 

    //sprite render for the marker 
    private SpriteRenderer spriteRenderer;
    private float spriteWidth;
    private float spriteHeight;

    //reference to the camera
    private new Camera camera;

    //dictionary that stores the marker and the missile
    private Dictionary<GameObject, GameObject> targetIndicators = new Dictionary<GameObject, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //getting the camera and setting the indicator prefab to the sprite renderer
        camera = Camera.main;
        spriteRenderer = indicatorPrefab.GetComponent<SpriteRenderer>();

        //getting the size of the sprite
        var bounds = spriteRenderer.bounds;
        spriteHeight = bounds.size.y/2f;
        spriteWidth = bounds.size.x/2f;

        //instantiating the marker
        foreach (var target in Targets)
        {
           var marker = Instantiate(indicatorPrefab);
           marker.SetActive(false);
           targetIndicators.Add(target, marker);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //updating the position of the marker
        foreach (KeyValuePair<GameObject, GameObject> entry in targetIndicators)
        {
            var marker = entry.Key;
            var missile = entry.Value;

            UpdateMissile(marker, missile);
        }
    }

    //function that updates the position and rotation of the marker clamping its position to the edges of the screen. 
    private void UpdateMissile(GameObject target, GameObject indicator)
    {
        var screenPos = camera.WorldToViewportPoint(target.transform.position);
        bool isOffScreen = screenPos.x <=0 || screenPos.x >=1 || screenPos.y <=0 || screenPos.y >=1;
        if (isOffScreen)
        {
            indicator.SetActive(true);
            var spriteSizeInVeiwPort = camera.WorldToViewportPoint(new Vector3(spriteWidth, spriteHeight, 0))
            - camera.WorldToViewportPoint(Vector3.zero);

            screenPos.x = Mathf.Clamp(screenPos.x, spriteSizeInVeiwPort.x, 1 - spriteSizeInVeiwPort.x);
            screenPos.y = Mathf.Clamp(screenPos.y, spriteSizeInVeiwPort.y, 1 - spriteSizeInVeiwPort.y);

            var worldPosition = camera.ViewportToWorldPoint(screenPos);
            worldPosition.z = 0;
            indicator.transform.position = worldPosition;

            Vector3 direction = target.transform.position - indicator.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
            indicator.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else
        {
            indicator.SetActive(false);
        }
    }

}
