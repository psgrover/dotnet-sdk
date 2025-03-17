# Step-by-Step Guide: Building a Clean Architecture Web API with AATs

This guide walks you through creating a **Clean Architecture Web API** using **.NET 9** and integrating **Automated Acceptance Testing (AATs)** with the `.NET SDK`. AATs to validate business rules before deployment. CI/CD only allows production deployment if AATs pass!

## **1️⃣ Create the Solution & Projects**
Run the following commands to set up a clean architecture project structure:

```sh
dotnet new sln -n SampleAATWebAPI
cd SampleAATWebAPI

dotnet new webapi -n WebApi

dotnet new classlib -n Core

dotnet new classlib -n Infrastructure

dotnet new xunit -n Tests

dotnet sln add WebApi Core Infrastructure Tests
```

## **2️⃣ Implement Clean Architecture Layers**

### **Core (Domain Layer)**
- Define entities, interfaces, and business logic.
- Example `Product.cs`:

```csharp
namespace Core.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

```

### **Infrastructure (Data Layer)**
- Implement persistence, repositories, and external integrations.
- Example `ProductRepository.cs`:

```csharp
using Core.Entities;
using System.Collections.Generic;

namespace Infrastructure.Persistence;

public class ProductRepository
{
    private static readonly List<Product> Products = new();

    public void Add(Product product)
    {
        product.Id = Products.Count + 1;
        Products.Add(product);
    }

    public Product GetById(int id) => Products.Find(p => p.Id == id);
}

```

### **Web API (Application Layer)**
- Define controllers and services.
- Example `ProductsController.cs`:

```csharp
using Core.Entities;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly ProductRepository _repository = new();

    [HttpPost]
    public IActionResult CreateProduct([FromBody] Product product)
    {
        _repository.Add(product);
        return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct(int id)
    {
        var product = _repository.GetById(id);
        return product != null ? Ok(product) : NotFound();
    }
}

```

## **3️⃣ Define AATs Using DSL**
Create `tests/ProductTests.dsl`:
```dsl
Feature: Product API Testing

Scenario: Create a new product
    Given a product with name "Laptop" and price 1200.00
    When the product is created via API
    Then the product should be available with a generated ID
```

## **4️⃣ Generate & Run AATs**
Generate AAT tests:
```sh
dotnet aat generate --input tests/ProductTests.dsl --output tests/ProductTests.cs
```
Run the tests:
```sh
dotnet test --filter Category=AAT
```

## **5️⃣ Set Up CI/CD to Run AATs**
Create `.github/workflows/build-and-test.yml`:

```yaml
name: Build and Deploy with AATs

on:
  push:
    branches:
      - main
  pull_request:
    branches:
      - main

jobs:
  build-and-test:
    runs-on: ubuntu-latest
    steps:
      - name: Checkout code
        uses: actions/checkout@v3

      - name: Setup .NET 9
        uses: actions/setup-dotnet@v3
        with:
          dotnet-version: '9.0.x'

      - name: Restore dependencies
        run: dotnet restore

      - name: Build the project
        run: dotnet build --configuration Release --no-restore

      - name: Generate AATs
        run: dotnet aat generate --input tests/ProductTests.dsl --output tests/ProductTests.cs

      - name: Run AATs
        run: dotnet test --filter Category=AAT

      - name: Deploy to Production
        if: success()
        run: echo "Deploying to production..." # Replace with actual deployment script
```
