# CountriesRepo
Countries App, provides information about countries all around the world (Vue.js and .NET)

## Requirements

To run this project, you will need the following requirements:

- **.NET 9.0** (or higher)
- **Entity Framework Core** (used for database interaction)
- **SQL Server** (with a local instance running)
- **@vue/cli 5.0.8** (specific version required)

## Database Setup

Before running the project, ensure that the necessary database is properly configured. Run the following SQL command to create the `HangfireDB` database:

```sql
CREATE DATABASE HangfireDB;

How to Run the Project
Step 1: Clone the Repository
Clone this repository to your local environment:

Step 2: Install Dependencies
Install the Vue CLI (version 5.0.8):

bash
npm install -g @vue/cli@5.0.8
Install the dependencies for the frontend (Vue.js):

bash
npm install

Install the dependencies for the backend (.NET):
bash
dotnet restore

Step 3: Set Up the Database
Ensure the HangfireDB database is created correctly (see the instructions above).

Step 4: Configure the Connection Strings
Make sure to update the connection strings for the database in the configuration files (e.g., appsettings.json for .NET backend).
By default it's "localhost", make sure your user have acces to this instance.

Step 5: Run the Project
Backend (API .NET): In the backend project directory, run:

bash
dotnet run
Frontend (Vue.js): In the frontend project directory, run:

bash
npm run serve

Step 6: Migrate the Database (if necessary)
If your project uses Entity Framework for migrations, run the following command to apply any pending migrations:

bash
dotnet ef database update
Step 7: Access the Application
Now, you can access the application in your browser. The frontend will be available at http://localhost:8080 (or another configured port), and the API will be available at the address specified in the backend configuration.



