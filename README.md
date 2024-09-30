# Qu Challenge

## Assumptions

It was not completely clear to me from the PDF if what needs to be counted is the number of times a word appears in the matrix or the number of times it appears on the word stream.
Please take into account that I did an implementation using the latter.
(In a real world scenario I would have checked this with the corresponding stakeholders).

I created a Console app that runs a sample in order to add depency injection, configuration and logging which would be in place in a real world scenario.

## Instructions

1. Unzip the file, open a terminal and CD to solution folder

2. Build the project

```bash
dotnet build
```

3. Run the tests
 
```bash
dotnet test
```

4. Optionally run the console project that contains some sample data and prints the output

```bash
dotnet run --project .\QuChallenge.Console\QuChallenge.Console.csproj
```
