# Sonatype OSS Index

This is a free API that shows vulnerabilities in packages, it supports a few types like nuget and npm.

## Example Nuget packages

* https://www.nuget.org/packages/log4net/

## Example Endpoint

The GET call needs the `coordinates` of the package which is built up as follows

```
model.Type = nuget
model.Namespace =      // namespace is an optional value
model.Name = log4net
model.Version = 1.2.10

var coordinates = $"pkg:{model.Type}/{model.Namespace}{model.Name}@{model.Version}";
```

Example coordinates `pkg:nuget/log4net@1.2.10`

The base URL is:

```
var endPoint = $"https://ossindex.sonatype.org/api/v3/component-report/{coordinates}";
```

The complete URL would be

```
https://ossindex.sonatype.org/api/v3/component-report/pkg:nuget/log4net@1.2.10
```

### Example GET response

* GET https://ossindex.sonatype.org/api/v3/component-report/pkg:nuget/log4net@2.0.14

The version `2.0.14` has no vulnerabilities at the time of this post.

```
{
    "coordinates": "pkg:nuget/log4net@2.0.14",
    "description": "log4net is a tool to help the programmer output log statements to a variety of output targets. log4net is a port of the excellent log4j framework to the .NET runtime",
    "reference": "https://ossindex.sonatype.org/component/pkg:nuget/log4net@2.0.14?utm_source=postmanruntime&utm_medium=integration&utm_content=7.28.4",
    "vulnerabilities": []
}
```

* GET https://ossindex.sonatype.org/api/v3/component-report/pkg:nuget/log4net@1.2.10

The version `1.2.10` has 1 known vulnerability at the time of this post.

```
{
    "coordinates": "pkg:nuget/log4net@1.2.10",
    "description": "log4net is a tool to help the programmer output log statements to a variety of output targets. log4net is a port of the excellent log4j framework to the .NET runtime",
    "reference": "https://ossindex.sonatype.org/component/pkg:nuget/log4net@1.2.10?utm_source=postmanruntime&utm_medium=integration&utm_content=7.28.4",
    "vulnerabilities": [
        {
            "id": "c4ac70fa-d3ce-4153-b4e9-e1a9d193be8c",
            "displayName": "CVE-2018-1285",
            "title": "[CVE-2018-1285] Apache log4net before 2.0.8 does not disable XML external entities when parsing ...",
            "description": "Apache log4net before 2.0.8 does not disable XML external entities when parsing log4net configuration files. This could allow for XXE-based attacks in applications that accept arbitrary configuration files from users.",
            "cvssScore": 9.8,
            "cvssVector": "CVSS:3.0/AV:N/AC:L/PR:N/UI:N/S:U/C:H/I:H/A:H",
            "cve": "CVE-2018-1285",
            "reference": "https://ossindex.sonatype.org/vulnerability/c4ac70fa-d3ce-4153-b4e9-e1a9d193be8c?component-type=nuget&component-name=log4net&utm_source=postmanruntime&utm_medium=integration&utm_content=7.28.4",
            "externalReferences": [
                "https://nvd.nist.gov/vuln/detail/CVE-2018-1285"
            ]
        }
    ]
}
```

## References

- https://ossindex.sonatype.org/