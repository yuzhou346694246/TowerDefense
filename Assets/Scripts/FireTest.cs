using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTest : MonoBehaviour {

    public Rigidbody rb;
    public float demage = 10;
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //AimWithTag("Cube");
	}

    private void AimWithTag(string tag)
    {
        GameObject tObject = GameObject.FindWithTag(tag);
        if(tObject == null)
        {
            Debug.Log("没有找到目标");
            return;
        }
        
        Transform target = tObject.transform;
        /*
         * 空间坐标系的锁定
        Vector3 targetDir = target.position - transform.position;
        float step = 5 * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        // transform.rotation = Quaternion.LookRotation(newDir);
        // rb.rotation = Quaternion.LookRotation(newDir);
        */
        /*
         * 锁定y轴，但是没有过渡过程
        Vector3 targetPostition = new Vector3(target.position.x,
                                        transform.position.y,
                                        target.position.z);
        transform.LookAt(targetPostition);
        */
        //带过渡过程的代码
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
    }

    private void OnCollisionStay(Collision collision)
    {
        GameObject obj = collision.gameObject;
        Debug.Log(obj.tag);
    }
    private void OnTriggerStay(Collider other)
    {
        GameObject obj = other.gameObject;
        Debug.Log("Trigger:"+obj.tag);
        if(obj != null)
        {
            AimWithTag("Cube");
            MoveController mc = obj.GetComponent<MoveController>();
            mc.Demage(demage * Time.deltaTime);
            // Debug.Log(mc);
        }
    }
}
