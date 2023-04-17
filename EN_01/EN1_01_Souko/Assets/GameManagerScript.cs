using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    // 配列の宣言
    int[] map;

    void PrintArray()
    {
        string debugText = "";
        for(int i = 0; i < map.Length; i++)
        {
            debugText += map[i].ToString() + ", ";
        }
        Debug.Log(debugText);
    }

    int GetPlayerIndex()
    {
        for(int i = 0; i < map.Length; i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
        }
        return -1;
    }

    bool MoveNumber(int number,int moveFrom,int moveTo)
    {
        if(moveTo < 0 || moveTo >= map.Length)
        {
            // 動けない条件を先に書き、リターンする。早期リターン
            return false;
        }
        if (map[moveTo] == 2)
        {
            // どの方向へ移動するかを産出
            int velocity = moveTo - moveFrom;
            // プレイヤーの移動先から、さらに先へ2(箱)を移動させる
            // 箱移動処理。MoveNumberメソッド内でMoveNumberメソッドを
            // 呼び、処理が再帰している。移動可不可をboolで記録
            bool success = MoveNumber(2, moveTo, moveTo + velocity);
            // もし箱が移動失敗したらプレイヤーの移動も失敗
            if (!success) { return false; }
        }
        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        // 配列の実体の作成と初期化
        map = new int[] { 0, 0, 0, 1, 0, 2, 0, 0, 0 };
        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            // メソッド化した処理を使用
            int playerIndex = GetPlayerIndex();

            // 移動処理を関数化
            MoveNumber(1, playerIndex, playerIndex + 1);
            PrintArray();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // メソッド化した処理を使用
            int playerIndex = GetPlayerIndex();

            // 移動処理を関数化
            MoveNumber(1, playerIndex, playerIndex - 1);
            PrintArray();
        }
    }
}
