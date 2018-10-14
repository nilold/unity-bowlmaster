using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

[TestFixture]
public class ScoreDisplayTest
{
    [Test]
    public void T00PassTest()
    {
        Assert.AreEqual(0, 0);
    }

    [Test]
    public void T01Bowl1(){
        int[] rolls = { 1 };
        string rollsString = "1";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T01Bowl11(){
        int[] rolls = { 1, 1 };
        string rollsString = "11";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T02Bowl155()
    {
        int[] rolls = { 1, 5, 5 };
        string rollsString = "155";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T03Bowl55Spare()
    {
        int[] rolls = { 5, 5 };
        string rollsString = "5/";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T04Bowl10Strike()
    {
        int[] rolls = { 10 };
        string rollsString = "X ";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }


    [Test]
    public void T05BowlSpareAndStrike()
    {
        int[] rolls = { 4, 6, 10 };
        string rollsString = "4/X ";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T06SampleGame()
    {
        int[] rolls = { 4,2, 7,0, 3,7, 1,2, 10, 10, 4,6, 3,2 };
        string rollsString = "427-3/12X X 4/32";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T07StrikeOnLastFrame()
    {
        int[] rolls = { 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10,3,3 };
        string rollsString = "111111111111111111X33";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }

    [Test]
    public void T08Zeros()
    {
        int[] rolls = { 1, 1, 0, 5, 0, 0 };
        string rollsString = "11-5--";
        Assert.AreEqual(rollsString, ScoreDisplay.FormatRolls(rolls.ToList()));
    }
}
