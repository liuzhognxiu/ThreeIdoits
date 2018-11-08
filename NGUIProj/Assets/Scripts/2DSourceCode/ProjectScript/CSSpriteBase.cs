using UnityEngine;
using System.Collections;
using System;

public interface ISFSprite
{
    Vector2 getUV { get; set; }
    void SetShader(Material mat, Vector4 color, Vector4 greyColor);
    SFAtlas getAtlas { get; set; }
}

public class CSSpriteBase : MonoBehaviour, ISFSprite
{
    public virtual UnityEngine.Color LastShaderGrey
    {
        get { return Color.white; }
        set {  }
    }
    public virtual CSSpriteBase getShadowSprite { get { return null; } set { } }
    public virtual Vector2 getUV
    {
        get { return UV; }
        set { UV = value; }
    }
    public virtual void SetShader(Material mat, Vector4 color, Vector4 greyColor){}
    public virtual SFAtlas getAtlas { get { return null; } set { } }

    public string mSpriteName = string.Empty;

    protected bool mIsInit = false;

    public Texture mPicture;

    public Material mMaterial;

    public Vector2 mSize = Vector2.zero;
    
    public Vector2 UV = Vector2.zero;

    protected bool misUVChange = false;
    public bool IsUVChange
    {
        get { return misUVChange; }
        set { misUVChange = value; }
    }
    protected static float angle = 48.78f;

    public bool isSlant;
    /// <summary>
    /// X 加 或 减
    /// </summary>
    public bool isNegative; // 仅仅使用与影子，模型只要五个方向时使用

    protected MeshFilter mMeshFilter;

    protected MeshRenderer mMeshRenderer;

    protected Transform mScaleTrans;

    protected Vector3 mScaleTransVec;

    protected bool mIsScalTrans = false;

    protected Texture mTexture = null;

    protected string mInitSetSpriteName;

    protected Transform mCacheTrans;
    public UnityEngine.Transform CacheTrans
    {
        get { return mCacheTrans; }
        set { mCacheTrans = value; }
    }
    protected virtual void Awake()
    {
        mCacheTrans = transform;
    }

    protected virtual void Start()
    {
    }

    public virtual string SpriteName { get; set; }
    public virtual Material MainMaterial { get; set; }

    public string InitSetSpriteName
    {
        get { return mInitSetSpriteName; }
        set
        {
            if (mInitSetSpriteName == value)
                return;
            mInitSetSpriteName = value;
            if (mIsInit)
            {
                SpriteName = value;
            }
        }
    }

    public virtual Texture Texture
    {
        get
        {
            Material mat = MainMaterial;
            if (mat == null) return null;
            return MainMaterial.mainTexture;
        }
        set
        {
            Material mat = MainMaterial;
            if (mat != null)
            {
                mat.mainTexture = value;
            }

            mTexture = value;
            if (mat.mainTexture != null)
            {
                this.mSize.y = (float)mat.mainTexture.height;
                this.mSize.x = (float)mat.mainTexture.width;
            }
        }
    }

    public void ResetMesh()
    {
        mMaterial = null;
        if (mMeshRenderer != null)
            mMeshRenderer.sharedMaterial = null;
        if (mMeshRenderer != null)
        {
            //Destroy(mMeshRenderer);
            //mMeshRenderer = null;
        }
        if (mMeshFilter != null)
        {
            mMeshFilter.mesh.Clear();
            //mMeshFilter
            //Destroy(mMeshFilter);
            //mMeshFilter = null;
        }
    }

    public void ApplyScaleTrans(Transform trans, Vector3 vec)
    {
        mScaleTrans = trans;
        mIsScalTrans = true;
        mScaleTransVec = vec;
    }

    protected void SetPosition()
    {
        //if (mCacheTrans != null)
        //{
        //    float x = mCacheTrans.localPosition.x;
        //    float y = mCacheTrans.localPosition.y;
        //    Vector3 vec3 = Vector3.zero;
        //    vec3.x = x;
        //    vec3.y = y;
        //    mCacheTrans.localPosition = vec3;
        //}
    }

    protected void setMeshVertices(Vector2 Uv, Color[] meshColorList)
    {
        Vector3 vec3 = Vector3.zero;
        vec3.x = -UV.x / 2f;
        vec3.y = -Uv.y / 2f;
        SFMisc.MeshVertices[0] = vec3;
        vec3.x = -UV.x / 2f;
        vec3.y = Uv.y / 2f;
        SFMisc.MeshVertices[1] = vec3;
        vec3.x = UV.x / 2f;
        vec3.y = -Uv.y / 2f;
        SFMisc.MeshVertices[2] = vec3;
        vec3.x = UV.x / 2f;
        vec3.y = Uv.y / 2f;
        SFMisc.MeshVertices[3] = vec3;
        mMeshFilter.mesh.vertices = SFMisc.MeshVertices;
        mMeshFilter.mesh.normals = SFMisc.MeshVertices;
        mMeshFilter.mesh.colors = meshColorList;
    }

    protected void setMeshVertices(int paddingLeft, int paddingRight, int paddingTop, int paddingBottom, int width, int height, Color[] meshColorList)
    {
        float originalWidth = paddingLeft + paddingRight + width;
        float originalHeight = paddingTop + paddingBottom + height;
        Vector3 vec3 = Vector3.zero;
        Vector2 vec22 = Vector2.zero;
        if (isSlant)
        {
            float bevelEdge = (float)Math.Sin(angle * Math.PI / 180) * (paddingBottom + height);

            float OffsetX = ((float)Math.Sin(angle * Math.PI / 180) * bevelEdge) - 283;
            float OffsetY = ((float)Math.Cos(angle * Math.PI / 180) * bevelEdge) - 248;

            float px1 = -(originalWidth / 2f - paddingLeft);
            float py1 = (originalHeight / 2f - paddingTop);

            if (isNegative)
            {
                px1 = px1 - OffsetX;
            }
            else
            {
                px1 = px1 + OffsetX;
            }
            py1 = py1 - OffsetY;
            vec3.x = px1;
            vec3.y = py1 + vec22.y;
            SFMisc.MeshVertices[1] = vec3;

            float px3 = (originalWidth / 2f - paddingRight);
            float py3 = (originalHeight / 2f - paddingTop);
            if (isNegative)
            {
                px3 = px3 - OffsetX;
            }
            else
            {
                px3 = px3 + OffsetX;
            }
            py3 = py3 - OffsetY;
            vec3.x = px3;
            vec3.y = py3 + vec22.y;
            SFMisc.MeshVertices[3] = vec3;

            bevelEdge = (float)Math.Sin(angle * Math.PI / 180) * paddingBottom;

            OffsetX = (float)Math.Sin(angle * Math.PI / 180) * bevelEdge - 283;
            OffsetY = (float)Math.Cos(angle * Math.PI / 180) * bevelEdge - 248;

            float px0 = -(originalWidth / 2f - paddingLeft);
            float py0 = -(originalHeight / 2f - paddingBottom);
            if (isNegative)
            {
                px0 = px0 - OffsetX;
            }
            else
            {
                px0 = px0 + OffsetX;
            }
            py0 = py0 - OffsetY;
            vec3.x = px0;
            vec3.y = py0 + vec22.y;
            SFMisc.MeshVertices[0] = vec3;

            float px2 = (originalWidth / 2f - paddingRight);
            float py2 = -(originalHeight / 2f - paddingBottom);
            if (isNegative)
            {
                px2 = px2 - OffsetX;
            }
            else
            {
                px2 = px2 + OffsetX;
            }
            py2 = py2 - OffsetY;
            vec3.x = px2;
            vec3.y = py2 + vec22.y;
            SFMisc.MeshVertices[2] = vec3;
        }
        else
        {
            float px0 = -(originalWidth / 2f - paddingLeft);
            float py0 = -(originalHeight / 2f - paddingBottom);
            vec3.x = px0;
            vec3.y = py0;
            SFMisc.MeshVertices[0] = vec3;
            float px1 = -(originalWidth / 2f - paddingLeft);
            float py1 = (originalHeight / 2f - paddingTop);
            vec3.x = px1;
            vec3.y = py1;
            SFMisc.MeshVertices[1] = vec3;

            float px2 = (originalWidth / 2f - paddingRight);
            float py2 = -(originalHeight / 2f - paddingBottom);
            vec3.x = px2;
            vec3.y = py2;
            SFMisc.MeshVertices[2] = vec3;

            float px3 = (originalWidth / 2f - paddingRight);
            float py3 = (originalHeight / 2f - paddingTop);
            vec3.x = px3;
            vec3.y = py3;
            SFMisc.MeshVertices[3] = vec3;
        }

        mMeshFilter.mesh.vertices = SFMisc.MeshVertices;
        mMeshFilter.mesh.normals = SFMisc.MeshVertices;
        mMeshFilter.mesh.colors = meshColorList;
    }

    public Vector3[] GetMeshVertices()
    {
        if (mMeshFilter == null) return null;
        return mMeshFilter.mesh.vertices;
    }


    protected void setMeshTriangles()
    {
        SFMisc.array2[0] = 0;
        SFMisc.array2[1] = 1;
        SFMisc.array2[2] = 2;
        SFMisc.array2[3] = 1;
        SFMisc.array2[4] = 3;
        SFMisc.array2[5] = 2;
        mMeshFilter.mesh.triangles = SFMisc.array2;
    }
    protected void setMeshUV()
    {
        Vector2 vec2 = Vector2.zero;
        vec2.x = 0;
        vec2.y = 0;
        SFMisc.uvs[0] = vec2;

        vec2.x = 0;
        vec2.y = 1;
        SFMisc.uvs[1] = vec2;

        vec2.x = 1;
        vec2.y = 0;
        SFMisc.uvs[2] = vec2;

        vec2.x = 1;
        vec2.y = 1;
        SFMisc.uvs[3] = vec2;


        mMeshFilter.mesh.uv = SFMisc.uvs;
    }

    protected void setMeshUV(int x, int width, int y, int height)
    {
        Vector2 size;
        size.x = this.mSize.x;
        size.y = this.mSize.y;
        Vector2 mUV_One;
        mUV_One.x = x / size.x;
        mUV_One.y = (size.y - (float)(y + height)) / size.y;

        Vector2 mUV_Two;
        mUV_Two.x = x / size.x; ;
        mUV_Two.y = (size.y - (float)y) / size.y;

        Vector2 mUV_Three;
        mUV_Three.x = (float)(x + width) / size.x;
        mUV_Three.y = (size.y - (float)(y + height)) / size.y;

        Vector2 mUV_Four;
        mUV_Four.x = (float)(x + width) / size.x;
        mUV_Four.y = (size.y - (float)y) / size.y;

        if (mMeshFilter != null)
        {
            SFMisc.uvs[0] = mUV_One;
            SFMisc.uvs[1] = mUV_Two;
            SFMisc.uvs[2] = mUV_Three;
            SFMisc.uvs[3] = mUV_Four;

            mMeshFilter.mesh.uv = SFMisc.uvs;
            //mMeshFilter.mesh.RecalculateNormals();
        }
    }

    protected void getComponent()
    {
        if (Texture == null) return;

        if (mMeshFilter == null)
        {
            mMeshFilter = gameObject.GetComponent<MeshFilter>();

            if (mMeshFilter == null)
            {
                mMeshFilter = gameObject.AddComponent<MeshFilter>();
            }
        }

        if (mMeshRenderer == null)
        {
            mMeshRenderer = gameObject.GetComponent<MeshRenderer>();
            if (mMeshRenderer == null)
            {
                mMeshRenderer = gameObject.AddComponent<MeshRenderer>();
            }
        }
        mMeshRenderer.sharedMaterial = this.MainMaterial;
        //if( mMeshRenderer.sharedMaterial == null)
        //{
        //    mMeshRenderer.sharedMaterial = this.Material;
        //}
    }


    public static void DrawRect(float sx, float sy, float width, float height, float PixelRatio)
    {
        Vector2[] point = new Vector2[4];

        point[0] = new Vector2(sx, sy);
        point[1] = new Vector2(sx, height);
        point[2] = new Vector2(width, height);
        point[3] = new Vector2(width, sy);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(point[0] * PixelRatio, point[1] * PixelRatio);
        Gizmos.DrawLine(point[1] * PixelRatio, point[2] * PixelRatio);
        Gizmos.DrawLine(point[2] * PixelRatio, point[3] * PixelRatio);
        Gizmos.DrawLine(point[3] * PixelRatio, point[0] * PixelRatio);
    }

    public static void DrawRect2(float sx, float sy, float width, float height)
    {
        Vector2[] point = new Vector2[4];

        point[0] = new Vector2(sx, sy);
        point[1] = new Vector2(sx, height);
        point[2] = new Vector2(width, height);
        point[3] = new Vector2(width, sy);

        Gizmos.DrawLine(point[0], point[1]);
        Gizmos.DrawLine(point[1], point[2]);
        Gizmos.DrawLine(point[2], point[3]);
        Gizmos.DrawLine(point[3], point[0]);
    }

    public virtual void Destroy()
    {

    }
}

