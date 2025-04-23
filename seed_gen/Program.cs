using System.Reflection.Metadata;
using System.Security.AccessControl;

class GenMain
{
    static void Main(string[] args)
    {

        GenMain driver = new GenMain();
        List<string> entries = driver.createUser(10);

        entries.AddRange(driver.createRSPGForm(15));

        entries.AddRange(driver.createResource(15, 1, 2, "personal"));
        entries.AddRange(driver.createResource(15, 1, 2, "equipment"));
        entries.AddRange(driver.createResource(15, 1, 2, "travel"));
        entries.AddRange(driver.createResource(15, 1, 2, "other"));
        entries.AddRange(driver.createRatings(10, 1, 1, 15));

        using (StreamWriter sw = new StreamWriter("output.txt"))
        {
            foreach (string s in entries) sw.WriteLine(s);
        }
        foreach (string s in entries) Console.WriteLine(s);
    }

    /// <summary>
    /// Generates users class constructor in string form
    /// </summary>
    /// <param name="number">The number of users to generate</param>
    /// <returns List<string>The newly created user constructors</returns>
    public List<string> createUser(int number)
    {
        // Used to create randoms names
        string[] firstNames = {"Lyndon", "Brooks", "Petra", "Pamela", "Moises", "Carroll", "Lucile", "Caleb", "Ernest", "Dena", "Ronda",
            "Tobias", "Gabriela", "Sidney", "Adam", "Dominique", "Aisha", "Janette", "Elisabeth", "Garland"};

        string[] lastNames = { "Key", "Marks", "Griffin", "Monroe", "Bender", "Gross", "Chen", "Vang", "Odom", "Blankenship",
            "Mccarthy", "Rush", "Cherry", "Schwartz", "Farley", "Dawson", "Mayo", "Mathews", "Kirk", "Garner" };

        // The list to return
        List<string> users = new List<string>();
        // Used to hold random numbers used in generation
        int fname = 0;
        int lname = 0;
        int college = 0;
        int dept = 0;
        // Used to hold the string representation of the constructor
        string user = string.Empty;

        Random random = new Random();

        // Loops through creating string of the user constructor
        for (int i = 0; i < number; i++)
        {
            fname = random.Next(0, 20);
            lname = random.Next(0, 20);
            college = random.Next(1, 3);
            if (college == 1)
            {
                dept = random.Next(1, 3);
            }
            else
            {
                dept = random.Next(3, 5);
            }

            // Build the string
            user = "new User\n{";
            user += "\temail = \"" + firstNames[fname].Substring(0, 3).ToLower() + lastNames[lname].Substring(0, 3).ToLower() + "@mail.com\",\n";
            user += "\tpassword = PasswordHelper.HashPassword(\"" + firstNames[fname].Substring(0, 3).ToLower() + "\")" + ",\n";
            user += createVariable("firstName", "string", 0, firstNames[fname]);
            user += createVariable("firstName", "string", 0, lastNames[fname]);
            user += createVariable("CollegeId", "int", college);
            user += createVariable("DepartmentId", "int", dept);
            user += createVariable("position", "string", 0, "Professor");
            user += createVariable("RSPGMember", "string", 0, "", true);
            user += createVariable("isRSPGChair", "bool");
            user += createVariable("isAdmin", "bool");
            user += "}";
            if (i != number - 1)
            {
                user += ",";
            }
            user += "\n";
            users.Add(user);
        }
        return users;
    }

    /// <summary>
    /// Generates RSPGForm class constructor in string form
    /// </summary>
    /// <param name="number">The number of forms to generate</param>
    /// <returns>The newly created RSPG constructors</returns>
    public List<string> createRSPGForm(int number)
    {
        // A list of the current users name in the database
        string[] currentUsers = ["Wade Wilson", "Jean Grey", "Charles Xavier", "James Sadler", "John Snow", "Garland Garner", "Moises Odom", "Moises Gross", "Carroll Schwartz", 
            "Ernest Schwartz", "Aisha Kirk", "Dena Odom", "Ronda Mathews", "Aisha Rush", "Moises Vang"];
        // The list to return
        List<string> entries = new List<string>();
        // Used to hold values for user generation
        int id = 0;
        int college = 0;
        int dept = 0;
        int deptChair = 0;
        int deanName = 0;
        int program = 0;
        bool isSubmitted = false;

        // Used to hold the string representation of the constructor
        string entry = string.Empty;

        Random random = new Random();

        for (int i = 0; i < 0 + number; i++)
        {
            // Get random numbers for names used
            id = random.Next(0, 15);
            deptChair = random.Next(0, 15);
            deanName = random.Next(0, 15);
            program = random.Next(0, 15);

            // Get a college ID at random then get a corresponding department
            college = random.Next(1, 3);
            if (college == 1)
            {
                dept = random.Next(1, 3);
            }
            else
            {
                dept = random.Next(3, 5);
            }

            if (random.Next(0, 2) == 0)
            {
                isSubmitted = true;
            }

            // Build the string
            entry = "new RSPGFormModel\n{";
            entry += createVariable("UserId", "int", id + 1);
            entry += createVariable("ProjectTitle", "string",0, "Test Project Number: " + i);
            entry += createVariable("ProjectDirector", "string", 0, currentUsers[id]);
            entry += createVariable("CollegeId", "int", college);
            entry += createVariable("DepartmentId", "int", dept);
            entry += createVariable("MailCode", "string", 0, "12345");
            entry += createVariable("DepartmentChairName", "string", 0, currentUsers[deptChair]);
            entry += createVariable("DeanName", "string", 0, currentUsers[deanName]);
            entry += createVariable("ProgramDirectorName", "string", 0, currentUsers[program]);
            entry += createVariable("OtherParticipants", "string", 0, "None");
            entry += createVariable("RequiresIRB", "bool", 0, "None", false);
            entry += createVariable("IsSubmitted", "bool", 0, "None", isSubmitted);
            entry += "}";
            if (i != number - 1)
            {
                entry += ",";
            }
            entry += "\n";

            entries.Add(entry);
        }
        return entries;
    }

    /// <summary>
    /// Generates a resoruce class constructor in string form
    /// </summary>
    /// <param name="numberOfIDs">number of budget ids to connect to</param>
    /// <param name="startingNumber">the staring budget id number</param>
    /// <param name="numberOfEntries">number of entries for each budget form</param>
    /// <param name="resource">The name of the resource to create</param>
    /// <returns>The newly created resource constructors</returns>
    public List<string> createResource(int numberOfIDs, int startingNumber, int numberOfEntries, string resource)
    {
        // Used to fill text entry forms
        string[] personal = ["Student", "Self", "Coworker"];
        string[] equipment = ["Office Supplies", "Computer Parts", "Lab Equipment"];
        string[] travel = ["Gas", "Rental Car", "Flights"];
        string[] other = ["Food", "Public Awareness", "Miscellaneous"];
        string[] from = ["College", "Private Donors", "Fundraising"];

        // Holds the class name of the resource to create
        string resourceType = string.Empty;
        // The list to return
        List<string> usedResource = new List<string>();

        // Used to hold the string representation of the constructor
        string entry = string.Empty;

        // The list to return
        List<string> entries = new List<string>();

        // Get usedResource based on the resource
        switch (resource)
        {
            case "personal":
                usedResource = personal.ToList();
                resourceType = "PersonalResources";
                break;
            case "equipment":
                usedResource = equipment.ToList();
                resourceType = "EquipmentResource";
                break;
            case "travel":
                usedResource = travel.ToList();
                resourceType = "TravelResource";
                break;
            case "other":
                usedResource = other.ToList();
                resourceType = "OtherResource";
                break;

        }
     
        Random random = new Random();

        // Number of FundFrom used in create the constructor string
        int numberFundsFrom = 0;

        for (int i = startingNumber; i < numberOfIDs + startingNumber; i++)
        {
            numberFundsFrom = random.Next(0,3);
            for(int j = 0; j < numberOfEntries; j++)
            {
                // Start building the string
                entry = "new " + resourceType + "\n{\n";
                entry += createVariable("BudgetFormId", "int", i + 1);
                int RSPGTotal = random.Next(1000, 2001);
                // If its a personal resource
                if (resource == "personal")
                {
                    // Creates to entry one as a student and one that is not
                    if(j == 0)
                    {
                        entry += createVariable("IsStudent", "bool", 0, "None", true);
                        entry += createVariable("Name", "string", 0, usedResource[0]);
                        entry += createVariable("FundsFrom1", "string", 0, from[0]);
                        entry += createVariable("Total1", "int", random.Next(500, 1001));
                        entry += createVariable("RSPGTotal", "int", RSPGTotal);
                    }
                    else
                    {
                        entry += createVariable("IsStudent", "bool", 0, "None", false);
                        entry += createVariable("Name", "string", 0, usedResource[1]);
                        entry += createVariable("FundsFrom1", "string", 0, from[0]);
                        entry += createVariable("Total1", "int", random.Next(500, 1001));
                        entry += createVariable("RSPGTotal", "int",RSPGTotal);
                    }

                }
                else
                {
                    // Based on the numberFundsFrom builds a different string
                    if(numberFundsFrom == 0)
                    {
                        entry += createVariable("Name", "string", 0, usedResource[j]);
                        entry += createVariable("FundsFrom1", "string", 0, from[0]);
                        entry += createVariable("Total1", "int", random.Next(500, 1001));
                        entry += createVariable("RSPGTotal", "int", RSPGTotal);
                    }
                    else if(numberFundsFrom == 1)
                    {
                        entry += createVariable("Name", "string", 0, usedResource[j]);
                        entry += createVariable("FundsFrom1", "string", 0, from[0]);
                        entry += createVariable("Total1", "int", random.Next(500, 1001));
                        entry += createVariable("FundsFrom2", "string", 0, from[1]);
                        entry += createVariable("Total2", "int", random.Next(500, 1001));
                        entry += createVariable("RSPGTotal", "int", RSPGTotal);
                    }
                    else
                    {
                        entry += createVariable("Name", "string", 0, usedResource[j]);
                        entry += createVariable("FundsFrom1", "string", 0, from[0]);
                        entry += createVariable("Total1", "int", random.Next(500, 1001));
                        entry += createVariable("FundsFrom2", "string", 0, from[1]);
                        entry += createVariable("Total2", "int", random.Next(500, 1001));
                        entry += createVariable("FundsFrom3", "string", 0, from[2]);
                        entry += createVariable("Total3", "int", random.Next(500, 1001));
                        entry += createVariable("RSPGTotal", "int", RSPGTotal);
                    }

                }
                entry += "},\n";
                entries.Add(entry);
            }

        }
        return entries;
    }


    /// <summary>
    /// Generates a rating class constructor in string form
    /// </summary>
    /// <param name="numberOfUsers">The number of users to create rating for</param>
    /// <param name="startingUserNumber">The starting number usersID to create form for</param>
    /// <param name="startingFormNumber">The starting number of RSPGFormID to start from</param>
    /// <param name="numberOfForms">The ending number of forms to be added</param>
    /// <returns></returns>
    public List<string> createRatings(int numberOfUsers, int startingUserNumber, int startingFormNumber, int numberOfForms)
    {

        Random random = new Random();
        // Return list
        List<string> entries = new List<string>();
        // Used to hold the string representation of the constructor
        string entry = string.Empty;

        // Loops through the number of forms
        for (int i = startingFormNumber; i < numberOfForms + startingFormNumber; i++)
        {
            // Loops through all the users
            for (int j = startingUserNumber; j < startingUserNumber + numberOfUsers - 1; j++)
            {
                // Builds string
                entry = "new Rating" + "\n{\n"; random.Next(50, 96);
                entry += createVariable("UserId", "int", j +1);
                entry += createVariable("RSPGFormId", "int",  i + 1);
                entry += createVariable("RSPGRating", "int", random.Next(50, 96));
                entry += "},\n";

                entries.Add(entry);
            }
        }
        return entries;
    }

    /// <summary>
    /// Generate an variable line that assignes it a value used in the constructor based on passed in data
    /// </summary>
    /// <param name="entry">The variable name</param>
    /// <param name="type">The type of variable</param>
    /// <param name="intValue">The int value for the variable</param>
    /// <param name="stringValue">The string value for the variable</param>
    /// <param name="boolValue">The bool value for the variable</param>
    /// <returns>String of the variable with its value</returns>
    public string createVariable(string entry, string type, int intValue = 0, string stringValue = "", bool boolValue = false)
    {
        string tab = "\t";
        string quote = "\"";
        string endLine = ",\n";
        string line = string.Empty;

        line += tab + entry + " = ";

        switch(type)
        {
            case "string":
                line += quote + stringValue + quote;
                break;
            case "bool":
                line += boolValue.ToString().ToLower();
                break;
            case "int":
                line += intValue;
                break;
        }

        line += endLine;


        return line;
    }
}
