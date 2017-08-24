# AspNetCoreRouteDebugger
An ASP.NET Core Route Debugger implemented as a Razor Page. Read more about [Debugging ASP.NET Core Routes](https://ardalis.com/debugging-aspnet-core-routes).

## Sample

Clone the rep. Run the sample project. Navigate to /routes. You should see a list of routes. Note that Razor Pages don't specify controllers/actions for their routes.

## Adding to your Project

Grab the two files from the RouteDebuggerPage folder. Drop them into a Pages folder in your ASP.NET Core app (2.0 or greater). Rename if desired (and lock down from public access). Fix namespaces if desired. That should be all you need to do!

### I'm Not Using Razor Pages!

Grab the Routes2Controller.cs file, which currently returns JSON. Write a simple page that consumes the JSON (via an API call), or modify the Routes.cshtml file to be a view and have the controller return a view instead. Pull requests accepted if someone wants to make this a bit cleaner.

#### Original Idea

Credit for the idea behind this sample goes to [this issue](https://github.com/aspnet/Mvc/issues/6330).
