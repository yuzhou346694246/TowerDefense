using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

    public Transform[] targets;
    public float speed = 10f;
    private int currentIndex = 0;
    public float maxHealth = 100f;
    public float currentHealth = 0f;
    public RectTransform rectTransform;
    // Use this for initialization
	void Start () {
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 currentTargetPosition = targets[currentIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, currentTargetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, currentTargetPosition)<0.1)
        {
            currentIndex++;
            if (currentIndex >= targets.Length)
            {
                Destroy(gameObject);
            }
        }
	}

    public void Demage(float delta)
    {
        currentHealth -= delta;
        rectTransform.localScale = new Vector3(currentHealth / maxHealth, 1f, 1f);
        if(currentHealth <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
