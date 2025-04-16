# TodoApp

## Overview
TodoApp is a Windows Forms application designed to help users manage their tasks efficiently. It provides features to create, update, delete, and view tasks (todos) with additional functionalities like setting priorities, start and end dates, and marking tasks as finished.

## Features
- **Task Management**: Add, edit, delete, and view todos.
- **User Profiles**: Manage todos for individual users.
- **Prioritization**: Assign priority levels to tasks.
- **Date Management**: Set start and end dates for tasks.
- **Status Tracking**: Mark tasks as finished or unfinished.
- **Database Integration**: Uses MySQL for data storage.

## Installation
1. Clone the repository to your local machine.
2. Ensure you have the following prerequisites installed:
   - .NET Framework 4.7.2
   - MySQL Server
3. Update the database connection string in `Database.cs` to match your MySQL server configuration.
4. Build and run the project in Visual Studio 2022.

## Usage
1. Launch the application.
2. Use the main interface to:
   - View tasks for a specific user.
   - Add new tasks using the "Add Todo" form.
   - Edit or delete existing tasks using the "Edit Todo" form.
3. The application will automatically save changes to the MySQL database.

## Dependencies
- **MySQL.Data**: For database connectivity.
- **System.Windows.Forms**: For the graphical user interface.

## Project Structure
- `Database.cs`: Handles all database operations (CRUD for todos and users).
- `AddTodoForm.cs`: Form for adding new tasks.
- `EditTodoForm.cs`: Form for editing or deleting existing tasks.
- `SplashForm.cs`: Displays a splash screen when the application starts.
- `Program.cs`: Entry point of the application.

## License
This project is licensed under the MIT License. See the LICENSE file for details.