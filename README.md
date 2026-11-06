# database-scrambler
Provide a way to scramble your offline SQL database with configuration

# usage
```bash
DatabaseScrambler.exe "<connection-string>" ScrambleConfiguration.xml custom_sql.sql
```
- custom_sql.sql is an optional parameter
- exits with code 0 on success and 1 on failure, in which case all changes are rolled back

# build
Requires the [.NET 10 SDK](https://dotnet.microsoft.com/download).
```bash
dotnet build -c Release
```
The executable is written to `DatabaseScrambler/bin/Release/net10.0/`.

Note: the tool uses `Microsoft.Data.SqlClient`, which encrypts connections by default. When the SQL Server has no trusted certificate, add `TrustServerCertificate=True` (or `Encrypt=False`) to the connection string.

Microsoft Entra authentication is supported through the `Authentication` keyword, e.g. `Authentication=Active Directory Default`. Set `AZURE_TOKEN_CREDENTIALS=AzureCliCredential` to make it use the Azure CLI login only (for example inside an Azure DevOps AzureCLI task).

# configuration
Sample configuration:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<!-- Default culture is NL, DE supports Address & City -->
<root culture="nl">
  <!-- These types replace column data based on pre-defined lists -->
  <scramble type="FirstName" tableName="Person" columnName="FirstName" identifier="id" />
  <scramble type="LastName" tableName="Person" columnName="LastName" identifier="id" />
  <scramble type="Email" tableName="Person" columnName="Email" identifier="id" />
  <scramble type="NationalNumber" tableName="Person" columnName="NationalNumber" identifier="id" />
  <scramble type="PhoneNumber" tableName="Person" columnName="PhoneNumber" identifier="id" />
  <scramble type="PhoneNumber" tableName="Person" columnName="CellPhoneNumber" identifier="id" />
  <scramble type="Birthdate" tableName="Person" columnName="Birthdate" identifier="id" />
  <scramble type="Zipcode" tableName="Person" columnName="Zipcode" identifier="id" />
  <scramble type="City" tableName="Person" columnName="City" identifier="id" />
  <scramble type="Address" tableName="Person" columnName="Address" identifier="id" />
  <scramble type="ContractNumber" tableName="Contract" columnName="ContractNumber" identifier="id" />

  <!-- To clear a column or table, use these types -->
  <scramble type="ClearColumn" tableName="User" columnName="ExternalSystemUniqueIdentifier" />
  <scramble type="ClearTable" tableName="Document" />
    
  <!-- To copy the value from a sourceColumn 'Id' to a targetColumn 'ExternalIdentifier', use CopyColumn -->
  <scramble type="CopyColumn" tableName="Person" columnName="ExternalIdentifier" copyColumn="Id" identifier="id" />

  <!-- To overwrite content, use set-content
       BEWARE: value tag allows you to write any SQL-type; nvarchar, datetime, binary, ...
       so use single quotes when setting a nvarchar -->
  <scramble type="SetContent" tableName="User" columnName="Password" value="'secret'" />
</root>
```
