using EFCorePolls.DTO;
using EFCorePolls.Entity;
using EFCorePolls.Enums;
using EFCorePolls.Infrustructor.Services;
using System;
using System.Collections.Generic;

UserService userService = new UserService();
PollService pollService = new PollService();

UserDto LoggedInUser = null;

while (true)
{
    Console.Clear();
    Console.WriteLine("Welcome to EFCorePolls!");
    if (LoggedInUser == null)
    {
        Console.WriteLine("1. Register");
        Console.WriteLine("2. Login");
        Console.WriteLine("3. Exit");
        Console.Write("Choose an option: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                RegisterUser();
                break;
            case "2":
                LoginUser();
                break;
            case "3":
                return;
        }
    }
    else
    {
        Console.WriteLine($"Hello, {LoggedInUser.UserName}! Role: {LoggedInUser.Role}");
        if (LoggedInUser.Role == UserEnum.Admin)
        {
            Console.WriteLine("1. Create Poll");
            Console.WriteLine("2. Show Poll Results");
            Console.WriteLine("3. Logout");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    CreatePoll();
                    break;
                case "2":
                    ShowResults();
                    break;
                case "3":
                    LoggedInUser = null;
                    break;
            }
        }
        else
        {
            Console.WriteLine("1. Vote in a Poll");
            Console.WriteLine("2. Show Polls");
            Console.WriteLine("3. Logout");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    VotePoll();
                    break;
                case "2":
                    ShowPolls();
                    break;
                case "3":
                    LoggedInUser = null;
                    break;
            }
        }
    }
    Console.WriteLine("Press any key to continue...");
    Console.ReadKey();
}


void RegisterUser()
{
    Console.Write("Enter username: ");
    string username = Console.ReadLine();
    Console.Write("Enter password: ");
    string password = Console.ReadLine();
    Console.Write("Enter role (Admin/User): ");
    string roleInput = Console.ReadLine();
    UserEnum role = roleInput?.ToLower() == "admin" ? UserEnum.Admin : UserEnum.NormalUser;

    var result = userService.Register(username, password, role);
    Console.WriteLine(result.Message);
}

void LoginUser()
{
    Console.Write("Enter username: ");
    string username = Console.ReadLine();
    Console.Write("Enter password: ");
    string password = Console.ReadLine();

    var loginResult = userService.login(username, password);
    Console.WriteLine(loginResult.Message);
    if (loginResult.IsSuccess)
    {
        LoggedInUser = new UserDto
        {
            Id = loginResult.Id,
            UserName = username,
            Role = loginResult.Role
        };
    }
}

void CreatePoll()
{
    Console.Write("Enter poll title: ");
    string title = Console.ReadLine();
    Console.Write("Enter question text: ");
    string question = Console.ReadLine();
    Console.Write("Option 1: ");
    string op1 = Console.ReadLine();
    Console.Write("Option 2: ");
    string op2 = Console.ReadLine();
    Console.Write("Option 3: ");
    string op3 = Console.ReadLine();
    Console.Write("Option 4: ");
    string op4 = Console.ReadLine();

    var result = pollService.CreatePoll(title, question, op1, op2, op3, op4);
    Console.WriteLine(result.Message);
}

void ShowResults()
{
    Console.Write("Enter Poll ID: ");
    if (int.TryParse(Console.ReadLine(), out int pollId))
    {
        var results = pollService.ShowPollResult(pollId);
        var questionDto = pollService.ShowQuestionText(pollId);
        Console.WriteLine($"Question: {questionDto.Text}");
        foreach (var r in results)
        {
            Console.WriteLine($"Option: {r.OptionText}, Votes: {r.VoteCount}, Percentage: {r.Percentage:F2}%");
        }
    }
    else
    {
        Console.WriteLine("Invalid Poll ID.");
    }
}

void ShowPolls()
{
    var polls = pollService.ShowPolls();
    foreach (var poll in polls)
    {
        Console.WriteLine($"Poll ID: {poll.PollId},QuestionId: {poll.QuestionId}, Question: {poll.QuestionText}");
    }
}

void VotePoll()
{
    ShowPolls();
    Console.Write("Enter Question ID: ");
    int questionId = int.Parse(Console.ReadLine());
    Console.Write("Enter option number (1-4): ");

    int selectedOption = int.Parse(Console.ReadLine());

    var result = pollService.Vote(questionId, selectedOption, LoggedInUser.Id, LoggedInUser.UserName);
    Console.WriteLine(result.Message);
}

class UserDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public UserEnum Role { get; set; }
}

//Console.WriteLine("salam seyed");
