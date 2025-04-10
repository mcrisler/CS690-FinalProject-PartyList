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
        File.WriteAllText(testFileName,"James Harden    yes     vegan    no");

    }

    [Fact]
    public void Test_ConfirmedGuestCount()
    {
        int result = PartyList.Program.ConfirmedGuestCount(testFileName);
        Assert.Equal(1, result);

    }
}
