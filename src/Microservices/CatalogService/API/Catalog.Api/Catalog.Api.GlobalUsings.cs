// Global using directives

global using System.Net;
global using Catalog.Api.CustomMiddleware.Shared;
global using Catalog.Application;
global using Catalog.Application.Exceptions;
global using Catalog.Application.Features.Product.Commands.CreateProduct;
global using Catalog.Application.Features.Product.Commands.DeleteProduct;
global using Catalog.Application.Features.Product.Commands.UpdateProduct;
global using Catalog.Application.Features.Product.Queries.Common;
global using Catalog.Application.Features.Product.Queries.GetProduct;
global using Catalog.Application.Features.Product.Queries.GetProducts;
global using Catalog.Application.Features.Product.Queries.GetProductsByCategoryName;
global using Catalog.Application.Features.Product.Queries.GetProductsByName;
global using Catalog.Persistence;
global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Server.Kestrel.Core;