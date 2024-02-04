# EmployeeInsight

## Project structure
- EmployeeInsight.ChartGeneratorAPI - REST API for creating pie chart as image in .png format for visualizing employee data.
- EmployeeInsight.Crawler - Shared class library for gathering employee data from remote API.
- EmployeeInsight.DataVisualizerMVC - MVC app for visualizing employee data in HTML.
- 

## Visualizing employee data using HTML
<img width="960" alt="table" src="https://github.com/NebojsaMarjanovic/Readme/assets/74599737/0d699504-c249-4058-a37d-f05ede64e1c8">

## Visualizing employee data with pie chart
![piechart(7)](https://github.com/NebojsaMarjanovic/Readme/assets/74599737/cf80d108-4a4b-441c-a61f-f46f51e9b85b)

<i>Note:</i> Currently, there is no library available for saving charts as .png image files in ASP.NET Core REST API. However, I managed to create pie charts using the [System.Drawing](https://learn.microsoft.com/en-us/dotnet/api/system.drawing.graphics.drawpie?view=dotnet-plat-ext-8.0) library.


## References
 - [How to add a caching layer with Decorator pattern and Scrutor ](https://www.code4it.dev/blog/caching-decorator-with-scrutor/)
 - [Implement resilient Http client](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/implement-http-call-retries-exponential-backoff-polly)
 - [Drawing pie charts with System.Drawing](https://learn.microsoft.com/en-us/dotnet/api/system.drawing.graphics.drawpie?view=dotnet-plat-ext-8.0)

