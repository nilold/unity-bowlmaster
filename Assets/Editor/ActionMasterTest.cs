using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections.Generic;

[TestFixture]
public class ActionMasterTest
{
    private ActionMaster actionMaster;
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;

    [SetUp]
    public void Setup()
    {
        actionMaster = new ActionMaster();
    }


    [Test]
    public void T00ScoreMasterTestsSimplePasses()
    {
        Assert.AreEqual(1, 1);
    }

    [Test]
    public void T01OneStrikeReturnsEndTurn()
    {
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
    }

    [Test]
    public void T02Bowl8ReturnTidy()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(8));
    }

    [Test]
    public void T03TwoBowlsReturnEndTurn()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(3));
        Assert.AreEqual(endTurn, actionMaster.Bowl(4));
    }

    [Test]
    public void T04Bowl28SpareReturnEndturn()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(2));
        Assert.AreEqual(endTurn, actionMaster.Bowl(8));
    }

    [Test]
    public void T05AnyPinsOnLastBowlReturnEndGame()
    {
        for (int i = 0; i < 18; i++)
        {
            actionMaster.Bowl(3);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(endGame, actionMaster.Bowl(2));
    }

    [Test]
    public void T06StrikeOn19thBowlReturnReset()
    {
        for (int i = 0; i < 18; i++)
        {
            actionMaster.Bowl(2);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T07StrikeOn20thBowlReturnReset()
    {
        for (int i = 0; i < 19; i++)
        {
            actionMaster.Bowl(2);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
    }

    [Test]
    public void T08SpareOnLastFrameAwarded()
    {
        for (int i = 0; i < 18; i++)
        {
            actionMaster.Bowl(2);
        }
        Assert.AreEqual(tidy, actionMaster.Bowl(5));
        Assert.AreEqual(reset, actionMaster.Bowl(5));
    }

    [Test]
    public void T09NoStrikeOnLastFrameEndsGameOn20thBowl()
    {
        for (int i = 0; i < 19; i++)
        {
            actionMaster.Bowl(2);
        }
        Assert.AreEqual(endGame, actionMaster.Bowl(3));
    }

    [Test]
    public void T10StrikeOn19thBowlAndNoStrikeOn20thReturnTidy()
    {
        for (int i = 0; i < 18; i++)
        {
            actionMaster.Bowl(2);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(tidy, actionMaster.Bowl(5));
    }

    [Test]
    public void T11StrikeOn19thAndZeroON20thReturnsTidy()
    {
        for (int i = 0; i < 18; i++)
        {
            actionMaster.Bowl(2);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
    }

    [Test]
    public void T12ZeroAndTenShouldAdvance1Bowl()
    {
        Assert.AreEqual(tidy, actionMaster.Bowl(0));
        Assert.AreEqual(endTurn, actionMaster.Bowl(10));
        Assert.AreEqual(tidy, actionMaster.Bowl(5));
    }

    [Test]
    public void T13BensTestForSingleAdvance()
    {
        int[] rolls = { 0, 10, 5 };
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(endTurn, actionMaster.Bowl(1));
    }

    [Test]
    public void T1410thFrameTurkey()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        foreach (int roll in rolls)
        {
            actionMaster.Bowl(roll);
        }
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(reset, actionMaster.Bowl(10));
        Assert.AreEqual(endGame, actionMaster.Bowl(10));
    }
}
