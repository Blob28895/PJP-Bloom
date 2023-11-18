using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject bulletMark;

    private SpriteRenderer _crosshairSprite;


    void Start()
    {
        _crosshairSprite = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            shoot();
        }

        //Debug.Log("X: " + Input.GetAxis("Mouse X") + ", Y: " + Input.GetAxis("Mouse Y"));

    }

    void shoot()
    {
        float crosshairSize = _crosshairSprite.transform.localScale.x * 0.9f;
        float shotX = UnityEngine.Random.Range(-1 * crosshairSize / 2, crosshairSize / 2);
        float maxYdeviation = crosshairSize / 2 - Math.Abs(shotX);
		float shotY = UnityEngine.Random.Range(-1 * maxYdeviation, maxYdeviation);
        Vector3 shotPosition = new Vector3(shotX, shotY, 0f);
		GameObject bullet = Instantiate(bulletMark, transform.position + shotPosition, Quaternion.identity);
        Destroy(bullet, 0.4f);
    }
}
