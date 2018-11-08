using UnityEngine;
using System.Collections;

public class testLogic : UILogic
{
    public void Initialize(testView view)
    {
        ItemSource = PomeloGameManager.Instance.tModel;

        this.SetBinding<int>(testModel.COUNT, view.UpdateCount);
    }
}
