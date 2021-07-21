using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsanteMasso : Interactive
{
    [SerializeField]
    Masso masso;
    public override void CallInteraction()
    {
        masso.CallFallCor();
    }
}
