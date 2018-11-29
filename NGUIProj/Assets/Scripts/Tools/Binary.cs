using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;
public class Binary : MonoBehaviour
{

    void Start()
    {
        string filepath = Application.dataPath + @"/StreamingAssets/binary.txt";

        if (File.Exists(filepath))
        {
            FileStream fs = new FileStream(filepath, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);

            int index = 0;
            //将二进制字节流全部读取在这个byte数组当中
            //ReadBytes传递的参数是一个长度，也就是流的长度
            byte[] tempall = br.ReadBytes((int)fs.Length);

            //开始解析这个字节数组
            while (true)
            {
                //当超过流长度，跳出循环
                if (index >= tempall.Length)
                {
                    break;
                }

                //得到第一个byte 也就是得到字符串的长度
                int scenelength = tempall[index];
                byte[] sceneName = new byte[scenelength];
                index += 1;
                //根据长度拷贝出对应长度的字节数组
                System.Array.Copy(tempall, index, sceneName, 0, sceneName.Length);
                //然后把字节数组对应转换成字符串
                string sname = System.Text.Encoding.Default.GetString(sceneName);

                //这里和上面原理一样就不赘述
                int objectLength = tempall[index + sceneName.Length];
                byte[] objectName = new byte[objectLength];

                index += sceneName.Length + 1;
                System.Array.Copy(tempall, index, objectName, 0, objectName.Length);
                string oname = System.Text.Encoding.Default.GetString(objectName);

                //下面就是拿short 每一个short的长度是2字节。

                index += objectName.Length;
                byte[] posx = new byte[2];
                System.Array.Copy(tempall, index, posx, 0, posx.Length);
                //取得对应的数值 然后 除以100 就是float拉。	
                float x = System.BitConverter.ToInt16(posx, 0) / 100.0f;

                //下面都差不多
                index += posx.Length;
                byte[] posy = new byte[2];
                System.Array.Copy(tempall, index, posy, 0, posy.Length);
                float y = System.BitConverter.ToInt16(posy, 0) / 100.0f;

                index += posy.Length;
                byte[] posz = new byte[2];
                System.Array.Copy(tempall, index, posz, 0, posz.Length);
                float z = System.BitConverter.ToInt16(posz, 0) / 100.0f;

                index += posz.Length;
                byte[] rotx = new byte[2];
                System.Array.Copy(tempall, index, rotx, 0, rotx.Length);
                float rx = System.BitConverter.ToInt16(rotx, 0) / 100.0f;

                index += rotx.Length;
                byte[] roty = new byte[2];
                System.Array.Copy(tempall, index, roty, 0, roty.Length);
                float ry = System.BitConverter.ToInt16(roty, 0) / 100.0f;

                index += roty.Length;
                byte[] rotz = new byte[2];
                System.Array.Copy(tempall, index, rotz, 0, rotz.Length);
                float rz = System.BitConverter.ToInt16(rotz, 0) / 100.0f;

                index += rotz.Length;
                byte[] scax = new byte[2];
                System.Array.Copy(tempall, index, scax, 0, scax.Length);
                float sx = System.BitConverter.ToInt16(scax, 0) / 100.0f;

                index += scax.Length;
                byte[] scay = new byte[2];
                System.Array.Copy(tempall, index, scay, 0, scay.Length);
                float sy = System.BitConverter.ToInt16(scay, 0) / 100.0f;

                index += scay.Length;
                byte[] scaz = new byte[2];
                System.Array.Copy(tempall, index, scaz, 0, scaz.Length);
                float sz = System.BitConverter.ToInt16(scaz, 0) / 100.0f;

                index += scaz.Length;

                if (sname.Equals("Assets/test.unity"))
                {
                    //最后在这里把场景生成出来
                    string asset = oname;
                    Vector3 pos = new Vector3(x, y, z);
                    Vector3 rot = new Vector3(rx, ry, rz);
                    Vector3 sca = new Vector3(sx, sy, sz);
                    GameObject ob = (GameObject)Instantiate(Resources.Load(asset), pos, Quaternion.Euler(rot));
                    ob.transform.localScale = sca;
                }

            }
        }
        AstarPath.active.Scan();
    }

    // Update is called once per frame
    void Update()
    {

    }
}