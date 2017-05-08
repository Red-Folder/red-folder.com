In this article, I look at how to pull in additional projects for use with Azure Functions.

I've used this method both locally and with Visual Studio Team Servers.

## This is the wrong way to do it
Ok, this works, but it is the wrong way to do it.

See these posts which talk about changing over to a class library - which is likely to closer to the roadmap that Microsoft are taking:

* [Azure Functions Tools Roadmap](https://blogs.msdn.microsoft.com/webdev/2017/04/14/azure-functions-tools-roadmap/)
* [Publishing a .NET class library as a Function App](https://blogs.msdn.microsoft.com/appserviceteam/2017/03/16/publishing-a-net-class-library-as-a-function-app/)

I will look at reporting on this method in the next article.

In this article however I want to summarise the method I used - which works - but should be consider depreciated.

## The .funproj method
This assumes that your Azure Functions project is using the .funproj template.

For me, I have a Visual Studio Solution which includes a functions project and a standard class library.

All of my "logic" is within my class library - the functions project is a very simple wrapper over that code.  This is great because the class library has considerably better tooling that the functions project.

All I need to do, is compile the class library and copy all the .dll files in \bin folder of my function (in this case called GithubWebhook within the DocFunctions project).

Easily accomplished with a Post Build event on the class library:

```
if exist "$(SolutionDir)src\DocFunctions\GithubWebhook\bin" (
   echo "Folder already exists"
) else (
   mkdir "$(SolutionDir)src\DocFunctions\GithubWebhook\bin"
)
copy /Y "$(TargetDir)*.dll" "$(SolutionDir)src\DocFunctions\GithubWebhook\bin"
```

I also have a Pre Build event to clear any previous files:

```
if exist "$(SolutionDir)src\DocFunctions\GithubWebhook\bin\*.*" (
   del /Q "$(SolutionDir)src\DocFunctions\GithubWebhook\bin\*.*"
)
```

And that is about it.

## Continuous Delivery
And bascially it all works for my VSTS - it's just built and then bundled up via nuget (see my [Azure Functions Continuous Delivery article](/blog/azure-functions-continuous-delivery)).

## And probably best not to do it that way
And as I say, this probably shouldn't be used.

In the next article I'll look at how to convert over to the new recommended approach.