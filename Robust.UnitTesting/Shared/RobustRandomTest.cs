using System;
using System.Collections.Generic;
using NUnit.Framework;
using Robust.Shared.Random;

namespace Robust.UnitTesting.Shared;

[TestFixture, TestOf(typeof(RandomExtensions))]
public sealed class RobustRandomTest
{
    private const int Seed = 85723475;

    [Test]
    public void TestDieRandom()
    {
        IRobustRandom compRandom = RobustRandom.FromSeed(Seed);
        IRobustRandom testRandom = RobustRandom.FromSeed(Seed);
        var actual = testRandom.RollDice(1, 4);
        var expected = compRandom.Next(1, 4);
        Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    [Parallelizable]
    [TestCase(3, 4)]
    [TestCase(1, 6)]
    [TestCase(4, 70)]
    [TestCase(5, 120)]
    [TestCase(30, 1000)]
    public void TestDiceRandom(int num, int faces)
    {
        IRobustRandom compRandom = RobustRandom.FromSeed(Seed);
        IRobustRandom testRandom = RobustRandom.FromSeed(Seed);
        var actual = testRandom.RollDice(num, faces);
        var sum = 0;
        for (uint i = 0; i < num; i++)
        {
            sum += compRandom.Next(1, faces + 1);
        }

        Assert.That(actual, Is.EqualTo(sum));
    }

    [Test]
    public void TestRandomSeed()
    {
        var num = 2;
        var faces = 20;
        for (int l = 0; l < 50; l++)
        {
            var seed = new RobustRandom().Next();
            IRobustRandom compRandom = RobustRandom.FromSeed(seed);
            IRobustRandom testRandom = RobustRandom.FromSeed(seed);
            var actual = testRandom.RollDice(num, faces);
            var sum = 0;
            for (uint i = 0; i < num; i++)
            {
                sum += compRandom.Next(1, faces + 1);
            }

            Assert.That(actual, Is.EqualTo(sum));
        }
    }

    [Test]
    public void TestAllRandomValues()
    {
        IRobustRandom random = RobustRandom.FromSeed(Seed);
        var sides = 6;
        var sidesSet = new HashSet<int>();
        var expectedSides = new HashSet<int>()
        {
            1, 2, 3, 4, 5, 6
        };
        for (int i = 0; i < 1000; i++)
        {
            sidesSet.Add(random.RollDice(1, sides));
        }

        CollectionAssert.AreEquivalent(expectedSides, sidesSet);
    }
}