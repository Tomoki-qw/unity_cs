using System;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

//クラス名はファイル名（今回はTrigerPull）
public class walk : MonoBehaviour
{
    //HMDの位置座標格納用
    private Vector3 HMDPosition;
    //HMDの回転座標格納用（クォータニオン）
    private Quaternion HMDRotationQ;
    //HMDの回転座標格納用（オイラー角）
    private Vector3 HMDRotation;

    //トリガーがどれだけ押されているのかを取得するためのtriggerpullという関数にSteamVR_Actions.default_TriggerPullを固定
    private SteamVR_Action_Single triggerpull = SteamVR_Actions.default_TriggerPull;
    //結果の格納用floot型関数
    private float pullleft, pullright;

    //1フレーム毎に呼び出されるUpdateメゾット
    void Update()
    {
        //結果をGetLastAxisで取得してpullleftに格納
        //SteamVR_Input_Sources.機器名（ここは左コントローラ）
        pullleft = triggerpull.GetLastAxis(SteamVR_Input_Sources.LeftHand);
        //pullleftの中身を確認
        Debug.Log("Left:" + pullleft);

        //Head（ヘッドマウンドディスプレイ）の情報を一時保管-----------
        //位置座標を取得
        HMDPosition = InputTracking.GetLocalPosition(XRNode.Head);
        //回転座標をクォータニオンで値を受け取る
        HMDRotationQ = InputTracking.GetLocalRotation(XRNode.Head);
        //取得した値をクォータニオン → オイラー角に変換
        HMDRotation = HMDRotationQ.eulerAngles;
        //--------------------------------------------------------------

        Debug.Log("Left:" + pullleft);

        Debug.Log("HMDP:" + HMDPosition.x + ", " + HMDPosition.y + ", " + HMDPosition.z + "\n" +
            "HMDR:" + HMDRotation.x + ", " + HMDRotation.y + ", " + HMDRotation.z);

        //トリガーの情報をVector3で入力
        Vector3 changePosition = new Vector3(pullleft, 0, 0);
        //HMDのY軸の角度取得して90度ずらす
        Vector3 changeRotation = new Vector3(0, HMDRotation.y - 90, 0);
        //[CameraRig]の位置変更
        this.transform.position += this.transform.rotation * (Quaternion.Euler(changeRotation) * changePosition / 50);
    }
}