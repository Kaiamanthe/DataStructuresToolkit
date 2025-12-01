using DataStructuresToolkit;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.IO;

namespace DataStructuresToolkit.Tests;

public class AssociativeHelpersTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void RunDictionary_WritesExpectedPhoneBookOutput()
    {
        // Arrange
        using var writer = new StringWriter();
        TextWriter originalOut = Console.Out;
        Console.SetOut(writer);

        try
        {
            // Act
            AssociativeHelpers.RunDictionary();
        }
        finally
        {
            Console.SetOut(originalOut);
        }

        // Assert
        string output = writer.ToString();

        // Basic header and key checks
        StringAssert.Contains("Dictionary (Phone Book):", output);
        StringAssert.Contains("ContainsKey(\"Alice\"): True", output);
        StringAssert.Contains("ContainsKey(\"David\"): False", output);
        StringAssert.Contains("Lookup \"Alice\" => 555-0101", output);
        StringAssert.Contains("Lookup \"David\" => (not found)", output);
    }

    [Test]
    public void RunHashSet_WritesExpectedSetOutput()
    {
        // Arrange
        using var writer = new StringWriter();
        TextWriter originalOut = Console.Out;
        Console.SetOut(writer);

        try
        {
            // Act
            AssociativeHelpers.RunHashSet();
        }
        finally
        {
            Console.SetOut(originalOut);
        }

        // Assert
        string output = writer.ToString();

        // Header
        StringAssert.Contains("HashSet (No Duplicates):", output);

        // Add behavior (first true, second false)
        StringAssert.Contains("Add(\"apple\"):  True", output);
        StringAssert.Contains("Add(\"apple\"):  False", output);
        StringAssert.Contains("Add(\"banana\"): True", output);

        // Contains checks
        StringAssert.Contains("Contains(\"apple\"): True", output);
        StringAssert.Contains("Contains(\"pear\"):  False", output);

        // Contents line
        StringAssert.Contains("Set contents:", output);
    }

    [Test]
    public void RunAllAsc_CallsDictionaryAndHashSetHelpers()
    {
        // Arrange
        using var writer = new StringWriter();
        TextWriter originalOut = Console.Out;
        Console.SetOut(writer);

        try
        {
            // Act
            AssociativeHelpers.RunAllAsc();
        }
        finally
        {
            Console.SetOut(originalOut);
        }

        // Assert
        string output = writer.ToString();

        // Should include both dictionary and hash set sections
        StringAssert.Contains("Dictionary (Phone Book):", output);
        StringAssert.Contains("HashSet (No Duplicates):", output);
    }
}
