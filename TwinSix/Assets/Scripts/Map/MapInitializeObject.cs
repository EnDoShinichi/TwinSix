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
                mapInfos[i].beforeMapData[j] = mapInfos[i].beforeMapData[j]; // 動的に再設定
            }

            for (int j = 0; j < mapInfos[i].nextMapData.Length;j++)
            {
                mapInfos[i].nextMapData[j] = mapInfos[i].nextMapData[j];
            }

            mapInfos[i].Initialize(); // コンパイル直後の値をデフォルト値に設定する関数を実行
        }

        GameStatus.lockMenber.MapStatusSeter(mapInfos); // ステータスにマップ情報を設定
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
