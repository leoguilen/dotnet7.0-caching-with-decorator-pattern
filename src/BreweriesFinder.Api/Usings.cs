global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.Diagnostics.CodeAnalysis;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;

global using BreweriesFinder.Api.Abstractions;
global using BreweriesFinder.Api.Enums;
global using BreweriesFinder.Api.Extensions;
global using BreweriesFinder.Api.Infra.Caching.Configurations;
global using BreweriesFinder.Api.Infra.Caching.Entries;
global using BreweriesFinder.Api.Infra.Caching.Enums;
global using BreweriesFinder.Api.Infra.Caching.Extensions;
global using BreweriesFinder.Api.Infra.ExternalServices.Decorators;
global using BreweriesFinder.Api.Infra.ExternalServices.Extensions;
global using BreweriesFinder.Api.Infra.ExternalServices.Models;
global using BreweriesFinder.Api.Infra.ExternalServices.Services;
global using BreweriesFinder.Api.Models.Requests;
global using BreweriesFinder.Api.Models.Responses;

global using Microsoft.AspNetCore.WebUtilities;
global using Microsoft.Extensions.Caching.Distributed;
global using Microsoft.Extensions.Caching.Memory;
global using Microsoft.Extensions.Options;

global using StackExchange.Redis;

global using static System.Text.Json.JsonSerializer;

global using ICacheEntry = BreweriesFinder.Api.Abstractions.ICacheEntry;
