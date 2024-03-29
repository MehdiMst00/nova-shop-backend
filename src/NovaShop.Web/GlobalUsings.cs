﻿global using System.Net;
global using System.ComponentModel.DataAnnotations;
global using NovaShop.Web;
global using NovaShop.Web.Filters;
global using NovaShop.Web.Controllers.Base;
global using NovaShop.Web.Configurations;
global using NovaShop.Web.ApiModels.Catalogs;
global using NovaShop.Web.ApiModels.Auth;
global using NovaShop.Web.ApiModels.Paging;
global using NovaShop.Web.ApiModels.Order;
global using NovaShop.Infrastructure;
global using NovaShop.Infrastructure.Data;
global using NovaShop.Infrastructure.Identity.Users;
global using NovaShop.Infrastructure.Identity.Services;
global using NovaShop.Infrastructure.Identity.Extensions;
global using NovaShop.ApplicationCore;
global using NovaShop.ApplicationCore.CatalogAggregate;
global using NovaShop.ApplicationCore.CatalogAggregate.Specification;
global using NovaShop.ApplicationCore.CustomerAggregate.Commands;
global using NovaShop.ApplicationCore.OrderAggregate.Commands.AddToOrder;
global using NovaShop.ApplicationCore.OrderAggregate.Commands.DeleteOrderDetail;
global using NovaShop.ApplicationCore.OrderAggregate.Commands.UpdateOrderDetail;
global using NovaShop.ApplicationCore.OrderAggregate.Queries.GetCustomerBasket;
global using NovaShop.SharedKernel.Interfaces;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.Extensions.Options;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc.Filters;
global using NovaShop.ApplicationCore.Exceptions;
global using NovaShop.Web.ActionResults;
global using Swashbuckle.AspNetCore.Annotations;
global using Autofac.Extensions.DependencyInjection;
global using Autofac;
global using AutoMapper;
global using MediatR;