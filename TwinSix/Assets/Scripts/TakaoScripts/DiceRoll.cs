using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DiceRoll : MonoBehaviour
{

    [SerializeField] private AudioSource dicesource;
    [SerializeField] private AudioClip diceSE;

    private Rigidbody rigidbody;
    private bool rollFlg = false;
    private bool stopFlg = true;

    int eyesNum = 0;

    private Vector3 initVec;
    private bool rollOn;
    // Start is called before the first frame update
    void Start()
    {
        rollOn = false;
        initVec = transform.position;
        rigidbody = GetComponent<Rigidbody>();
        dicesource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!rollOn) return;
        DiceRotation();

        if (rollFlg == true)
        {
            ACT_DiceRoll();
        }

        if (rigidbody.velocity == new Vector3(0, 0, 0) && !stopFlg)
        {
            stopFlg = true;
            //サイコロの数字を読み取るメソッド
            DiceEyes();
        }
    }

    public void DiceOn()
    {
        rollOn = true;
    }

    public void DiceReset()
    {
        transform.position = initVec;
        rigidbody.isKinematic = true;
        rollOn = false;
        eyesNum = 0;
    }

    private void DiceEyes()
    {
        Vector3 check_5 = transform.TransformDirection(Vector3.forward);
        Vector3 check_3 = transform.TransformDirection(Vector3.right);
        Vector3 check_6 = transform.TransformDirection(Vector3.up);
        int result = 0;

        if (Mathf.Abs(Mathf.Round(check_5.y)) != 1)
        {
            if (Mathf.Abs(Mathf.Round(check_3.y)) != 1)
            {
                if (Mathf.Round(check_6.y) == 1)
                {
                    result = 6;
                }
                else
                {
                    result = 1;
                }
            }
            else
            {
                if (Mathf.Round(check_3.y) == 1)
                {
                    result = 3;
                }
                else
                {
                    result = 4;
                }
            }
        }
        else
        {
            if (Mathf.Round(check_5.y) == 1)
            {
                result = 5;
            }
            else
            {
                result = 2;
            }
        }
        Debug.Log(result);
        eyesNum = result;
    }

    public int GetDiceEyes()
    {
        return eyesNum;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "TransparentWall")
        {
            rollFlg = false;
            stopFlg = false;
            Debug.Log("触れた");
        }
    }

    private void DiceRotation()
    {
        if (Input.GetMouseButton(0))
        {
            rollFlg = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            dicesource.PlayOneShot(diceSE);
            rigidbody.isKinematic = false;
        }
    }

    private void ACT_DiceRoll()
    {
        transform.Rotate(10, 10, 20);
    }
}
