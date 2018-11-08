

//-------------------------------------------------------------------------
//CSPart
//Author LiZongFu
//Time 2015.1.15
//-------------------------------------------------------------------------


using UnityEngine;
using System.Collections;

public class SFOrgan
{
    public GameObject       Go;
    public Transform GoTrans;
    public ModelBearing     Type;
    public ISFSpriteAnimation Animation;

    public ISFAvater  IAvater;

    public bool IsHasShoadow = true;
    public ModelStructure Structure;

    //private Vector3         mBounds;
    //private Vector3         mPosition;

    public SFOrgan(GameObject gb, ModelStructure strcutType, ISFAvater avater)
    {
        Go = gb;
        Structure = strcutType;
        if (Go != null) GoTrans = Go.transform;
        IAvater = avater as ISFAvater;
    }

    public int GetModelHeight()
    {
        if (Animation != null)
        {
            CSSpriteBase sprite = Animation.getSprite;
            if (sprite != null)
            {
                //return (int)sprite.EdgeFromTheDistance.z;
                //Debug.Log((int)sprite.EdgeFromTheDistance.z + " , " + (int)sprite.UV.y);
                return (int)sprite.UV.y;
            }
        }
        return 0;
    }

    public Object GetShareMatObj()
    {
        if (Animation != null)
        {
            if (Animation.getSprite.getAtlas != null) return Animation.getSprite.getAtlas;
            return Animation.getSprite.mPicture;
        }
        return null;
    }

    public void SetShareMat(EShareMatType type, Material mat, Vector4 color,Vector4 greyColor)
    {
        if (Animation == null) return;
        if (type == EShareMatType.Balck || type == EShareMatType.Balck_Transparent)
        {
            if (Animation.ShadowData != null)
            {
                Animation.ShadowData.ShadowSprite.SetShader(mat, color, greyColor);
            }
        }
        else
        {
            Animation.getSprite.SetShader(mat, color, greyColor);
        }
    }

    public void SetShaderColor(Material mat,Vector4 color)
    {
        Animation.getSprite.SetShader(mat, color, Animation.getSprite.LastShaderGrey);
    }

    public virtual void Initialization()
    {
       
    }

    public void SetSpecialBoxColliderTbl(float x, float y,float centerY)
    {
        if (Animation != null)Animation.SetSpecialBoxColliderTbl(x, y, centerY);
    }

    public virtual void InitOrgan() 
    {
        Initialization();
        SetLoop(true);
        //ChangeAction(true);
    }

    public virtual void SwitchAction(int curDirection, bool isReset = false, int depth = 0)
    {
        SwitchActionDirection(curDirection, isReset, depth);
    }

    public virtual void SwitchActionDirection(int curDirection, bool isReset = false, int depth = 0) 
    {
        SetPartDepth(depth);

        if(Animation!=null)Animation.UpdateAnimationsNames(curDirection, isReset);
    }

    public void SetShodowDepath()
    {
        if (Animation != null) Animation.SetShodowDepath();
    }

    public virtual void SetPartDepth(int depth)
    {
        if (Animation != null) Animation.setAniGoDepth(depth);
    }

    public virtual void SetAtlas(SFAtlas atlas, int curDirection)
    {
        if (Animation == null) return;
        Animation.setAtlas(atlas, curDirection);
    }

    public virtual void SetSprite(string spriteName)
    {
        if (Animation != null) Animation.SetSprite(spriteName);
    }

    public virtual CSSpriteBase GetCurSprite()
    {
        if (Animation != null) return Animation.getSprite;
        return null;
    }

    public virtual void ClearAtlas()
    {
        if (Animation != null) Animation.ClearAtlas();
    }

    public virtual void SetFPS(int fps)
    {
        if (Animation != null) Animation.setFPS(fps);
    }

    public virtual void SetLoop(bool bl)
    {
        if (Animation != null) Animation.setLoop(bl);
    }

    public virtual void SetCurrentNames(CSMotion motion)
    {
        if (Animation != null) Animation.SetCurrentNames(motion);
    }

    public virtual void SetStopFrameType(CSMotion motion, EActionStopFrameType stopType)
    {
        if (Animation != null) Animation.SetStopFrameType(motion, stopType);
    }

    public void Play(bool isReset) 
    {
        if (Animation != null) Animation.Play(isReset);
    }
    public bool getLoop() 
    {
        if (Animation != null) return Animation.getLoop();
        return false;
    }

    public bool getMediaStop()
    {
        if (Animation != null)
        {
            return Animation.Run;
        }

        return false;
    }

    public bool EndOfAction(string motion,CSMotion m)
    {
        if (Animation != null)
        {
            return Animation.EndOfAction(motion, m);
        }
        return false;
    }
    
    public float getMediaTime()
    {
        if (Animation != null) return Animation.getTime();
        return 0;
    }

    public void ShowShadow(bool isShow)
    {
        if (Animation != null) Animation.ShowShadow(isShow);
    }

    public bool getMediaStop(int montion)
    {
        if (Animation != null)
        {
            return Animation.Run;
        }

        return false;
    }

    public Vector3 getPosition() 
    {
        if (Animation != null) return Animation.getAniPosition();
        return Vector3.zero;
    }

    public virtual void Update()
    {
        if (Animation != null) Animation.Update();
    }

    public virtual void TakeOff()
    {

    }

    public void Destroy()
    {
        if (Animation != null) Animation.Destroy();
    }

    public virtual void OnChangeOneFrame(int frame)
    {

    }
}
