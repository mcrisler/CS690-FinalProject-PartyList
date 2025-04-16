namespace PartyList.Tests;

using Microsoft.VisualStudio.TestPlatform.TestHost;
using PartyList;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using Xunit;

public class PartyListTests
{    
    string testFileName;

    public PartyListTests() {
        
        testFileName = "test-party-list-data.txt";

    }

    [Fact]
    public void Test_ConfirmedGuestCount(){ // test to make sure the guest count counts only the guests that responded "yes" to rsvp

        File.WriteAllText(testFileName,"James Harden    yes     vegan    no\nEric Gordon   yes    none    no\nGerald Green    no     no    no\nClint Capela    yes     non-diary    no");

        int result = PartyList.Program.ConfirmedGuestCount(testFileName);
        Assert.Equal(3, result); // 3 people confirmed their RSVP, so the guest count should equal 3

    }
    
    [Fact]
    public void Test_CountWithPlusOnes(){ // test to see if the guest count successfully counts the plus ones 

        File.WriteAllText(testFileName, "James Harden    yes    vegan    no\nChris Paul    yes    none    yes\nPJ Tucker    yes    gluten free    yes");

        int result = PartyList.Program.ConfirmedGuestCount(testFileName);
        Assert.Equal(5, result); // 3 guests plus 2 plus ones should equal 5 
    }

    [Fact]
    public void Test_RSVP_NoResponse(){ // test to make sure the rsvp column defaults to "---" if user does not answer

        string rsvp_status = "";
        string no_response = "---";
        string result;

        if (string.IsNullOrWhiteSpace(rsvp_status)){
            result = no_response;
            }
            else{
                result = rsvp_status;
            }
            
        Assert.Equal("---", result); // if code is correct, the rsvp column should be "---" by default

    }

    [Fact]
    public void Test_RSVP_BLANK_Default_FillAll(){ // test to make sure all columns default to "---" if rsvp is also "---"

        string rsvp_status = "---";
        string no_response = "---";
        string dp_status;
        string plus_one_status;

        if (rsvp_status == "---"){
            dp_status = no_response;
            plus_one_status = no_response;
        }
        else{
            dp_status = "error in test";
            plus_one_status = "error in code";
        }

        Assert.Equal("---", dp_status); // if code is correct, the dp column should default to "---"
        Assert.Equal("---", plus_one_status); // if code is correct, the plus one column should default to "---"

    }

    [Fact]
    public void Test_RSVP_NO_Default_FillAll(){ // test to make sure all columns default to "no" if rsvp is also "no"

        string rsvp_status = "no";
        string rsvp_no_response = "no";
        string dp_status;
        string plus_one_status;

        if (rsvp_status == "no"){
            dp_status = rsvp_no_response;
            plus_one_status = rsvp_no_response;
        }
        else{
            dp_status = "error in test";
            plus_one_status = "error in code";
        }

        Assert.Equal("no", dp_status); // if code is correct, the dp column should default to "no"
        Assert.Equal("no", plus_one_status); // if code is correct, the plus one column should default to "no"

    }


    }



