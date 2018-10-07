using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

[TestFixture]
public class ActionMasterTest
{
    List<int> pinFalls;
    private ActionMaster.Action endTurn = ActionMaster.Action.EndTurn;
    private ActionMaster.Action endGame = ActionMaster.Action.EndGame;
    private ActionMaster.Action tidy = ActionMaster.Action.Tidy;
    private ActionMaster.Action reset = ActionMaster.Action.Reset;

    [SetUp]
    public void Setup()
    {
        pinFalls = new List<int>();
    }


    [Test]
    public void T01OneStrikeReturnsEndTurn()
    {
        pinFalls.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T02Bowl8ReturnTidy()
    {
        pinFalls.Add(8);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T03TwoBowlsReturnEndTurn()
    {
        pinFalls.Add(3);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
        pinFalls.Add(4);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T04Bowl28SpareReturnEndturn()
    {
        int[] rolls = { 2, 8 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T05AnyPinsOnLastBowlReturnEndGame()
    {
        HitPins(3, 18);
        pinFalls.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
        pinFalls.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
        pinFalls.Add(2);
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T06StrikeOn19thBowlReturnReset()
    {
        HitPins(3, 18);
        pinFalls.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T07StrikeOn20thBowlReturnReset()
    {
        HitPins(3, 19);
        pinFalls.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T08SpareOnLastFrameAwarded()
    {
        HitPins(3, 18);
        pinFalls.Add(5);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
        pinFalls.Add(5);
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T09NoStrikeOnLastFrameEndsGameOn20thBowl()
    {
        HitPins(2, 19);
        pinFalls.Add(3);
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T10StrikeOn19thBowlAndNoStrikeOn20thReturnTidy()
    {
        HitPins(2, 18);
        pinFalls.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
        pinFalls.Add(5);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T11StrikeOn19thAndZeroON20thReturnsTidy()
    {
        HitPins(2, 18);
        pinFalls.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
        pinFalls.Add(0);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }


    [Test]
    public void T12ZeroAndTenShouldAdvance1Bowl()
    {
        pinFalls.Add(0);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
        pinFalls.Add(10);
        Assert.AreEqual(endTurn, ActionMaster.NextAction(pinFalls));
        pinFalls.Add(5);
        Assert.AreEqual(tidy, ActionMaster.NextAction(pinFalls));
    }

    [Test]
    public void T13BensTestForSingleAdvance()
    {
        int[] rolls = { 0, 10, 5, 1 };
        Assert.AreEqual(endTurn, ActionMaster.NextAction(rolls.ToList()));
    }

    [Test]
    public void T1410thFrameTurkey()
    {
        int[] rolls = { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 };
        pinFalls = rolls.ToList();
        pinFalls.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
        pinFalls.Add(10);
        Assert.AreEqual(reset, ActionMaster.NextAction(pinFalls));
        pinFalls.Add(10);
        Assert.AreEqual(endGame, ActionMaster.NextAction(pinFalls));
    }

    private void HitPins(int pinCount, int bowlCount)
    {
        for (int i = 0; i < bowlCount; i++)
        {
            pinFalls.Add(pinCount);
        }
    }
}
