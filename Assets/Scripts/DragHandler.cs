using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragHandler : MonoBehaviour,IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GameObject prefab;
    GameObject instance;
    public string hitTargetName;

    // Use this for initialization
    void Start () {
        instance = Instantiate(prefab);
        instance.SetActive(false);
        removeScript();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnBeginDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Debug.Log("Begin Drag");
        instance.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit[] hits = Physics.RaycastAll(ray);
        Debug.DrawRay(ray.origin, ray.direction*1000, Color.green);
        Debug.Log(hits);
        if(hits !=null && hits.Length != 0)
        {
            int index = HitTargetIndex(hits);
            Debug.Log(index);
            if (index != -1)
            {
                instance.transform.position = hits[index].point;
                instance.SetActive(true);
            }
            else
            {
                instance.SetActive(false);
            }
        }
    }

    int HitTargetIndex(RaycastHit[] hits)
    {
        for(int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if(hit.collider.gameObject.name == hitTargetName)
            {
                return i;
            }
        }
        return -1;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (instance.activeSelf)
        {
            Instantiate(prefab, instance.transform.position, Quaternion.identity);
            instance.SetActive(false);
        }
    }

    void removeScript()
    {
        Destroy(instance.GetComponent<TurretController>());
    }
}
