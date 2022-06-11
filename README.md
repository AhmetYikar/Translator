# Translator
![image](https://user-images.githubusercontent.com/58369826/173206661-8771a820-3739-4087-a165-e9b5a7037952.png)

12 June 2022

#### C#, .NET 5: Translator, 

## Description
Translator, is a web app aimed to allow users to translate English words into some other languages. For now, you can only translate words into ["l33tsp34k"](https://en.wikipedia.org/wiki/Leet).
But new languages will be added to application soon.

## Project Details

The project has four main parts:

1. Translation public page. This part will be open for every user. It allows users to enter a text and see the translated result. The app, uses an available API, https://funtranslations.com/api/, for translation.
2. Admin Panel. This section is now open to all registered users. But later it will be open only to those with admin privileges. In this panel, admins can see all translated texts and also they can see all logs.
3. Identity. This Area contains some operations regarding authentication like login and register. 
4. Unit Tests. Aims to validate that each unit of code performs as expected.

## Setup/Installation Requirements

Visual Studio

.NET 5

MSSQL

Jquery

----

Clone this repo: git clone https://github.com/AhmetYikar/Translator.git

Go into the repo and run this application:

$ dotnet run

#### Then you have to create a database. This can be done via:

$ dotnet ef database update (for .NET CLI)

$ Update-Database (VS PowerShell)



