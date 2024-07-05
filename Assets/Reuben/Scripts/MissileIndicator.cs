using System.Collections.Generic;
using UnityEngine;

public class MissileIndicator : MonoBehaviour
{
    public GameObject indicatorPrefab;
    private Camera camera;
    private SpriteRenderer spriteRenderer;
    private float spriteWidth;
    private float spriteHeight;
    public Transform markerHolder;
    private List<KeyValuePair<GameObject, GameObject>> targetIndicators = new List<KeyValuePair<GameObject, GameObject>>();

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

    private void Start()
    {
        camera = Camera.main;
        spriteRenderer = indicatorPrefab.GetComponent<SpriteRenderer>();
        var bounds = spriteRenderer.bounds;
        spriteHeight = bounds.size.y / 2f;
        spriteWidth = bounds.size.x / 2f;
    }

    private void Update()
    {
        foreach (var pair in new List<KeyValuePair<GameObject, GameObject>>(targetIndicators))
        {
            var marker = pair.Value;
            var missile = pair.Key;

            if (marker == null || missile == null)
            {
                targetIndicators.Remove(pair);
                continue;
            }

            UpdateMissile(marker, missile);
        }
    }

    void UpdateMissile(GameObject target, GameObject indicator)
    {
        if (target == null || indicator == null)
        {
            return;
        }

        var screenPos = camera.WorldToViewportPoint(target.transform.position);
        bool isOffScreen = screenPos.x <= 0 || screenPos.x >= 1 || screenPos.y <= 0 || screenPos.y >= 1;

        if (isOffScreen)
        {
            indicator.SetActive(true);
            Vector3 direction = target.transform.position - camera.WorldToViewportPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            Vector3 clampedPosition = camera.WorldToViewportPoint(target.transform.position);
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, 0.05f, 0.95f);
            clampedPosition.y = Mathf.Clamp(clampedPosition.y, 0.05f, 0.95f);
            indicator.transform.position = camera.ViewportToWorldPoint(clampedPosition);
            indicator.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        else
        {
            indicator.SetActive(false);
        }
    }

    private void OnMissileSpawned(GameObject missile)
    {
        var marker = Instantiate(indicatorPrefab, markerHolder);
        marker.SetActive(false);
        targetIndicators.Add(new KeyValuePair<GameObject, GameObject>(missile, marker));

        missile.SetActive(true);
    }

    private void OnMissileDestroyed(GameObject missile)
    {
        var pairToRemove = targetIndicators.Find(pair => pair.Key == missile);
        if (pairToRemove.Key != null)
        {
            targetIndicators.Remove(pairToRemove);
            Destroy(pairToRemove.Value);
        }
    }
}