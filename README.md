# Shift Logger Application
A simple, console-based application for logging shifts for multiple employees.

## Description
This is a basic Shift Logger application. It allows the user to manage employees and shifts. Employees consist of a First Name and a Last Name and can be read, created, renamed, or deleted. Shifts are added to a specific employee and consist of a clock in DateTime and a clock out DateTime. Shifts can be created, edited, or deleted. They can also be viewed for a specific employee by a custom date range.

This was built following The C# Academy [Shift Logger project guidelines](https://www.thecsharpacademy.com/project/17/shifts-logger).

### Limitations/Assumptions
- DateTime entries must strictly follow the format: "yyyy-MM-dd HH:mm:ss" (Similar to ISO 88601 but without the added "T" separating the date and time portions)
- Employee First Name and Last Name combinations must be unique (i.e. there can be only one "John Doe")

## Setup

1. Clone the repository using the provided GitHub link: [ShiftLogger Repository](https://github.com/jzhartman/ShiftLoggerApp)

2. Open the PowerShell and run the command:
<p align="center"><code>dotnet ef database update --startup-project ShiftLogger.WebApi --project ShiftLogger.Infrastructure</code></p>

3. Configure Multiple Startup Projects:
	 - Ensure that both ShiftLogger.Api and ShifLogger.Console are selected as Startup Projects!
     <p align="center"><img width="750" height="388" alt="image" src="https://github.com/user-attachments/assets/f79b248c-910b-49a2-aff4-191bb9b2ae91" /></p

4. In the ShiftLogger.Console project, open the appsettings.json file. Change the port from `1234` in the section below to the correct port for your local host. This can be found in the console window that opens when the Web API project is run.
    ```json
    "ApiSettings": {
        "BaseUrl": "https://localhost:1234/api/"
      }
    ```

5. Run the application.

## Features
- Clean Architecture (layered as Domain → Application → Infrastructure → Presentation)
- SQLite database with Employees and Shifts tables ordered in a one-to-many relationship (i.e. one employee may have many shifts)
- Entity Framework Core for mapping database tables to C# models
- Asp.Net core Web API with custom endpoints to route all CRUD operations to the appropriate handlers
- Console-based client to consume API
- UI using Spectre.Console
- Notable design patterns used:
  - Results Pattern for passing results and errors between layers
  - Repository Pattern to simplify changing out database types


## Usage
### Main Menu
- Opens with a Main Menu providing the options:

<p align="center"><img width="750" height="115" alt="image" src="https://github.com/user-attachments/assets/71d48ea4-01df-48d6-8cb8-4911bd7b4d93" /></p>

### Select Employee
- Sends HTTP request to API to get all employees
- Prints the list and prompts the user to select an employee. User can search the list by typing a name.
  <p align="center"><img width="750" height="198" alt="image" src="https://github.com/user-attachments/assets/ecf85113-6705-46de-9c65-329513c68552" /></p>

- Selecting an employee gives the Employee Menu:
<p align="center"><img width="750" height="158" alt="image" src="https://github.com/user-attachments/assets/080420ea-c16d-41af-8688-4dad390f38fa" /></p>

### Log a New Shift
Allows the user to enter a clock in time and a clock out time. Confirmation is given with a calculated duration. If approved, the client sends a post command to the API. A result object is returned. Errors or success messages are provided. The user then determines if they wish to add another shift, or return to the previous screen.

<p align="center"><img width="750" height="293" alt="image" src="https://github.com/user-attachments/assets/2e2fef4d-8982-4f1c-8386-bee288428402" /></p>

### View Previous Shifts
Prompts the user for a custom start date and end date (format: yyyy-MM-dd). It will the send a get request to the API and return a results object. This will either contain a list of all shifts included in the date range, or an error message. The list will print in table format, followed by a calculated value of the total number of hours worked for the selected date range. This is followed by the Shifts Menu.

  <p align="center"><img width="750" height="446" alt="image" src="https://github.com/user-attachments/assets/b75a0732-e739-439a-9225-72268b9efdcb" /></p>

  - <b>Edit Shift:</b> Enter the row Id of the shift to edit. Add a new clock in time and clock out time. A confirmation message is given displaying the old and new data side-by-side. If accepted, a put command is sent to the API. A result object is returned that reports either success or failure.
    <p align="center"><img width="750" height="285" alt="image" src="https://github.com/user-attachments/assets/f4da854e-110a-441d-a859-25bebcf14269" /></p>
    
  - <b>Delete Shift:</b> Enter the row Id of the shift to delete. A confirmation message is given. If accepted, a delete message is sent to the API. A result object is returned that reports either success or failure.
    <p align="center"><img width="750" height="180" alt="image" src="https://github.com/user-attachments/assets/c5387822-bcb9-472e-b74d-e87c10d87dc3" /></p>

  - <b>Select New Date Range:</b> Returns to the previous step and prompts the user for a start date and end date. It the reprints the table with the new date range, updates the total hours work field, and then reprints the Shift Menu.
      <p align="center"><img width="750" height="101" alt="image" src="https://github.com/user-attachments/assets/28ac9301-d331-4170-99af-eee2ed039b99" /></p>
      
  - <b>Return to Last Menu:</b> Returns to the previous menu.

- <b>Update Employee Name:</b> Prompts the user for a new first name and last name. A confirmation message is provided that displays the current name and new name. If the user accepts the changes, a put command is sent to the API. A result object is returned that reports either success or failure.

<p align="center"><img width="750" height="137" alt="image" src="https://github.com/user-attachments/assets/4578ff95-2912-46a7-ad70-48ccc90fa70d" /></p>

- <b>Delete Employee:</b> Deletes the user and all shifts associated with that user. A confirmation message is provided. If the user accepts, a delete command is sent to the API for both the shifts controller and the employees controller. A result object is returned that reports either success or failure.

<p align="center"><img width="750" height="76" alt="image" src="https://github.com/user-attachments/assets/bd37f43b-93a2-4b15-b05d-b6c7999675d2" /></p>

- <b>Return to Employee Selection:</b> Returns to the employee selection screen.

- <b>Return to Main Menu:</b> Returns to the main menu.

### Create a New Employee
Queries user for a first name and last name for the new employee. A post request is sent to the API. A results object is returned that reports either success or failure.

<p aligh="center"><img width="750" height="117" alt="image" src="https://github.com/user-attachments/assets/b79edf91-7fb8-4039-8eb4-338e24768746" /></p>

### Close Application
- Closes the application

---
## Developer Notes
This was my first project where I built both the API and the client. There were a lot of new technologies and conventions that I needed to learn for this that I plan to use for the next project in The C# Academy roadmap.

I've been using Clean Architecture for most of my projects and see a lot of pluses and minuses, particularly because my projects are relatively small. A common plus is that the organization has become familiar to me and it is much easier for me to build it in layers and to pick up where I left off between coding sessions. One downside is that it is a lot more complicated and requires more setup to get working. There is also the possibility for bloated constructors, which can create an issue for readability. One way I addressed this issue in my API controllers was through the use of method injection. This allowed me to wire up my endpoints to a handler from my use cases and directly inject the method into the specific endpoint. This was done using built-in ASP.net core features and did not require MedaitR.

Prior to building this project, I followed an online tutorial for building a flight tracker. This included search filters, sorting, and pagination. There are all features that would have benefited this application however, I opted to forgo their implementation. This project itself included a lot of new technologies for me to learn, so I wanted to simplify it a bit. The next project in the roadmap is a more advanced API/client project and specifically requires pagination. I plan to implement all three in that project.

The client was built using Spectre.Console. This is an amazing library for building very beautiful console applications. There are a lot of features that I have used in the past that could have benefited me here. However, very little of what I could have implemented design-wise would have had a strong lasting value to my learning (other than the force of repetition). Therefore, I opted to keep the UI very simple. My intent is to focus more on UI design when I move out of the Console Projects section and into one of the many front end options.
