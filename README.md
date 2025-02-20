# DocumentStorage.API

Simple API for DocumentStorage

## API

- supports JSON for sending data
- supports JSON and XML as return types
- for all supported functions look at `DocumentStorage.API/Controllers/DocumentsController.cs`
- `Scalar` included for testing - it will open on application run

## Instructions

1. Download repo
2. Install MSSQL server
3. Update `DocumentStorageDbConnectionString` in `DocumentStorage.API/appsettings.json` to your own `connection string`
4. Run migrations - run command (`Update-Database`) in Package manager console

## Test functionality
1. Run the application (Scalar will be opened)
2. Test functions

## Message format

- `id` element is number
- `data` element is stringified JSON
- `tags` element is array of strings

```
{
  "id": 1,
  "data": "{
      "name" : "MyName", 
      "content" : "Some content"
      "footer" : "Some footer"
      }",
  "tags": [".NET", "C#"]
}

