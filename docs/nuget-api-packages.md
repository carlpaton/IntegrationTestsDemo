# Nuget API

The [service index](https://api.nuget.org/v3/index.json) is the entry point for a NuGet package source. Looking at the document the `SearchQueryService` has the comment `Query endpoint of NuGet Search service (primary) that supports package type filtering` and is avalible at https://azuresearch-usnc.nuget.org/query which seems like it will work for my application.

## Search Query Service

The package [log4net](https://www.nuget.org/packages/log4net) felt like a good candidate to pick on:

* GET https://azuresearch-usnc.nuget.org/query?q=log4net

Pruning down the results I can see the following data should be useful and matched my search of `log4net`

```json
{
	"data": [{
		"id": "log4net",
		"version": "2.0.14",
		"description": "log4net is a tool to help the programmer output log statements to a variety ....",
		"totalDownloads": 107054643,
		"versions": [{
			"version": "1.2.10",
			"downloads": 5725024,
			"@id": "https://api.nuget.org/v3/registration5-semver1/log4net/1.2.10.json"
		}, {
			"version": "2.0.0",
			"downloads": 2961996,
			"@id": "https://api.nuget.org/v3/registration5-semver1/log4net/2.0.0.json"
		}]
	}, {
		"id": "Microsoft.ApplicationInsights.Log4NetAppender",
		"version": "2.20.0",
		"description": "Application Insights Log4Net Appender is a customer appender allowing you to send Log4Net log messages to Application ...",
		"totalDownloads": 7938163
	}]
}
```

## References

- https://docs.microsoft.com/en-us/nuget/api/service-index
- https://docs.microsoft.com/en-us/nuget/api/search-query-service-resource