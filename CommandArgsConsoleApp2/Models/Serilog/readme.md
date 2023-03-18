# About

Classes which represent a section for `SerilLog` in appsettings.`Environment`.json

```json
"Serilog": {
  "SinkOptions": {
    "batchPostingLimit": 5,
    "batchPeriod": "00:00:15",
    "eagerlyEmitFirstEvent": true
  },
  "ColumnOptions": {
    "addStandardColumns": [ "LogEvent" ],
    "removeStandardColumns": [ "MessageTemplate","Properties" ],
    "timeStamp": {
      "columnName": "Timestamp",
      "convertToUtc": false
    }
  }
}
```

Example usage `SetupLogging.cs`

```csharp
ColumnOptions columnOptions = configuration.GetSection("Serilog").Get<ColumnOptions>();
var timeStamp = columnOptions.TimeStamp.ColumnName;
```