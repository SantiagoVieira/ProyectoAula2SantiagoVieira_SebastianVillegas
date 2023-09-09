using System;
using System.Collections.Generic;
using System.Linq;

class BusinessIdeaTests
{
    public int Code { get; }
    public string Name { get; set; }
    public string SocialEconomicImpact { get; set; }
    public List<Department> BeneficiaryDepartments { get; set; }
    public decimal InvestmentValue { get; set; }
    public decimal First3YearsIncome { get; set; }
    public List<TeamMember> TeamMembers { get; set; }
    public List<string> Industry4Tools { get; set; }

    public BusinessIdeaTests(int code)
    {
        Code = code;
        BeneficiaryDepartments = new List<Department>();
        TeamMembers = new List<TeamMember>();
        Industry4Tools = new List<string>();
    }
}

class Department
{
    public int Code { get; set; }
    public string Name { get; set; }
}

class TeamMember
{
    public string Identification { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Role { get; set; }
    public string Email { get; set; }
}

class Program
{
    static List<BusinessIdeaTests> businessIdeas = new List<BusinessIdeaTests>();

    static void Main()
    {
        int businessIdeaCode = 1;

        while (true)
        {
            Console.WriteLine("Menu of Options:");
            Console.WriteLine("1. Enter a business idea");
            Console.WriteLine("2. Add team members");
            Console.WriteLine("3. Remove team members");
            Console.WriteLine("4. Modify investment value");
            Console.WriteLine("5. Show information");
            Console.WriteLine("6. Show team members of a business idea");
            Console.WriteLine("7. Exit");

            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    EnterBusinessIdea(businessIdeaCode++);
                    break;
                case 2:
                    AddTeamMember();
                    break;
                case 3:
                    RemoveTeamMember();
                    break;
                case 4:
                    ModifyInvestmentValue();
                    break;
                case 5:
                    ShowInformation();
                    break;
                case 6:
                    ShowTeamMembersByBusinessIdeaCode();
                    break;
                case 7:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please select a valid option.");
                    break;
            }
        }
    }

    static void EnterBusinessIdea(int code)
    {
        var idea = new BusinessIdeaTests(code);

        Console.WriteLine("Enter the name of the business idea:");
        idea.Name = Console.ReadLine();

        Console.WriteLine("Enter the social or economic impact:");
        idea.SocialEconomicImpact = Console.ReadLine();

        Console.WriteLine("Enter the beneficiary departments (comma-separated):");
        var departmentsInput = Console.ReadLine().Split(',');
        foreach (var dept in departmentsInput)
        {
            var department = new Department();
            department.Code = businessIdeas.Count + 1;
            department.Name = dept.Trim();
            idea.BeneficiaryDepartments.Add(department);
        }

        Console.WriteLine("Enter the investment value:");
        idea.InvestmentValue = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Enter the total income in the first 3 years:");
        idea.First3YearsIncome = decimal.Parse(Console.ReadLine());

        Console.WriteLine("Enter the Industry 4.0 tools used (comma-separated):");
        var toolsInput = Console.ReadLine().Split(',');
        foreach (var tool in toolsInput)
        {
            idea.Industry4Tools.Add(tool.Trim());
        }

        businessIdeas.Add(idea);

        Console.WriteLine($"Business idea registered successfully. Idea code: {idea.Code}");
    }

    static void AddTeamMember()
    {
        Console.WriteLine("Enter the code of the business idea:");
        int ideaCode = int.Parse(Console.ReadLine());

        var idea = businessIdeas.FirstOrDefault(i => i.Code == ideaCode);

        if (idea != null)
        {
            var teamMember = new TeamMember();

            Console.WriteLine("Enter the identification of the team member:");
            teamMember.Identification = Console.ReadLine();

            Console.WriteLine("Enter the name of the team member:");
            teamMember.Name = Console.ReadLine();

            Console.WriteLine("Enter the surname of the team member:");
            teamMember.Surname = Console.ReadLine();

            Console.WriteLine("Enter the role of the team member:");
            teamMember.Role = Console.ReadLine();

            Console.WriteLine("Enter the email of the team member:");
            teamMember.Email = Console.ReadLine();

            idea.TeamMembers.Add(teamMember);
            Console.WriteLine("Team member added successfully.");
        }
        else
        {
            Console.WriteLine("No business idea found with that code.");
        }
    }

    static void RemoveTeamMember()
    {
        Console.WriteLine("Enter the code of the business idea:");
        int ideaCode = int.Parse(Console.ReadLine());

        var idea = businessIdeas.FirstOrDefault(i => i.Code == ideaCode);

        if (idea != null)
        {
            Console.WriteLine("Enter the identification of the team member to remove:");
            string identification = Console.ReadLine();

            var teamMember = idea.TeamMembers.FirstOrDefault(i => i.Identification == identification);

            if (teamMember != null)
            {
                idea.TeamMembers.Remove(teamMember);
                Console.WriteLine("Team member removed successfully.");
            }
            else
            {
                Console.WriteLine("No team member found with that identification.");
            }
        }
        else
        {
            Console.WriteLine("No business idea found with that code.");
        }
    }

    static void ModifyInvestmentValue()
    {
        Console.WriteLine("Enter the code of the business idea:");
        int ideaCode = int.Parse(Console.ReadLine());

        var idea = businessIdeas.FirstOrDefault(i => i.Code == ideaCode);

        if (idea != null)
        {
            Console.WriteLine("Enter the new investment value:");
            idea.InvestmentValue = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Investment value modified successfully.");
        }
        else
        {
            Console.WriteLine("No business idea found with that code.");
        }
    }

    static void ShowTeamMembersByBusinessIdeaCode()
    {
        Console.WriteLine("Enter the code of the business idea to view its team members:");
        int ideaCode = int.Parse(Console.ReadLine());

        var idea = businessIdeas.FirstOrDefault(i => i.Code == ideaCode);

        if (idea != null)
        {
            Console.WriteLine($"Team members of Business Idea ({idea.Name}):");
            foreach (var teamMember in idea.TeamMembers)
            {
                Console.WriteLine($"Identification: {teamMember.Identification}");
                Console.WriteLine($"Name: {teamMember.Name}");
                Console.WriteLine($"Surname: {teamMember.Surname}");
                Console.WriteLine($"Role: {teamMember.Role}");
                Console.WriteLine($"Email: {teamMember.Email}");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine("No business idea found with that code.");
        }
    }

    static void ShowInformation()
    {
        Console.WriteLine("Information of the business idea that impacts the most departments:");
        var maxDepartments = businessIdeas.OrderByDescending(i => i.BeneficiaryDepartments.Count).FirstOrDefault();
        ShowIdea(maxDepartments);

        Console.WriteLine("\nInformation of the business idea with the highest total income in the first 3 years:");
        var maxIncome = businessIdeas.OrderByDescending(i => i.First3YearsIncome).FirstOrDefault();
        ShowIdea(maxIncome);

        Console.WriteLine("\nNames of the top 3 most profitable business ideas:");
        var profitableIdeas = businessIdeas.OrderByDescending(i => i.First3YearsIncome).Take(3);
        ShowIdeas(profitableIdeas);

        Console.WriteLine("\nNames of the business ideas that impact more than 3 departments:");
        var departmentIdeas = businessIdeas.Where(i => i.BeneficiaryDepartments.Count > 3);
        ShowIdeas(departmentIdeas);

        decimal totalIncomeSum = businessIdeas.Sum(i => i.First3YearsIncome);
        decimal totalInvestmentSum = businessIdeas.Sum(i => i.InvestmentValue);

        Console.WriteLine($"\nTotal Sum of Incomes for All Business Ideas: {totalIncomeSum}");
        Console.WriteLine($"Total Sum of Investment Needed for All Business Ideas: {totalInvestmentSum}");

        Console.WriteLine("\nThe business idea with the most Industry 4.0 tools:");
        ShowIdeaWithMostIndustry4Tools();

        int ideasWithAI = businessIdeas.Count(i => i.Industry4Tools.Contains("Artificial Intelligence"));
        Console.WriteLine($"\nNumber of Business Ideas that use Artificial Intelligence: {ideasWithAI}");
    }

    static void ShowIdea(BusinessIdeaTests idea)
    {
        if (idea != null)
        {
            Console.WriteLine($"Name: {idea.Name}");
            Console.WriteLine($"Social or Economic Impact: {idea.SocialEconomicImpact}");
            Console.WriteLine("Beneficiary Departments:");
            foreach (var dept in idea.BeneficiaryDepartments)
            {
                Console.WriteLine($"{dept.Name}");
            }
        }
    }

    static void ShowIdeas(IEnumerable<BusinessIdeaTests> ideas)
    {
        foreach (var idea in ideas)
        {
            ShowIdea(idea);
        }
    }

    static void ShowIdeaWithMostIndustry4Tools()
    {
        var ideaWithMostIndustry4Tools = businessIdeas
            .OrderByDescending(i => i.Industry4Tools.Count)
            .FirstOrDefault();

        if (ideaWithMostIndustry4Tools != null)
        {
            Console.WriteLine($"Business idea with the most Industry 4.0 tools: {ideaWithMostIndustry4Tools.Name}");
        }
        else
        {
            Console.WriteLine("No business idea found with Industry 4.0 tools.");
        }
    }
}