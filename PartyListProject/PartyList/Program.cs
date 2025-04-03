namespace PartyList;
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string mode = AskForInput("Please enter the number of the mode you would like to access: \n1. View Party List\n2. Add New Guest\n3. Edit Current Guest\nMode Selection: ");

        if(mode=="1"){
            
            var party_list = File.ReadLines("party-list-data.txt");
            Console.WriteLine("                                     ~ Party List ~\n");

            foreach (var row in party_list){
                Console.WriteLine(row);
            }

            string main_menu = AskForInput("\nPress ENTER to return to main menu");
            if (string.IsNullOrWhiteSpace(main_menu)){
                
            }

        }

        if(mode=="2"){

            string command;

            do{

                string guest_name = AskForInput("Enter guest first and last name: ");
                if (string.IsNullOrWhiteSpace(guest_name)) guest_name = "n/a";

                string rsvp_status = AskForInput("Enter guest RSVP status (yes or no): ");
                if (string.IsNullOrWhiteSpace(rsvp_status)) rsvp_status = "n/a";

                string dietary_preference = AskForInput("Enter guest dietary preference (Leave blank if none): ");
                if (string.IsNullOrWhiteSpace(dietary_preference)) dietary_preference = "n/a";

                string plus_one_status = AskForInput("Enter guest plus-one status (yes or no): ");
                if (string.IsNullOrWhiteSpace(plus_one_status)) plus_one_status = "n/a";

                string row = string.Format("{0,-21} {1,-19} {2,-27} {3,-20}", guest_name, rsvp_status, dietary_preference, plus_one_status);

                File.AppendAllText("party-list-data.txt", row +Environment.NewLine);

                command = AskForInput("\nEnter one of the following options (1 or 2):\n1. Add another guest\n2. Return to main menu\nSelection: ");

            } while(command!= "2");
            
        }

        if (mode=="3"){
            string edit_guest = AskForInput("Enter guest first and last name: ");

            var lines = File.ReadAllLines("party-list-data.txt");
        }
    }

    public static string AskForInput(string message) {
        Console.WriteLine(message);
        return Console.ReadLine();
    }
}
