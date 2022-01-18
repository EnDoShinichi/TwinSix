using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoll : MonoBehaviour
{
    private Rigidbody rigidbody;
    private bool rollFlg = false;
    private bool stopFlg = true;

    int eyesNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        DiceRotation();

        if (rollFlg == true)
        {
            ACT_DiceRoll();
        }

        if (rigidbody.velocity == new Vector3(0, 0, 0) && !stopFlg)
        {
            stopFlg = true;
            //�T�C�R���̐�����ǂݎ�郁�\�b�h
            DiceEyes();
        }
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
            Debug.Log("�G�ꂽ");
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
            rigidbody.isKinematic = false;
        }
    }

    private void ACT_DiceRoll()
    {
        transform.Rotate(10, 10, 20);
    }
}