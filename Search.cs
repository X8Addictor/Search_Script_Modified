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

            try
            {

                if (args.Length != 2)
                {
                    throw new Exception("Expected: Search.exe <csv_file_path> <column_index>");
                }

                csv_file_path = args[0];

                if (!File.Exists(csv_file_path))
                {
                    throw new Exception("File path " + "\"" + csv_file_path + "\"" + " cannot be found.");
                }

                if (!int.TryParse(args[1], out column_index))
                {
                    throw new Exception("Invalid <column_index> " + "\"" + args[1] + "\"");
                }

                column_index = int.Parse(args[1]);
                search_key = "";

                if (column_index == 0)
                {
                    int id;
                    Console.Write("Enter ID: ");
                    search_key = Console.ReadLine().Trim();

                    if (search_key == "")
                    {
                        throw new Exception("<ID> cannot be empty.");
                    }

                    if (!int.TryParse(search_key, out id))
                    {
                        throw new Exception("Invalid <ID> " + "\"" + search_key + "\"");
                    }
                }
                else if (column_index == 1)
                {
                    Console.Write("Enter first name: ");
                    search_key = Console.ReadLine().Trim();

                    if (search_key == "")
                    {
                        throw new Exception("<First_Name> cannot be empty.");
                    }
                }
                else if (column_index == 2)
                {
                    Console.Write("Enter last name: ");
                    search_key = Console.ReadLine().Trim();

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

                    if (search_key == "")
                    {
                        throw new Exception("<Date_Of_Birth> cannot be empty.");
                    }

                    if (search_key.Length != 10)
                    {
                        throw new Exception("Invalid <Date_Of_Birth> " + "\"" + search_key + "\" , Expected: (dd/mm/yyyy)");
                    }

                    if (!int.TryParse(search_key.Substring(0,2), out date) || !int.TryParse(search_key.Substring(3,2), out date) || !int.TryParse(search_key.Substring(6,4), out date))
                    {
                        throw new Exception("Invalid <Date_Of_Birth> " + "\"" + search_key + "\" , Expected: (dd/mm/yyyy)");
                    }

                    if (int.Parse(search_key.Substring(0,2)) < 1 || int.Parse(search_key.Substring(0,2)) > 31 || search_key.Substring(0,2).Length < 2 || search_key.Substring(0,2).Length > 2)
                    {
                        throw new Exception("Invalid day " + "\"" + search_key.Substring(0,2) + "\"");
                    }

                    if (int.Parse(search_key.Substring(3,2)) < 1 || int.Parse(search_key.Substring(3,2)) > 12 || search_key.Substring(3,2).Length < 2 || search_key.Substring(3,2).Length > 2)
                    {
                        throw new Exception("Invalid month " + "\"" + search_key.Substring(3,2) + "\"");
                    }

                    if (search_key.Substring(6,4).Length < 4 || search_key.Substring(6,4).Length > 4)
                    {
                        throw new Exception("Invalid year" + "\"" + search_key.Substring(6,4) + "\"");
                    }
                }

                found = false;

                using (StreamReader streamReader = new StreamReader(csv_file_path))
                {
                    while (streamReader.EndOfStream == false)
                    {
                        record = streamReader.ReadLine().Trim();
                        output = record;

                        if (record == "" || record == null || record.Split(',').ToList().Count <= column_index)
                        {
                            continue;
                        }

                        record = record.Remove(record.IndexOf(";"));
                        fields = record.Split(',').ToList();
                        field = fields[column_index].Trim();

                        if (field.ToLower().Equals(search_key.ToLower()))
                        {
                            found = true;
                            Console.WriteLine(output);
                            break;
                        }
                    }
                }

                if (!found)
                {
                    Console.WriteLine("\"" + search_key + "\"" + " not found.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return;
            }
        }
    }
}