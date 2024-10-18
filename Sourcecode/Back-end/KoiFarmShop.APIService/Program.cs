﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using KoiFarmShop.Data;
using Microsoft.OpenApi.Models;
using KoiFarmShop.Business.Business.TokenBusiness;
using KoiFarmShop.Business.Business.UserBusiness;
using KoiFarmShop.Business.Business.AccountBusiness;
using KoiFarmShop.Business.AutoMap;
using KoiFarmShop.Business.Business.KoiBusiness;
using KoiFarmShop.Business.Business.KoiTypeBusiness;
using KoiFarmShop.Business.Business.ConsignmentBusiness;
using KoiFarmShop.Business.Business.PromotionBusiness;

namespace KoiFarmShop.APIService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddAuthorization();

            builder.Services.AddAuthorization(options =>
            {
                // Chính sách cho vai trò manager
                options.AddPolicy("Manager", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleClaim = user.FindFirst("Role");
                        if (roleClaim != null && roleClaim.Value == "1")
                        {
                            return true;
                        }
                        return false;
                    });
                });

                // Chính sách cho vai trò customer
                options.AddPolicy("Customer", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleClaim = user.FindFirst("Role");
                        if (roleClaim != null && roleClaim.Value == "2")
                        {
                            return true;
                        }
                        return false;
                    });
                });

                // Chính sách cho vai trò staff
                options.AddPolicy("Staff", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleClaim = user.FindFirst("Role");
                        if (roleClaim != null && roleClaim.Value == "3")
                        {
                            return true;
                        }
                        return false;
                    });
                });

                // Chính sách cho các tài nguyên mà cả manager và customer có thể truy cập
                options.AddPolicy("AdminOrCustomerAccessPolicy", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleClaim = user.FindFirst("Role");
                        if (roleClaim != null && (roleClaim.Value == "1" || roleClaim.Value == "2"))
                        {
                            return true;
                        }
                        return false;
                    });
                });

                // Chính sách cho các tài nguyên mà cả manager, customer, và staff đều có thể truy cập
                options.AddPolicy("AdminCustomerOrShipperAccessPolicy", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleClaim = user.FindFirst("Role");
                        if (roleClaim != null && (roleClaim.Value == "1" || roleClaim.Value == "2" || roleClaim.Value == "3"))
                        {
                            return true;
                        }
                        return false;
                    });
                });
            });


            // builder.Services.AddSwaggerGen();
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter the JWT token obtained from the login endpoint",
                    Name = "Authorization"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
            builder.Services.AddAuthentication(item =>
            {
                item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(item =>
            {
                item.RequireHttpsMetadata = true;
                item.SaveToken = true;
                item.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("c2VydmVwZXJmZWN0bHljaGVlc2VxdWlja2NvYWNoY29sbGVjdHNsb3Bld2lzZWNhbWU=")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });


            //add automapper
            builder.Services.AddAutoMapper(typeof(MappingProfile));

            //builder.Services.AddScoped<FA_SE1854_SWP391_G3_KoiFarmShopContext>();
            builder.Services.AddScoped<UnitOfWork>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAccountService, AccountService>();

            builder.Services.AddScoped<IPromotionService, PromotionService>();

            builder.Services.AddScoped<IKoiService,KoiService>();
            builder.Services.AddScoped<IKoiTypeService ,KoiTypeService>();

            builder.Services.AddScoped<IConsignmentRequestService, ConsignmentRequestService>();
            builder.Services.AddScoped<IConsignmentDetailService, ConsignmentDetailService>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            var app = builder.Build();

            //exception handler
            app.UseMiddleware<GlobalExceptionHandler>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
