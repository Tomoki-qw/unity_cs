using System;
using UnityEngine;
using Valve.VR;

//クラス名はファイル名（今回はTrackpad）
public class Trackpad : MonoBehaviour
{

    //パッドのどこを触っているのかを取得するためのTrackPadという関数にSteamVR_Actions.default_TrackPadを固定
    private SteamVR_Action_Vector2 TrackPad = SteamVR_Actions.default_TrackPad;
    //結果の格納用Vector2型関数
    private Vector2 posleft, posright;

    //1フレーム毎に呼び出されるUpdateメゾット
    void Update()
    {

        //結果をGetLastAxisで取得してposleftに格納
        //SteamVR_Input_Sources.機器名（ここは左コントローラ）
        posleft = TrackPad.GetLastAxis(SteamVR_Input_Sources.LeftHand);
        //posleftの中身を確認
        Debug.Log(posleft.x + " " + posleft.y);

        //結果をGetLastAxisで取得してposrightに格納
        //SteamVR_Input_Sources.機器名（ここは右コントローラ）
        posright = TrackPad.GetLastAxis(SteamVR_Input_Sources.RightHand);
        //posleftの中身を確認
        Debug.Log(posright.x + " " + posright.y);

    }
}