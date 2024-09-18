# ToolLendify Backend

ToolLendify Backend is the server-side application for the ToolLendify project. It handles the API endpoints, database operations, and business logic required to support the frontend functionality.

## Features

- **User Authentication:** Registration, Login, JWT-based sessions.
- **Tool Management:** Add, update, delete tools.
- **Rental Management:** Handle rental requests and track statuses.
- **Payment Processing:** Secure transactions through Paymob.
- **Real-Time Notifications:** Send updates for rental requests and status changes via Firebase Cloud Messaging (FCM).

## Tech Stack

- **Backend:** [.NET Core (C#)](https://docs.microsoft.com/en-us/dotnet/core/)
- **Database:** [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-2019)
- **Authentication:** [ASP.NET Core Identity with JWT](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity)
- **Payment:** [Paymob](https://www.paymob.com/)
- **Notifications:** [Firebase Cloud Messaging (FCM)](https://firebase.google.com/docs/cloud-messaging)

## Getting Started

### Prerequisites

Make sure you have the following installed:

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Paymob Account](https://www.paymob.com/)
- [Firebase Account](https://firebase.google.com/)

### Installation

1. **Clone the repository:**
    ```bash
    git clone https://github.com/JoudMahmoud/ToolLendify-backend.git
    cd ToolLendify-backend
    ```

2. **Install Dependencies:**
    ```bash
    dotnet restore
    ```

3. **Database Setup:**
    - Create a new SQL Server database.
    - Update the connection string in the `appsettings.json` file.

4. **Paymob Setup:**
    - Create a Paymob account and obtain the API keys.
    - Update the Paymob configuration in `appsettings.json`.

5. **Firebase Setup:**
    - Create a Firebase project and enable Firebase Cloud Messaging.
    - Obtain the Server Key from Firebase and update the backend project with the configuration.

### Running the Application

1. **Start the Backend:**
    ```bash
    dotnet run
    ```

## Usage

- **Endpoints:** Refer to the API documentation for available endpoints.

## Contributing

Contributions are welcome! Open an issue or submit a pull request.


## Contact

For support, contact [joud.mahmoud1323@gmail.com].

## Acknowledgements

- [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/) for the backend framework.
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-2019) for the database.
- [Paymob](https://www.paymob.com/) for payment processing.
- [Firebase Cloud Messaging](https://firebase.google.com/docs/cloud-messaging) for notifications.
