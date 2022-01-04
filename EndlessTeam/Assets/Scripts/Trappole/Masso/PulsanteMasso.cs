using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsanteMasso : Interactive
{
    [SerializeField]
    Masso masso = default;
    public override void CallInteraction()
    {
        masso.CallFallCor();
    }
}
