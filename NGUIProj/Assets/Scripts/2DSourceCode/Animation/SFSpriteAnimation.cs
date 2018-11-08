
//Author LiZongFu
//date 2015.12.15

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public interface ISFSpriteAnimation
{
    IBaseFrame getFrameAni { get; set; }
    CSSpriteBase getSprite { get; set; }
    ShadowData ShadowData { get; set; }
    void SetSpecialBoxColliderTbl(float x, float y, float centerY);
    void UpdateAnimationsNames(int curDirection, bool isReset = false);
    void SetShodowDepath();
    void setAniGoDepth(int depth);
    void setAtlas(SFAtlas atlas, int curDirection);
    void SetSprite(string spriteName);
    void ClearAtlas();
    void setFPS(int fps);
    void setLoop(bool bl);
    void SetCurrentNames(CSMotion motion);
    void SetStopFrameType(CSMotion motion, EActionStopFrameType stopType);
    bool getLoop();
    void Play(bool isReset = true);
    bool Run { get; }
    bool EndOfAction(string motion, CSMotion m);
    float getTime();
    void ShowShadow(bool isShow);
    Vector3 getAniPosition();
    void Update();
    void Destroy();
}