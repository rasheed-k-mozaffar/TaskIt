var builder = WebApplication.CreateBuilder(args);

//Getting The connection strings required for the databases , and storing them in variables for later use
var applicationConnectionStrings = builder.Configuration.GetConnectionString("ApplicationDBConnection");
var usersConnectionStrings = builder.Configuration.GetConnectionString("UsersDBConnection");

//Adding services to the container
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//Registering the Db Context as a service
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(applicationConnectionStrings));
builder.Services.AddDbContext<UsersDbContext>(options => options.UseSqlServer(usersConnectionStrings , options => options.MigrationsAssembly("TaskIt.Web")));

//Adding Identity using the users db context class that inherits the identity db context
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<UsersDbContext>();

//Configuring the applications's cookies to that all required login request are redirected to the login page
builder.Services.ConfigureApplicationCookie(config => config.LoginPath = "/User/SignIn");

//Adding repositories as services to the container so we can access them through DI
//This appraoch allows us to write cleaner code and not let the controllers handle the business logic
//Plus adding more separation while allocating different errands to various parts of the app.
builder.Services.AddTransient<ITodoRepository, TodoRepository>();



var app = builder.Build();



app.MapDefaultControllerRoute();
app.UseStaticFiles();

//Adding Authentication and authorization middlewares to the request pipeline
app.UseAuthentication();
app.UseAuthorization();

app.Run();

