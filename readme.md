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

3. I want it to be assignable from list of errors

    ```csharp
    var errors = [Error.Invalid(), Error.Forbidden()];
    Maybe<TestClass> maybe = errors;
    var isError = maybe.IsError; // true;
    var error = maybe.Error; // Error.Invalid();
    var isInvalid = maybe.Status == ErrorStatus.Invalid; // true;
    var isForbidden = maybe.Status == ErrorStatus.Forbidden; // true;
    ```
   
4. I want it to be assignable from generic

    ```csharp
    TestClass testClass = new TestClass();
    Maybe<TestClass> maybeTyped = testClass;
    Maybe rawMaybe = maybeTyped;
    var isErrorTyped = maybeTyped.IsError; // false;
    var isErrorRaw = maybeRaw.IsError; // false;
    var resultTyped = maybeTyped.Value; // testClass;
    var resultRaw = maybeRaw.Value; // ResultStatus.Ok;
    ```

5. I want to map the result to action result with fluent operators

   ```csharp
   public IActionResult Test(){
        TestClass testClass = new TestClass();
        Maybe<TestClass> maybeTyped = testClass;
        return maybeTyped.On(ResultStatus.Ok, ()=>
            {
                return Page();
            }).On(ResultStatus.Invalid, ()=>
            {
                ModelState.AddModelError("Error");
                return Page();
            }).On(ResultStatus.NotFound, ()=>NotFound());
   }
   ```