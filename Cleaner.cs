using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleaner : MonoBehaviour
{
    public float destroyTime = 15f;
    private float lastUpdate = 0f;
    public List<GameObject> items;

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastUpdate > destroyTime)
        {
            lastUpdate = Time.time;
            if (items != null)
            {
                foreach (GameObject item in items)
                {
                    Destroy(item);
                    items.Remove(item);
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
            items.Add(collision.gameObject);

    }
}
