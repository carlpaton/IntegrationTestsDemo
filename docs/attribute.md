# Attribute 

Namespace `Microsoft.AspNetCore.Mvc`

## FromBody

Controller

```c#
[HttpPost]
public async Task<ActionResult<Artist>> AddArtist([FromBody] Artist artist)
{
    ...
}
```

Postman collection -> Body, Raw, Json

```json
{
    "Id": "f22e850e-4b31-417d-bc4b-4813ba98a50c",
    "Name": "Carl Paton"
}
```

## FromForm

Controller

```c#
[HttpPut]
public async Task<ActionResult<Artist>> UpdateArtist([FromForm] Guid id, [FromForm] string name)
{
    ...
}
```

Postman collection -> Body, form-data

```
KEY             VALUE
-------------------------------------------------------
Id              f22e850e-4b31-417d-bc4b-4813ba98a50c
Name            Carlos Poephole
```

## FromRoute

Controller

```c#
[HttpPut]
public async Task<ActionResult<Artist>> UpdateArtist([FromRoute] Guid sweetId)
{
    ...
}
```

Postman collection -> PUT

```
http://localhost:5000/foo/{{sweetId}}/bar/{{id}}
```

## FromQuery

```c#
public async Task<ActionResult<List<Artist>>> SearchArtist([FromQuery(Name = "q")] string searchOn)
{
    ...
}
```

Postman collection -> GET

```
KEY             VALUE
-------------------------------------------------------
q               bob marley
```