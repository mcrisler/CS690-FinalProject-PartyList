namespace PartyList;
using System;
using System.IO;
using System.Text.RegularExpressions;

class Program
{
    static void Main(string[] args)
    {
         var party_list = File.ReadLines("party-list-data.txt");

            foreach (var row in party_list){
                Console.WriteLine(row);
            }

        string mode = AskForInput("\nPlease enter one of the following options (1 or 2): \n1. Add New Guest\n2. Edit Current Guest\nOption Selection: ");

        if(mode=="1"){

            string guest_name = AskForInput("Enter guest first and last name: ");
            if (string.IsNullOrWhiteSpace(guest_name)) guest_name = "n/a";

            string rsvp_status = AskForInput("Enter guest RSVP status (yes or no): ");
            if (string.IsNullOrWhiteSpace(rsvp_status)) rsvp_status = "n/a";

            string dietary_preference = AskForInput("Enter guest dietary preference (Leave blank if none): ");
            if (string.IsNullOrWhiteSpace(dietary_preference)) dietary_preference = "n/a";

            string plus_one_status = AskForInput("Enter guest plus-one status (yes or no): ");
            if (string.IsNullOrWhiteSpace(plus_one_status)) plus_one_status = "n/a";

            string row = string.Format("{0,-24} {1,-13} {2,-13} {3,-13}", guest_name, rsvp_status, dietary_preference, plus_one_status);

            File.AppendAllText("party-list-data.txt", row +Environment.NewLine);
            
        }

        if (mode=="2"){

            bool guest_name_recognized = false;

            while (!guest_name_recognized){

                string edit_guest = AskForInput("Enter guest first and last name: ");

            string[] lines = File.ReadAllLines("party-list-data.txt");
            bool verified_guest = false;

            for (int i = 0; i < lines.Length; i++) {

                string[] cat_columns = Regex.Split(lines[i].Trim(), @"\s{4,}");

                if (cat_columns[0].Trim() == edit_guest) {
                    verified_guest = true;
                    Console.WriteLine($"\nGuest Information \nGuest Name: {cat_columns[0]}\nRSVP Status: {cat_columns[1]}\nDietary Preference: {cat_columns[2]}\nPlus-One: {cat_columns[3]}\n");

                    Console.Write("Update RSVP Status (yes or no): ");
                    cat_columns[1] = Console.ReadLine();
                    Console.Write("Update Dietary Preference: ");
                    cat_columns[2] = Console.ReadLine();
                    Console.Write("Update Plus-One Status (yes or no): ");
                    cat_columns[3] = Console.ReadLine();

                    lines[i] = string.Join("            ", cat_columns);

                    File.WriteAllLines("party-list-data.txt", lines);
                    Console.WriteLine("Guest Information Updated");

                    guest_name_recognized = true;

                    foreach (var row in party_list){
                    Console.WriteLine(row);
                    }

                    break;
                }
            }

            if(!verified_guest){

                Console.WriteLine("\nGuest not found in system. Try Again.");
                
            }
        }

            }

            
    }

    public static string AskForInput(string message) {
        Console.WriteLine(message);
        return Console.ReadLine();
    }
}
