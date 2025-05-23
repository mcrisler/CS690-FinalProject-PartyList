﻿namespace PartyList;

using System;
using System.IO;
using System.Text.RegularExpressions;
using Spectre.Console;
using Spectre.Console.Cli;

public class Program
{
    static void Main(string[] args)
    {    
         // convert the party list txt file into a string so it can be displayed for and edited by the user
         string party_list_file = "party-list-data.txt";

         // use a while statement to loop through the program until the user wants to exit
         while(true) {
         
         var party_list = File.ReadLines("party-list-data.txt");
         // clear the console after the user exits the program
         Console.Clear();

            // read and print each line of the party list in the console for the user
            foreach (var row in party_list){
                Console.WriteLine(row);
            }
            // display the current party list
            int confirmed_guest_rsvp = ConfirmedGuestCount(party_list_file);
            Console.WriteLine($"\nConfirmed Guest Count: {confirmed_guest_rsvp}");
            Console.WriteLine("_____________________________________________________________");

        // define a variable "no_response" to be a place filler for any inputs the user skips
        string no_response = "---";

        // display main menu options
        var mode = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("\nPlease select mode:").AddChoices(new[]{"Add New Guest","Edit Guest","Remove Guest","EXIT"}));

        // Mode 1: ADD NEW GUEST
        if(mode=="Add New Guest"){

            string command;

            do{
                // prompt user for new guest name
                string guest_name = AskForInput("\nEnter guest first and last name: ");
            if (string.IsNullOrWhiteSpace(guest_name)) guest_name = no_response;
            guest_name = string.Join(" ", guest_name.Split(' ').Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));

            string dp_status = "";
            string plus_one_status = "";

            // prompt user for new guest rsvp
            string rsvp_status = AskForInput("\nEnter guest RSVP status (yes or no): ");
            if (rsvp_status == "yes" || rsvp_status == "Yes" || rsvp_status == "YES") {

                rsvp_status = "yes";
            
                // prompt user for new guest dietary preference
                dp_status = AskForInput("\nEnter guest dietary preference (Leave blank if none): ");
                if (string.IsNullOrWhiteSpace(dp_status)) dp_status = no_response;
                dp_status = dp_status.ToLower(); //convert user inputs to lowercase to maintain list consistency 

                // prompt user for new guest plus one 
                plus_one_status = AskForInput("\nEnter guest plus-one status (yes or no): ");
                if (string.IsNullOrWhiteSpace(plus_one_status)) plus_one_status = no_response;

                if (plus_one_status == "yes" || plus_one_status == "Yes" || plus_one_status == "YES") {
                    plus_one_status = "yes";
                }
                if (plus_one_status == "no" || plus_one_status == "No" || plus_one_status == "NO") {
                    plus_one_status = "no";
                }
            }
            
            if (string.IsNullOrWhiteSpace(rsvp_status)) {

                // fill dp and plus one columns with "no_response" if user does not provide input for rsvp
                // if the guest is not confirmed to come, their DP and plus one status are irrelevant atm
                rsvp_status = no_response;
                dp_status = no_response;
                plus_one_status = no_response;

            } 

            if (rsvp_status == "no" || rsvp_status == "No" || rsvp_status == "NO") {

                // fill dp and plus one columns with "no" if user inputs "no" for rsvp
                // if the guest is not coming, their DP and plus one status are automatically "no"
                rsvp_status = "no";
                dp_status = "no";
                plus_one_status = "no";

            } 

            // add new guest info to party list
            string row = string.Format("{0,-24} {1,-13} {2,-13} {3,-13}", guest_name, rsvp_status, dp_status, plus_one_status);
            File.AppendAllText("party-list-data.txt", row +Environment.NewLine);
            Console.WriteLine($"\n* {guest_name} will be added to party list *\n");

            // prompt user to either enter another new guest or return to main menu
            command = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Please make a selection to continue:").AddChoices(new[]{"Add Another Guest","DONE"}));
            

            // loop through until user selects "DONE" to go back to main menu
            } while (command!="DONE");

            
        }

        // Mode 2: EDIT GUEST
        if (mode=="Edit Guest"){

            string command;

            do{
            
            Console.WriteLine("\nEDIT GUEST");

            bool guest_name_not_recognized = false;
    
            // if name is found in name, continue through functions. 
            while (guest_name_not_recognized == false){

                // prompt user to enter guest name to edit
                string edit_guest = AskForInput("\nEnter guest first and last name: ");
                edit_guest = string.Join(" ", edit_guest.Split(' ').Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower())); // capitalize the first letter of first and last name

                string[] lines = File.ReadAllLines("party-list-data.txt");
                bool guest_in_list = false;
                for (int i = 0; i < lines.Length; i++) {
                string[] list_items = Regex.Split(lines[i], @"\s{4,}");

                // display the guests current party list info for user 
                if (list_items[0].Trim() == edit_guest) {
                    guest_in_list = true;
                    Console.WriteLine($"\nGuest Information \nGuest Name: {list_items[0]}\nRSVP Status: {list_items[1]}\nDietary Preference: {list_items[2]}\nPlus-One: {list_items[3]}\n");

                    // prompt user to enter updated rsvp status for guest
                    string update_rsvp = AskForInput("\nRSVP Status (yes or no): ");
                    if (string.IsNullOrWhiteSpace(update_rsvp)) update_rsvp = no_response;
                    // update column
                    list_items[1] = update_rsvp;

                    // prompt user for updated inputs for DP and plus one ONLY if they reply "yes" to rsvp status
                    if (update_rsvp == "yes") {

                        string update_dp = AskForInput("\nDietary Preference: ");
                        update_dp = update_dp.ToLower();
                        if (string.IsNullOrWhiteSpace(update_dp)) update_dp = no_response;
                        list_items[2] = update_dp;

                        string update_plusone = AskForInput("\nPlus-One Status (yes or no): ");
                        update_plusone = update_plusone.ToLower();
                        if (string.IsNullOrWhiteSpace(update_plusone)) update_plusone = no_response;
                        list_items[3] = update_plusone;
                    }

                    // if updated rsvp is "no", fill other columns with "no"
                    if (update_rsvp == "no"|| update_rsvp == "No" || update_rsvp == "NO") {

                        list_items[2] = "no";
                        list_items[3] = "no";

                    }
                    // if updated rsvp was left blank, fill other columns with no_response string
                    if (update_rsvp == "---"){
                        list_items[2] = no_response;
                        list_items[3] = no_response;
                    }
                    
                    // update guest info to party list
                    lines[i] = string.Format("{0,-24} {1,-13} {2,-13} {3,-13}", edit_guest, list_items[1], list_items[2], list_items[3]);

                    File.WriteAllLines("party-list-data.txt", lines);
                    Console.WriteLine($"\n* {edit_guest}'s information will be updated *\n");

                    guest_name_not_recognized = true;
                        
                    }
                }

            // if guest name is not found in party list, prompt user to enter name again until found
            if(guest_in_list == false){

                Console.WriteLine($"\n{edit_guest} not found in system. Try Again.");            
                        }
                    }
                    // prompt user to either edit another guest's info or return to main menu
                    command = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Please make a selection to continue:").AddChoices(new[]{"Edit Another Guest","DONE"}));
                    } while(command!="DONE");
                }


            // Mode 3: REMOVE GUEST
            if (mode == "Remove Guest"){

                string command;

                do{

                Console.WriteLine("REMOVE GUEST");
                bool guest_name_recognized = false;

                while(!guest_name_recognized){

                // prompt user to enter guest name to remove
                string remove_guest = AskForInput("\nEnter guest first and last name:");
                remove_guest = string.Join(" ", remove_guest.Split(' ').Select(word => char.ToUpper(word[0]) + word.Substring(1).ToLower()));
                string[] list_rows = File.ReadAllLines(party_list_file);
                

                var guest_removed_list = new System.Collections.Generic.List<string>();

                // search for guest name in party list 
                foreach (var row in list_rows) {
                    string[] guest_names = Regex.Split(row, @"\s{4,}");
                    if (guest_names[0].Trim() == remove_guest) {
                        guest_name_recognized = true;
                    }
                    else{
                        guest_removed_list.Add(row);
                    }
                }

                // rewrite entire party list, minus the guest name user wants to remove
                if(guest_name_recognized){

                    File.WriteAllLines(party_list_file, guest_removed_list);
                    Console.WriteLine($"\n* {remove_guest} will be removed from party list *\n");
                } else {
                    Console.WriteLine($"{remove_guest} not found in party list. Please try again.");
                }
                }
                command = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("Please make a selection to continue:").AddChoices(new[]{"Remove Another Guest","DONE"}));
                } while(command!="DONE");
            }


            // Mode 4: EXIT PROGRAM
            else if (mode == "EXIT") {
                // clears console and ends program
                Console.Clear();
                Console.WriteLine("\nParty List Closed\n");
                break;
            } 
            }      
    }


    public static string AskForInput(string message) {
        Console.WriteLine(message);
        return Console.ReadLine();
    }

    public static int ConfirmedGuestCount(string filePath){

        int guest_count = 0;

        string[] list_rows = File.ReadAllLines(filePath);

        foreach (var row in list_rows){

            string[] list_columns = Regex.Split(row, @"\s{2,}");
            if (list_columns.Length >= 4) {

                if (list_columns[1].Trim() == "yes") {

                    guest_count++;

                }
                if (list_columns[3].Trim() == "yes") {
                    guest_count++;
                } 
            }
        }
        return guest_count;
    }
}
