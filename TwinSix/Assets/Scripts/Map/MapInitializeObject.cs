using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInitializeObject : MonoBehaviour
{
    [SerializeField] private List<MapInfoScriptableObject> mapInfos = new List<MapInfoScriptableObject>();

    private void Awake()
    {
        for (int i = 0;i < mapInfos.Count;i++)
        {
            for (int j= 0; j < mapInfos[i].beforeMapData.Length;j++)
            {
                mapInfos[i].beforeMapData[j] = mapInfos[i].beforeMapData[j]; // ���I�ɍĐݒ�
            }

            for (int j = 0; j < mapInfos[i].nextMapData.Length;j++)
            {
                mapInfos[i].nextMapData[j] = mapInfos[i].nextMapData[j];
            }

            mapInfos[i].Initialize(); // �R���p�C������̒l���f�t�H���g�l�ɐݒ肷��֐������s
        }

        GameStatus.lockMenber.MapStatusSeter(mapInfos); // �X�e�[�^�X�Ƀ}�b�v����ݒ�
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
