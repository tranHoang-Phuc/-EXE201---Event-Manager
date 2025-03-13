using EventManagerAPI.Configuration;
using EventManagerAPI.DTO.Request;
using EventManagerAPI.Mapper;
using EventManagerAPI.Models;
using EventManagerAPI.Repository.Implementations;
using EventManagerAPI.Repository.Interfaces;
using EventManagerAPI.Services.Implementations;
using EventManagerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;


namespace EventManagerAPI
{
    public class Startup
	{
		private IConfiguration _configuration;

		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllers();
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp",
                    policy =>
                    {
                        policy.WithOrigins("http://eventset.online", "http://localhost:3000'") 
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials();
                    });
            });
            services.AddDbContext<EventSetDbContext>(option =>
			{
				option.UseSqlServer(_configuration.GetConnectionString("DemoConnectStr"));
			});
            services.Configure<EmailSettings>(_configuration.GetSection("EmailSettings"));

            services.AddAutoMapper(typeof(UserMapper), typeof(RoleMapper));
			InjectService(services);
			ConfigureJWT(services);


		}

		//add services
		private void InjectService(IServiceCollection services)
		{
            //add services repository pattern
            services.AddTransient<IApplicationInitConfigService, ApplicationInitConfig>();
			services.AddScoped<IUserRepository, UserRepository>();
			services.AddScoped<IRoleRepository, RoleRepository>();

			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IRoleService, RoleService>();

			services.AddScoped<UserManager<AppUser>>();
            services.AddScoped<RoleManager<Role>>();

            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<Services.Interfaces.IAuthenticationService, Services.Implementations.AuthenticationService>();

			//Mail
            services.AddScoped<IEmailService, EmailService>();

			// Event 
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IEventService, EventService>();

			// Agenda
            services.AddScoped<IAgendaRepository, AgendaRepository>();
            services.AddScoped<IAgendaService, AgendaService>();

			// Event Participant
            services.AddScoped<IEPRepository, EPRepository>();
            services.AddScoped<IEPService, EPService>();

            // Category Participant
            services.AddScoped<IECRepository, ECRepository>();
            services.AddScoped<IECService, ECService>();

        }

		private void ConfigureJWT(IServiceCollection services)
		{
			services.Configure<AuthenticationRequest>(_configuration.GetSection("Jwt"));
			//services.Configure<AdminAccount>(_configuration.GetSection("AdminAccount"));
			services.AddIdentity<AppUser, Role>()
				.AddEntityFrameworkStores<EventSetDbContext>()
				.AddDefaultTokenProviders();
			services.AddIdentityCore<AppUser>();
			//services.AddIdentityCore<Role>();

			services.Configure<IdentityOptions>(op =>
			{

				op.Password.RequireDigit = false;
				op.Password.RequireLowercase = false;
				op.Password.RequireNonAlphanumeric = false;
				op.Password.RequireUppercase = false;
				op.Password.RequiredLength = 3;

			
				op.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
				op.Lockout.MaxFailedAccessAttempts = 3;
				op.Lockout.AllowedForNewUsers = true;

				op.User.RequireUniqueEmail = true;
                op.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
         

            });

            services.AddAuthentication(op =>
			{
				op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				op.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

			}).AddJwtBearer(options =>
			{
				options.SaveToken = true;
				options.RequireHttpsMetadata = false;
				options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = _configuration["Jwt:Issuer"],
					ValidAudience = _configuration["Jwt:Issuer"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SignerKey"]))
				};
			});

		}

		public void Configure(WebApplication app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();
			app.UseSwagger();
			app.UseSwaggerUI();
            app.UseCors("AllowReactApp");
            app.UseAuthentication();
			app.UseAuthorization();


			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
			var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider.GetServices<IApplicationInitConfigService>();
			foreach (var service in services)
			{
				service.applicationRunner().GetAwaiter().GetResult();
			}
			app.Run();
		}
	}
}

