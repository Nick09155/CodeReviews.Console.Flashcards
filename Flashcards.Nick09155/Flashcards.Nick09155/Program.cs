// See https://aka.ms/new-console-template for more information

using Flashcards.Nick09155.Models;
using Flashcards.Nick09155.Services;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json")
    .Build();

var service = new DatabaseService(config);
service.InitializeSchema();
service.SeedData();
UserMenu userMenu = new UserMenu(config);
userMenu.GetUserInput();
