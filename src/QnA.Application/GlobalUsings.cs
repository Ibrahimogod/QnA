global using System.Text;
global using System.Security.Claims;
global using System.IdentityModel.Tokens.Jwt;
global using System.Linq.Expressions;

global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Design;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;

global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Http;

global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Caching.Memory;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;

global using Microsoft.IdentityModel.Tokens;

global using IdentityModel;

global using MediatR;

global using QnA.Core;
global using QnA.Core.Domains.Questions;
global using QnA.Core.Domains.Answers;
global using QnA.Core.Domains.Votes;
global using QnA.Core.Domains.Users;

global using QnA.Application.Common;
global using QnA.Application.Data;
global using QnA.Application.Data.Repositories;
global using QnA.Application.Models;
global using QnA.Application.Models.Options;
global using QnA.Application.Services;


