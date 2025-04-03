namespace PartyList;
using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Please enter the number of the mode you would like to access: \n1. View Party List\n2. Add New Guest\n3. Edit Current Guest\nMode Selection: ");
        string mode = Console.ReadLine();

        if(mode=="1"){
            
            var party_list = File.ReadLines("party-list-data.txt");
            Console.WriteLine("                                     ~ Party List ~\n");

            foreach (var row in party_list){
                Console.WriteLine(row);
            }

            Console.WriteLine("\nPress ENTER to return to main menu");
            string main_menu = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(main_menu)){
                
            }

        }

        if(mode=="2"){

            string command;

            do{

                Console.Write("Enter guest first and last name: ");
                string guest_name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(guest_name)) guest_name = "n/a";

                Console.Write("Enter guest RSVP status (yes or no): ");
                string rsvp_status = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(rsvp_status)) rsvp_status = "n/a";

                Console.Write("Enter guest dietary preference (Leave blank if none): ");
                string dietary_preference = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(dietary_preference)) dietary_preference = "n/a";

                Console.Write("Enter guest plus-one status (yes or no): ");
                string plus_one_status = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(plus_one_status)) plus_one_status = "n/a";

                string row = string.Format("{0,-21} {1,-19} {2,-27} {3,-20}", guest_name, rsvp_status, dietary_preference, plus_one_status);

                File.AppendAllText("party-list-data.txt", row +Environment.NewLine);

                Console.Write("\nEnter one of the following options (1 or 2):\n1. Add another guest\n2. Return to main menu\nSelection: ");
                command = Console.ReadLine();

            } while(command!= "2");
            
        }

        if (mode=="3"){
            Console.Write("Enter guest first and last name: ");
            string edit_guest = Console.ReadLine();

            var lines = File.ReadAllLines("party-list-data.txt");
        }
    }
}
