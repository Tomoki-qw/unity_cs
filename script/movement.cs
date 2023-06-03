using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;
using System;
public class movement : MonoBehaviour
{
    private Vector3 HMDPosition;
    //HMDの回転座標格納用（クォータニオン）
    private Quaternion HMDRotationQ;
    //HMDの回転座標格納用（オイラー角）
    private Vector3 HMDRotation;
    //HMDの初期位置を定める
    private Vector3 HMDPositionP;
    private Vector3 HMDPositionC;
    private Vector3 Move;
    private float kyori;
    [SerializeField] float hankei = 10;
    private float ko;
    [SerializeField] float op = 1;
    movement()
    {
        HMDPositionC = InputTracking.GetLocalPosition(XRNode.Head);
        HMDPositionP = InputTracking.GetLocalPosition(XRNode.Head);
    }
    //1フレーム毎に呼び出されるUpdateメゾット
    void Update()
    {
        //座標変化を求める
        Move = HMDPositionC - HMDPositionP;
        //変化の距離を求める
        kyori = (float)Math.Sqrt(Math.Pow((float)Move.x, 2) + Math.Pow((float)Move.z, 2));
        //Head（ヘッドマウンドディスプレイ）の情報を一時保管-----------
        //位置座標を取得
        HMDPosition = InputTracking.GetLocalPosition(XRNode.Head);
        //回転座標をクォータニオンで値を受け取る
        HMDRotationQ = InputTracking.GetLocalRotation(XRNode.Head);
        //取得した値をクォータニオン → オイラー角に変換
        HMDRotation = HMDRotationQ.eulerAngles;
        //--------------------------------------------------------------
        
        ko = 3 * 2 * hankei;
        this.transform.Rotate(0.0f, op * (kyori * 360) / ko, 0.0f);
        HMDPositionP = HMDPositionC;
        HMDPositionC = InputTracking.GetLocalPosition(XRNode.Head);

        Debug.Log("HMDP:" + HMDPosition.x + ", " + HMDPosition.y + ", " + HMDPosition.z + "\n" +
            "HMDR:" + HMDRotation.x + ", " + HMDRotation.y + ", " + HMDRotation.z);
    }
}
//past=現フレームの座標
//current=現フレームの座標

//以下繰り返し

//movement= (past zip current).map
//{ case (past, current) => past - current }(座標の変化)
//kyori = movementから算出
//kyori1mにつき9°補正をかける(kyori * 9°)
//past = current
//current = InputTracking.GetLocalPosition(XRNode.Head)