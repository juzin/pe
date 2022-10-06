## Tasks 1. and 3.

- Install latest dotnet sdk .NET6 https://dotnet.microsoft.com/en-us/download/visual-studio-sdks

## For UI test
- Open TestProject solution and build project
- In bin folder there will be available powershell script "playwright.ps1"
- Run following command which will install playwright, examle:
```bash
pwsh /Users/currentUser/Projects/TestProject/bin/Debug/net6.0/playwright.ps1 install
```
- Run tests
```bash
dotnet test
```

## Task 2

- Is python script "app.py" located in "Counter" folder