using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace CSscript
{
    class Search
    {
        static void Main(string[] args)
        {
            List<string> fields;
            string field;
            string csv_file_path;
            string search_key;
            string record;
            string output;
            int column_index;
            bool found;

            //Try catch block to handle exceptions
            try
            {
                //Checks if 2 arguments has been passed or not
                if (args.Length != 2)
                {
                    throw new Exception("Expected: Search.exe <csv_file_path> <column_index>");
                }

                //Assigns csv_file_path
                csv_file_path = args[0];

                //Checks whether the csv file exists within the passed file path
                if (!File.Exists(csv_file_path))
                {
                    throw new Exception("File path " + "\"" + csv_file_path + "\"" + " cannot be found.");
                }

                //Checks whether the second argument is an integer
                if (!int.TryParse(args[1], out column_index))
                {
                    throw new Exception("Invalid <column_index> " + "\"" + args[1] + "\"");
                }

                //Assigns the column_index
                column_index = int.Parse(args[1]);
                search_key = "";

                //Based on the column_index value, requests user to input the specified search_key 
                if (column_index == 0)
                {
                    int id;
                    Console.Write("Enter ID: ");
                    search_key = Console.ReadLine().Trim();

                    //Checks if search_key is empty
                    if (search_key == "")
                    {
                        throw new Exception("<ID> cannot be empty.");
                    }

                    //Checks if search_key is an integer 
                    if (!int.TryParse(search_key, out id))
                    {
                        throw new Exception("Invalid <ID> " + "\"" + search_key + "\"");
                    }
                }
                else if (column_index == 1)
                {
                    Console.Write("Enter first name: ");
                    search_key = Console.ReadLine().Trim();

                    //Checks if search_key is empty
                    if (search_key == "")
                    {
                        throw new Exception("<First_Name> cannot be empty.");
                    }
                }
                else if (column_index == 2)
                {
                    Console.Write("Enter last name: ");
                    search_key = Console.ReadLine().Trim();

                    //Checks if search_key is empty
                    if (search_key == "")
                    {
                        throw new Exception("<Last_Name> cannot be empty.");
                    }
                }
                else if (column_index == 3)
                {
                    int date;
                    Console.Write("Enter date of birth(dd/mm/yyyy): ");
                    search_key = Console.ReadLine().Trim();

                    //Checks if search_key is empty
                    if (search_key == "")
                    {
                        throw new Exception("<Date_Of_Birth> cannot be empty.");
                    }

                    //Checks if date format length is valid or not
                    if (search_key.Length != 10)
                    {
                        throw new Exception("Invalid <Date_Of_Birth> " + "\"" + search_key + "\" , Expected: (dd/mm/yyyy)");
                    }

                    //Checks if date values are integer or not
                    if (!int.TryParse(search_key.Substring(0,2), out date) || !int.TryParse(search_key.Substring(3,2), out date) || !int.TryParse(search_key.Substring(6,4), out date))
                    {
                        throw new Exception("Invalid <Date_Of_Birth> " + "\"" + search_key + "\" , Expected: (dd/mm/yyyy)");
                    }

                    //Checks if dd is of valid or not
                    if (int.Parse(search_key.Substring(0,2)) < 1 || int.Parse(search_key.Substring(0,2)) > 31)
                    {
                        throw new Exception("Invalid day " + "\"" + search_key.Substring(0,2) + "\"");
                    }

                    //Checks if mm is of valid or not
                    if (int.Parse(search_key.Substring(3,2)) < 1 || int.Parse(search_key.Substring(3,2)) > 12)
                    {
                        throw new Exception("Invalid month " + "\"" + search_key.Substring(3,2) + "\"");
                    }

                    //Checks if yyyy is of valid length or not
                    if (search_key.Substring(6,4).Length < 4 || search_key.Substring(6,4).Length > 4)
                    {
                        throw new Exception("Invalid year" + "\"" + search_key.Substring(6,4) + "\"");
                    }
                }

                //Flag to check whether the search_key has been found inside the csv file
                found = false;

                //Opens the csv file and reads the content
                using (StreamReader streamReader = new StreamReader(csv_file_path))
                {
                    //Reads the csv file till end of file
                    while (streamReader.EndOfStream == false)
                    {
                        //Reads the current line
                        record = streamReader.ReadLine().Trim();
                        output = record;

                        //Checks whether the line is empty or column_index is out of range
                        if (record == "" || record == null || record.Split(',').ToList().Count <= column_index)
                        {
                            continue;
                        }

                        //Removes the ";" at the end of the line
                        record = record.Remove(record.IndexOf(";"));

                        //Converts the line to a list
                        fields = record.Split(',').ToList();

                        //Assigns the field with the given column index
                        field = fields[column_index].Trim();

                        //Checks whether the field equals to the search_key
                        if (field.ToLower().Equals(search_key.ToLower()))
                        {
                            //Sets found flag to true
                            found = true;

                            //Returns the line
                            Console.WriteLine(output);
                            break;
                        }
                    }
                }

                //Checks whether search_key has been found or not
                if (!found)
                {
                    Console.WriteLine("\"" + search_key + "\"" + " not found.");
                }
            }
            catch (Exception e)
            {
                //Returns an exception if encountered any
                Console.WriteLine(e);
                return;
            }
        }
    }
}