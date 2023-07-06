# HTML template to PDF generator

The project is a .NET Core 6 API application that handles basic CRUD operations for HTML templates and generates PDFs based on the specified HTML templates. It uses a Clean Architecture pattern and supports Docker. The HTML templates are stored in a MySQL database. The API returns the generated PDFs in base64 format.

## Prerequisites

To run the project locally, ensure you have the following prerequisites installed:

- [.NET Core 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [Docker](https://www.docker.com/)
- [MySQL](https://www.mysql.com/) database (can be running locally or in a Docker container)

## Configuration

Before running the project, you need to configure the database connection string and other settings. Follow these steps:

1. Open the `appsettings.json` file in the project's root directory.
2. Locate the `ConnectionStrings` section and replace the `DefaultConnection` value with your MySQL connection string. Example:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Port=3306;Database=YourDatabase;Uid=YourUsername;Pwd=YourPassword;"
   }

## Getting Started

Follow these steps to get the project up and running:

1. Clone the repository:

   ```bash
   git clone https://github.com/Srestituyo/HtmlTemplateToPdfGenerator.git

2. Navigate to the project's root directory:
   
   ```bash
   cd HtmlTemplateToPdfGenerator
   
4. Build the Docker container:

   ```bash
    docker build -t your-container-name .

5. Run the Docker container:
    ```bash
    docker run -p 5000:80 -e "ConnectionStrings:DefaultConnection=YourMySQLConnectionString" your-container-name

### API Endpoints


The following endpoints are available:

- **GET /api/templates**: Retrieves all HTML templates.
- **GET /api/templates/{id}**: Retrieves a specific HTML template by ID.
- **POST /api/templates**: Creates a new HTML template.
- **PUT /api/templates/{id}**: Updates an existing HTML template by ID.
- **DELETE /api/templates/{id}**: Deletes an HTML template by ID.
- **POST /api/templates/generate-pdf**: Generates a PDF based on the specified HTML template and provided context.
