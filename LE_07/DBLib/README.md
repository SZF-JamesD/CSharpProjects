#DBLib - Database Connection Library

DBLib is a portable C# class library that provides a simple way to establish MySQL database connections using external configuration. The library reads 
your connection details from a JSON configuration file, making it easy to manage sensitive information outside of your source control.

Features

- External Configuration:
	Reads the MYSQL connection string from a JSON file (e.g., dbconfig.json), which can be git-ignored.

- Multiple Connection Methods:
	- Server Connection: Use 'GetConnection()' to connect to the MYSQL server without specifying a database.
	
	- Database Connection: Use 'GetConnection(string database)' to connect to a specific database.
	This allows you to connect to the server, create a new database, then connect directly to that database.

- Portable and Reusable:
	Designed as a class library so it can be easily used across different projects without relying on an 'app.config'.


Prerequisites

- .NET Framework 4.7.2
- MySql.Data NuGet package
- Newtonsoft.Json NuGet package


Installlation

1. Clone or add the DBLib project to your soluation.
2. Install NuGet Packages (Install-Package MySql.Data , Install-Package Newtonsoft.Json)
3. Ensure DBLib is targeting .NET Framework 4.7.2.  -  You can check this in the project porperties under the Application tab.


Configuration

Create a JSON file named 'dbconfig.json' (or different if preffered) with your MySQL connection string. For example:
{
	"dbConnectionString": "Server=YOUR_SERVER;User=YOUR_USER;Password=YOUR_PASSWORD;"
}


Configuration Location

- Default Location:
	If no environment variable is set, the library will look for 'dbconfig.json' in the current working directory.

- Custom Location:
	To specify a custom path for the configuration file, set an environment variable 'DB_CONFIG_PATH' with the full path to your JSON config file.


Usage

- Getting a Connection to the Server
	Use this method when you want to connect to the MySQL server without specifying a databse. This can be useful when you need to create a new database:
	using System.Data.Common;
	using DBLib;

	DbConnection serverConnection = DBConnetion.GetConnection();
	// Open the connection, perform your operations, then close it.

- Getting a Connection to a Specific Database
	After creating a database on the server, connect to it by specifying the datbase name:
	using System.Data.Common;
	using DBLib;

	DbConnection dbConnection = DBConnection.GetConnection("YourDatabaseName");
	// Open the connection, perform your operations, then close it.


Troubleshooting

- Missing References:
	If you encounter missing reference errors, ensure that:
		- The NuGet packages 'MySql.Data' and 'Newtonsoft.Json' are installed in your DBLib project.
		- Your project targets .NET Framework 4.7.2.
		- You have cleaned and rebuilt the solution.

- Configuration File Not Found:
	Make sure that the 'dbconfig.json' file is located in the expected directory or set the 'DB_CONFIG_PATH' environment variable to the correct path.