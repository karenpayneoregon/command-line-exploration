# About

This code sample shows how to take two arguments into the application expecting a user name and a user password.

The user data is stored in a Dictionary, key is the user name and value is the user’s last name for their password. Note that the dictionary has been setup as case insensitive.

In a real world application the user name and password would not be hard-coded but stored in a secure container such as an encrypted file or secure table in a database.

**Install**

```
dotnet tool install --global --add-source ./nupkg PaynePromptForUserNamePassword
```

**Uninstall**

```
uninstall  -g PaynePromptForUserNamePassword
```


# Requires

:heavy_check_mark: NuGet package [CommandLineParser](https://www.nuget.org/packages/CommandLineParser).