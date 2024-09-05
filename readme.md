# General Information

This is a result pattern implementation. While this is nothing new, as I started to this project, there were a lot of variants. I've combined existing libraries and added some use case specific changes to them for my taste. 

# Functionalities

1. I want it to be assignable from any object

    For example:
    ```csharp
    TestClass test = new TestClass();
    Maybe<TestClass> maybe = test;
    TestClass fromMaybe = maybe.Value; 
    ```

2. I want it to be assignable from error

    ```csharp
    var error = Error.Invalid();
    Maybe<TestClass> maybe = error;
    var isError = maybe.IsError; // true
    var error = maybe.Error; // Error.Invalid();
    ```

3. I want validation errors to be seperated from regular errors. 
Validation errors are the ones that should cause immediate failure before business logic executes.
While normal validation error can happen on business logic as well, but generally it creates different error messages to the client

    ```csharp
    var errors = [ValidationError.From("...","..."), ValidationError.From("...","...")];
    Maybe<TestClass> maybe = errors;
    var isError = maybe.IsInvalid; // true;
    var validationErrors = maybe.ValidationErrors; // Error.Invalid();
    ```
