# database-scrambler
Provide a way to scramble your offline SQL database with configuration

# usage
```bash
DatabaseScrambler.exe "<connection-string>" ScrambleConfiguration.xml
```

# configuration
Sample configuration:
```xml
<?xml version="1.0" encoding="utf-8" ?>
<root>
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

  <!-- To overwrite content, use set-content -->
  <scramble type="SetContent" tableName="User" columnName="Password" Value="secret" />
</root>
```