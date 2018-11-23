using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurretController : MonoBehaviour {

    public Transform turret;// 炮台
    public float demage = 10f;
    public Transform laserOrigin;//激光发射点
    public LineRenderer lineRenderer;
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void AimWithTag(string tag)
    {
        GameObject tObject = GameObject.FindWithTag(tag);
        if (tObject == null)
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
         * 
         */

        Vector3 targetPostition = new Vector3(target.position.x,
                                        turret.position.y,
                                        target.position.z);
        // transform.LookAt(targetPostition);
        turret.LookAt(targetPostition);

        //带过渡过程的代码
        /*
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
      
        turret.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5);
        */
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject obj = other.gameObject;
        ////Debug.Log("Trigger:" + obj.tag);
        if (obj != null)
        {
            AimWithTag("Cube");
            MoveController mc = obj.GetComponent<MoveController>();
            mc.Demage(demage * Time.deltaTime);
            FireLaser(obj.transform.position);
            // Debug.Log(mc);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FireLaser(laserOrigin.position);
    }

    private void FireLaser(Vector3 endPoint)
    {
        lineRenderer.SetPosition(0, laserOrigin.position);
        lineRenderer.SetPosition(1, endPoint);
    }
}
