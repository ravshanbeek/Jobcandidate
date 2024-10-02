
### Using the Application

This section explains how to interact with the **Job Candidate Hub API**, including adding or modifying candidate information and retrieving the preferred communication methods.

#### 1. Add or Update Candidate Information

The **`/api/candidate/send`** endpoint allows you to add a new candidate or update existing candidate information based on the email.

- **Endpoint**: `POST /api/candidate/send`
- **Description**: Creates a new candidate or updates the existing candidate's information. The candidate is identified by the email, which is unique.
- **Body**: The request body should contain a JSON object representing the candidate's information.

##### Request Body Example

```json
{
    "firstName": "Ravshan",
    "lastName": "Akhadov",
    "phoneNumber": "+998903245611",
    "email": "ravshanaxadov@mail.ru",
    "linkedInProfile": "https://www.linkedin.com/in/ravshan-akhadov/",
    "gitHubProfile": "https://github.com/ravshanbeek/",
    "comments": "Skilled developer.",
    "preferWay": 1,
    "callTimeInterval": "9AM-11AM"
}
```

##### Response Example

```json
{
    "statusCode": 200,
    "message": "Success",
    "data": {
        "firstName": "Ravshan",
        "lastName": "Akhadov",
        "phoneNumber": "+998903245611",
        "email": "ravshanaxadov@mail.ru",
        "linkedInProfile": "https://www.linkedin.com/in/ravshan-akhadov/",
        "gitHubProfile": "https://github.com/ravshanbeek/",
        "comments": "Skilled developer.",
        "callTimeInterval": "9AM-11AM"
    }
}
```

#### 2. Get Preferred Communication Ways

The **`/api/manual/getpreferway`** endpoint returns the list of available communication methods that candidates can prefer.

- **Endpoint**: `GET /api/manual/getpreferway`
- **Description**: Retrieves a list of communication preferences (e.g., call, email, LinkedIn, GitHub).
- **Response**: The API will return a list of communication methods.

##### Response Example

```json
{
    "statusCode": 200,
    "message": "Success",
    "data": [
        {
            "id": 1,
            "name": "Call"
        },
        {
            "id": 2,
            "name": "Email"
        },
        {
            "id": 3,
            "name": "LinkedIn"
        },
        {
            "id": 4,
            "name": "GitHub"
        }
    ]
}
```

### Request/Response Status Codes

- **200 OK**: The request was successful, and the response contains the expected data.
- **400 Bad Request**: The request was invalid or missing required fields.
- **500 Internal Server Error**: There was an issue on the server side.

### Data Validation

The `CandiateCreateOrModifyDto` contains validations:
- **FirstName**, **LastName**, and **Email** are required fields.
- **Email** must be a valid email address format.
- **PhoneNumber** should be in the valid phone number format.
- **LinkedInProfile** and **GitHubProfile** should not exceed 100 characters.

### Example API Calls

Using `curl` to interact with the API:

1. **Add or Update Candidate**:
   ```bash
   curl -X POST https://localhost:7136/api/candidate/send \
   -H "Content-Type: application/json" \
   -d '{
         "firstName": "Ravshan",
         "lastName": "Akhadov",
         "phoneNumber": "+998903245611",
         "email": "ravshanaxadov@mail.ru",
         "linkedInProfile": "https://www.linkedin.com/in/ravshan-akhadov/",
         "gitHubProfile": "https://github.com/ravshanbeek/",
         "comments": "Skilled developer.",
         "preferWay": 1,
         "callTimeInterval": "9AM-11AM"
       }'
   ```

2. **Get Preferred Communication Methods**:
   ```bash
   curl https://localhost:7136/api/manual/getpreferway
   ```

### Development and Contribution

To set up the development environment, follow these steps:

1. **Clone the repository**:
   ```bash
   git clone https://github.com/yourusername/job-candidate-hub.git
   cd job-candidate-hub
   ```

2. **Run the application**:
   ```bash
   dotnet run
   ```

3. The API will be available at `https://localhost:7136`.
