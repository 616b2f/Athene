# Contributing Guidelines

## Basics

- Use 4 spaces of indentation (no tabs)
- Use `_camelCase` for private fields
- Always specify member visiblity, even if it's the default (i.e. `private string _foo;` not `string _foo;`)
- Always write method parameters and variables in `camelCase`

### Use only one empty line

#### Correct
```csharp
    var cornelsen = new Publisher { Name = "Cornelsen Verlag GmbH" };
    var ernstKlett = new Publisher { Name = "Ernst Klett Verlag GmbH" };

    var robertCMartin = new Author { FullName = "Robert C. Martin" };
    var deanWampler = new Author { FullName = "Dean Wampler" };
```
#### Incorrect
```csharp
    var cornelsen = new Publisher { Name = "Cornelsen Verlag GmbH" };
    var ernstKlett = new Publisher { Name = "Ernst Klett Verlag GmbH" };


    var robertCMartin = new Author { FullName = "Robert C. Martin" };
    var deanWampler = new Author { FullName = "Dean Wampler" };
```
