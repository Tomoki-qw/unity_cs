using System;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

//クラス名はファイル名（今回はTrigerPull）
public class camerareset : MonoBehaviour
{
    //HMDの位置座標格納用
    private Vector3 HMDPosition;
    private Vector3 HMDPositionR;
    private Quaternion HMDRotationR;
    //HMDの回転座標格納用（クォータニオン）
    private Quaternion HMDRotationQ;
    //HMDの回転座標格納用（オイラー角）
    private Vector3 HMDRotation;

    //トリガーがどれだけ押されているのかを取得するためのtriggerpullという関数にSteamVR_Actions.default_TriggerPullを固定
    private SteamVR_Action_Single triggerpull = SteamVR_Actions.default_TriggerPull;
    //結果の格納用floot型関数
    private float pullleft, pullright;
    camerareset()
    {
        HMDPositionR = InputTracking.GetLocalPosition(XRNode.Head);

        HMDRotationR = InputTracking.GetLocalRotation(XRNode.Head);
    }
    //1フレーム毎に呼び出されるUpdateメゾット
    void Update()
    {
        //結果をGetLastAxisで取得してpullleftに格納
        //SteamVR_Input_Sources.機器名（ここは左コントローラ）
        pullright = triggerpull.GetLastAxis(SteamVR_Input_Sources.RightHand);
        //pullleftの中身を確認
        Debug.Log("Left:" + pullright);

        //Head（ヘッドマウンドディスプレイ）の情報を一時保管-----------
        //位置座標を取得
        HMDPosition = InputTracking.GetLocalPosition(XRNode.Head);
        //回転座標をクォータニオンで値を受け取る
        HMDRotationQ = InputTracking.GetLocalRotation(XRNode.Head);
        //取得した値をクォータニオン → オイラー角に変換
        HMDRotation = HMDRotationQ.eulerAngles;
        //--------------------------------------------------------------
        Vector3 HMDPositionR2 = new Vector3(HMDPositionR.x,HMDPositionR.y+2, HMDPositionR.z);
        if(pullright == 1)
        {
            this.transform.position = HMDPositionR2;
            this.transform.rotation = HMDRotationR;
        }
    }
}