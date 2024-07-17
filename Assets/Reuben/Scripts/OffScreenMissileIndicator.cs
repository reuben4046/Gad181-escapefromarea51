using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;

public class OffScreenMissileIndicator : MonoBehaviour
{
    //marker that shows where the missiles are
    public GameObject indicatorPrefab; 

    //sprite render for the marker 
    private SpriteRenderer spriteRenderer;
    private float spriteWidth;
    private float spriteHeight;

    //reference to the camera
    private new Camera camera;

    //reference to the marker holder (organisation purposes)
    public Transform indicatorHolder;

    //dictionary that stores the marker and the missile
    private Dictionary<Missile, GameObject> targetIndicators = new Dictionary<Missile, GameObject>();

    private void OnEnable()
    {
        EventSystemRP.OnMissileSpawned += OnMissileSpawned;
        EventSystemRP.OnMissileDestroyed += OnMissileDestroyed;
    }

    private void OnDisable()
    {
        EventSystemRP.OnMissileSpawned -= OnMissileSpawned;
        EventSystemRP.OnMissileDestroyed -= OnMissileDestroyed;
    }
    
    private void OnMissileSpawned(Missile missile)
    {
        var indicator = Instantiate(indicatorPrefab, indicatorHolder);
        indicator.SetActive(false);
        targetIndicators.Add(missile, indicator);
    }

    private void OnMissileDestroyed(Missile missile)
    {
        var indicator = targetIndicators[missile];

        targetIndicators.Remove(missile);

        Destroy(indicator.gameObject);
        Destroy(missile);
    }

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
    }       
        
    // Update is called once per frame
    void Update()
    {
        // updating the position of the marker
        foreach (KeyValuePair<Missile, GameObject> entry in targetIndicators)
        {
            var missile = entry.Key;
            var indicator = entry.Value;

            UpdateMissile(missile, indicator);
        }
    }

    //function that updates the position and rotation of the marker clamping its position to the edges of the screen. 
    private void UpdateMissile(Missile missile, GameObject indicator)
    {
        var screenPos = camera.WorldToViewportPoint(missile.transform.position);
        bool offScreen = screenPos.x <=0 || screenPos.x >=1 || screenPos.y <=0 || screenPos.y >=1;
        if (offScreen)
        {
            if (missile == null)
            {
                indicator.SetActive(false);
                return;
            } 
            indicator.SetActive(true);
            var spriteSizeInVeiwPort = camera.WorldToViewportPoint(new Vector3(spriteWidth, spriteHeight, 0))
            - camera.WorldToViewportPoint(Vector3.zero);

            screenPos.x = Mathf.Clamp(screenPos.x, spriteSizeInVeiwPort.x, 1 - spriteSizeInVeiwPort.x);
            screenPos.y = Mathf.Clamp(screenPos.y, spriteSizeInVeiwPort.y, 1 - spriteSizeInVeiwPort.y);

            var worldPosition = camera.ViewportToWorldPoint(screenPos);
            worldPosition.z = 0;
            indicator.transform.position = worldPosition;

            Vector3 direction = missile.transform.position - indicator.transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
            indicator.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else
        {
            indicator.SetActive(false);
        }
    }

}
