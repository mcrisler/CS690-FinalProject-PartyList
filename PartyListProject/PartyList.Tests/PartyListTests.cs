namespace PartyList.Tests;

using Microsoft.VisualStudio.TestPlatform.TestHost;
using PartyList;
using System.IO;
using Xunit;

public class PartyListTests
{    
    string testFileName;

    public PartyListTests() {
        
        testFileName = "test-party-list-data.txt";
        File.WriteAllText(testFileName,"James Harden    yes     vegan    yes\nEric Gordon   yes    none    no\nGerald Green    no     no    no\nClint Capela    yes     non-diary    yes");

    }

    [Fact]
    public void Test_ConfirmedGuestCount()
    {
        int result = PartyList.Program.ConfirmedGuestCount(testFileName);
        Assert.Equal(5, result);

    }
}
