using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TaskModel = WebAPI.Models.Task;
using WebAPI.Models;
using WebAPI.Services;
using Microsoft.AspNet.Identity;
using WebAPI.Services.TokenGenerators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI.Services.TokenValidators;
using WebAPI.Services.RefreshTokenRepositories;
using WebAPI.Services.Authenticators;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;
using WebAPI.Services.ControllerServices;

namespace WebAPI {
	public class Startup {
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		private readonly IConfiguration _configuration;
		public Startup(IConfiguration configuration) {
			_configuration = configuration;

		}
		public void ConfigureServices(IServiceCollection services) {

			AuthenticationConfiguration authenticationConfiguration = new AuthenticationConfiguration();
			_configuration.Bind("Authentication", authenticationConfiguration);

			string connection = _configuration.GetConnectionString("DefaultConnection");
			services.AddDbContext<TaskManagementDBContext>(options =>
				options.UseSqlServer(connection));
			services.AddSingleton(authenticationConfiguration);
			services.AddScoped<Authenticator>();
			services.AddScoped<IAccountService, AccountDataService>();
			services.AddScoped<IAccountServiceForController, AccountServiceForController>();
			services.AddScoped<IAuthenticationServiceForController, AuthenticationServiceForController>();
			services.AddScoped<IIssueServiceForController, IssueServiceForController>();
			services.AddScoped<ITaskServiceForController, TaskServiceForController>();
			services.AddScoped<IPasswordHasher, PasswordHasher>();
			services.AddScoped<AccessTokenGenerator>();
			services.AddScoped<RefreshTokenGenerator>();
			services.AddScoped<RefreshTokenValidator>();
			services.AddScoped<TokenGenerator>();
			services.AddScoped<GenericDataService<Issue>>();
			services.AddScoped<IDataService<Issue>, IssueDataService>();
			services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
			services.AddScoped<IDataService<TaskModel>, GenericDataService<TaskModel>>();
			services.AddControllers().AddNewtonsoftJson(options =>
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
			);

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o => {
				o.TokenValidationParameters = new TokenValidationParameters() {
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationConfiguration.AccessTokenSecret)),
					ValidIssuer = authenticationConfiguration.Issuer,
					ValidAudience = authenticationConfiguration.Audience,
					ValidateIssuerSigningKey = true,
					ValidateIssuer = true,
					ValidateAudience = true,
					ClockSkew = TimeSpan.Zero
				};
			});

			services.AddSwaggerGen(c => {
				var securitySchema = new OpenApiSecurityScheme {
					Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.Http,
					Scheme = "bearer",
					Reference = new OpenApiReference {
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					}
				};

				c.AddSecurityDefinition("Bearer", securitySchema);

				var securityRequirement = new OpenApiSecurityRequirement
				{
					{ securitySchema, new[] { "Bearer" } }
				};

				c.AddSecurityRequirement(securityRequirement);

				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c =>
				{
					c.SwaggerEndpoint("v1/swagger.json", "MyAPI V1");
				});
			}

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseEndpoints(endpoints => {
				endpoints.MapControllers();
			});
		}
	}
}
