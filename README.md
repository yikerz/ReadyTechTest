# Coffee Machine API

This project implements an HTTP API for controlling an imaginary internet-connected coffee machine. It fulfills the specified requirements and includes tests for various scenarios.

## Requirements

### Implemented Features

1. **GET /brew-coffee Endpoint**: When this endpoint is called, it returns a `200 OK` status code with a JSON response body containing a status message and the current date/time in ISO-8601 format.
    - Example response:
      ```json
      {
        "message": "Your piping hot coffee is ready",
        "prepared": "2024-03-01T09:30:00+00:00"
      }
      ```

2. **Coffee Machine Status Check**: On every fifth call to the `/brew-coffee` endpoint, it returns a `503 Service Unavailable` status code with an empty response body, indicating that the coffee machine is out of coffee.

3. **April 1st Special Case**: If the date is April 1st, all calls to the `/brew-coffee` endpoint return a `418 I’m a teapot` status code with an empty response body, indicating that the endpoint is not brewing coffee on April 1st.

## Non-functional Requirements

1. **Implementation**: The solution is implemented in .NET Core, ensuring compatibility and flexibility.
2. **Testing**: The project includes comprehensive unit tests and integration tests to cover various scenarios, ensuring the correctness and robustness of the API.
