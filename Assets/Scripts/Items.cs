using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Items : MonoBehaviour
{
    [SerializeField] private Image itemImageHolder;
    private List<Color> items = new List<Color>();
    private int activeItemIndex = -1;
    [SerializeField] private TMP_Text messageField;
    private float cooldownCounter = 0f;
    public static event Action<int> OnRotatePickup;
    public static event Action<int> OnSpeedPickup;
    public static event Action<float> OncooldownPickup;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ShowMessage("Welcome to Space 4 8. \n Move your ship with the arrows or WASD. \n Shoot with SPACE. \n Gather pickups and cycle with 'Left CTRL'.  \n Use pickups with 'E'.", 5f));
    }

    // Update is called once per frame
    void Update()
    {
        CycleItems();
        UseItems();
    }

    IEnumerator ShowMessage(string message, float duration)
    {
        messageField.enabled = true;
        messageField.text = message;
        yield return new WaitForSeconds(duration);
        messageField.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            PickUpItem(other.gameObject);
        }
    }

    public void PickUpItem(GameObject item)
    {
        Color color = item.gameObject.GetComponent<Renderer>().material.color;

        Destroy(item);
        items.Add(color);

        activeItemIndex = items.Count - 1;

        itemImageHolder.color = items[activeItemIndex];
        itemImageHolder.enabled = true;
    }

    public void CycleItems()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            if (items.Count > 0)
            {
                if (activeItemIndex < items.Count - 1)
                {
                    activeItemIndex++;
                }
                else
                {
                    activeItemIndex = 0;
                }
                itemImageHolder.color = items[activeItemIndex];
            }
            else
            {
                itemImageHolder.color = Color.white;
                activeItemIndex = -1;
                itemImageHolder.enabled = false;
            }
        }
    }

    public void UseItems()
    {
        if (Input.GetKeyDown(KeyCode.E) && items.Count > 0 && activeItemIndex != -1)
        {
            if (items[activeItemIndex] == Color.blue)
            {
                StartCoroutine(ShowMessage(" + Move Speed", 3f));
                OnSpeedPickup.Invoke(5); // += 5;
            }
            else if (items[activeItemIndex] == Color.red)
            {
                StartCoroutine(ShowMessage(" + Fire Rate", 3f));
                OncooldownPickup.Invoke(0.1f); // -= 0.1f;
            }
            else if (items[activeItemIndex] == Color.green)
            {
                StartCoroutine(ShowMessage(" + Rotation Speed", 3f));
                OnRotatePickup.Invoke(10); // += 10;
            }

            items.RemoveAt(activeItemIndex);
            if (activeItemIndex > 0)
            {
                activeItemIndex--;
                itemImageHolder.color = items[activeItemIndex];
            }
            else if (items.Count == 0)
            {
                itemImageHolder.color = Color.white;
                activeItemIndex = -1;
                itemImageHolder.enabled = false;
            }
        }
    }
}
