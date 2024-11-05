using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeShift : MonoBehaviour
{
    [Range(0.01f, 1f)]
    public float shiftSpeed;
    private bool isOn = true;
    private float minScale = 0f;
    private float maxScale;
    private float calcValue;
    private float scale;
    private float bladeX;
    private float bladeZ;
    private float newShiftSpeed;
    public GameObject blade;
    public AudioSource sounds;
    public AudioClip onSound;
    public AudioClip offSound;

    // Start is called before the first frame update
    void Start()
    {
        shiftSpeed = 0.5f;
        bladeX = transform.localScale.x;
        bladeZ = transform.localScale.z;
        maxScale = transform.localScale.y;
        scale = maxScale;
        calcValue = maxScale / shiftSpeed;
        isOn = true;
        sounds = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isOn)
            {
                calcValue = -Mathf.Abs(calcValue);
                sounds.clip = offSound;
                sounds.Play();
            }
            else if (!isOn)
            {
                calcValue = Mathf.Abs(calcValue);
                sounds.clip = onSound;
                sounds.Play();
            }
             
        }

        scale += calcValue * Time.deltaTime;
        scale = Mathf.Clamp(scale, minScale, maxScale);
        transform.localScale = new Vector3(bladeX, scale, bladeZ);
        isOn = scale > 0;

        if (isOn && !blade.activeSelf)
        {
            blade.SetActive(true);
        } else if (!isOn && blade.activeSelf)
        {
            blade.SetActive(false);
        }
    }
}
