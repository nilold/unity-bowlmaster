using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class ActionMasterTest
{

    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;


    [Test]
    public void T00ScoreMasterTestsSimplePasses()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn()
    {
        ActionMaster actionMaster = new ActionMaster();
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }
}
