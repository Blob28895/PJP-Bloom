using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject bulletMark;
    [SerializeField] Sprite invertedSprite;
    [SerializeField] float crosshairSizeCorrection;
    [SerializeField] float screenShakeDuration = 0.075f;
    [SerializeField] AudioClip[] audioClips;

    private Sprite _baseSprite;
    private SpriteRenderer _crosshairSprite;
    private ScreenShake _screenShake;
    private AudioSource _audioSource;


    void Start()
    {
        _crosshairSprite = GetComponent<SpriteRenderer>();
        _screenShake = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ScreenShake>();
        _baseSprite = _crosshairSprite.sprite;
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            shoot();
            _screenShake.shakeCamera(screenShakeDuration);
            StartCoroutine(invertCrosshair());
            _audioSource.clip = audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
            _audioSource.Play();
            
        }
        //Debug.Log("X: " + Input.GetAxis("Mouse X") + ", Y: " + Input.GetAxis("Mouse Y"));

    }

    void shoot()
    {
        float crosshairSize = _crosshairSprite.transform.localScale.x * crosshairSizeCorrection;
        float shotX = UnityEngine.Random.Range(-1 * crosshairSize / 2, crosshairSize / 2);
        float maxYdeviation = crosshairSize / 2 - Math.Abs(shotX);
		float shotY = UnityEngine.Random.Range(-1 * maxYdeviation, maxYdeviation);
        Vector3 shotPosition = new Vector3(shotX, shotY, 0f);
		GameObject bullet = Instantiate(bulletMark, transform.position + shotPosition, Quaternion.identity);
        Destroy(bullet, 0.4f);
    }

    IEnumerator invertCrosshair()
    {
        _crosshairSprite.sprite = invertedSprite;
        yield return new WaitForSeconds(0.05f);
        _crosshairSprite.sprite = _baseSprite;
    }
}
