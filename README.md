<div dir="ltr">

# 🥇Professional REST API design with ASP.NET Core WebAPI

This project is an example of lightweight and extensible infrastructure for building RESTful Web API with ASP.NET Core.

This example contains a number of tricks and techniques that is the result of many years of my experience in WebAPI/RESTful programming in ASP.NET Core

If you want a total deep dive on REST, API security, ASP.NET Core and much more, check out my [Course](http://beyamooz.com/project-based-aspnet/%D8%AF%D9%88%D8%B1%D9%87-api-%D9%86%D9%88%DB%8C%D8%B3%DB%8C-%D8%A7%D8%B5%D9%88%D9%84%DB%8C-%D9%88-%D8%AD%D8%B1%D9%81%D9%87-%D8%A7%DB%8C-%D8%AF%D8%B1-asp-net-core).

## Testing it out
1. Clone or download this repository
2. Build the solution using command line with `dotnet build`
3. Go to **MyApi** directory and run project using command line with `dotnet run`
4. Browse to this url https://localhost:5001/swagger to see SwaggerUI page

## Techniques and Features
- JWT Authentication
- Secure JWT using Encryption (JWE)
- Logging to File, Console and Database using [Elmah](https://github.com/ElmahCore/ElmahCore) & [NLog](https://github.com/NLog/NLog.Web)
- Logging to [sentry.io](sentry.io) (Log Management System)
- Exception Handling using Custom Middleware
- Automatic Validation
- Standard API Resulting
- Dependency Injection using [Autofac (Ioc Container)](https://github.com/autofac/Autofac)
- Map resources using [AutoMapper](https://github.com/AutoMapper/AutoMapper)
- Async/Await Best Practices
- Versioning Management
- Using [Swagger](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) (Swashbuckle)
- Auto Document Generator for Swagger
- Integrate Swagger and Versioning
- Integrate Swagger and JWT/OAuth Authentication
- Best Practices for Performance and Security

## Give a Star! ⭐️
If you like this project, learn something or you are using it in your applications, please give it a star. Thanks!

</div>

---

<div dir="rtl">

# 🥇پروژه API نویسی اصولی و حرفه ای در ASP.NET Core

## تکنولوژی، ابزار ها و قابلیت ها
  - **لایه بندی اصولی پروژه (Project Layering and Architecture)** : در این دوره لایه بندی اصولی یک پروژه را از ابتدا شروع و هر بخش را بررسی می کنیم. همچنین مباحث Repository و UOW رو هم بررسی می کنیم.
  - **احراز هویت (Authentication)**
    - **ASP.NET Core Identity** : احراز هویت توسط Identity + سفارشی سازی
    - **(Json Web Token) JWT** : احراز هویت توسط Jwt + یکپارچه سازی آن با Identity
    - **(Json Web Encryption) JWE** : ایمن سازی توکن ها بوسیله [رمزنگاری توکن (JWE)](https://www.dotnettips.info/post/2992) 
    - **Security Stamp** : جلوگیری از اعتبارسنجی توکن به هنگام تغییر دسترسی های کاربر جهت امنیت بیشتر
    - **Claims** : کار با Claim ها و تولید خودکار آنها توسط ClaimsFactory
  - **Logging (ثبت خطا ها)**
    - **Elmah** : استفاده از [Elmah](https://github.com/ElmahCore/ElmahCore) برای لاگ خطا ها در Memory, XML File و Database
    - **NLog** : استفاده از [NLog](https://github.com/NLog/NLog.Web) برای لاگ خطا ها در File و Console
    - **Custom Middleware** : نوشتن یک میدلویر سفارشی جهت لاگ تمامی خطا (Exception) ها
    - **Custom Exception** : نوشتن Exception برای مدیریت ساده تر خطا ها
    - **Sentry** : ثبت خطا ها در سیستم مدیریت لاگ [sentry.io](sentry.io) (مناسب برای پروژه های بزرگ)
  - **تزریق وابستگی (Dependency Injection**)
    - **ASP.NET Core IOC Container** : استفاده از Ioc container داخلی Asp Core
    - **Autofac** : استفاده از محبوب ترین کتابخانه [Autofac (Ioc Container)](https://github.com/autofac/Autofac)
    - **Auto Registeration** : ثبت خودکار سرویس ها توسط یک تکنیک خلاقانه با کمک Autofac
  - **ارتباط با دیتابیس (Data Access)**
    - **Entity Framework Core** : استفاده از EF Core
    - **Auto Entity Registration** : ثبت Entity های DbContext به صورت خودکار توسط Reflection
    - **Pluralizing Table Name** : جمع بندی نام جداول EF Core به صورت خودکار توسط کتابخانه [Pluralize.NET](https://github.com/sarathkcm/Pluralize.NET) و Reflection
    - **Automatic Configuration** : اعمال کانفیگ های EntityTypeConfiguration (FluentApi) به صورت خودکار توسط Reflection
    - **Sequential Guid** : بهینه سازی مقدار دهی identity برای Guid به صورت خودکار توسط Reflection
    - **Repository** : توضیحاتی در مورد معماری اصولی Repository در EF Core
    - **Data Intitializer** : یک معماری اصولی برای Seed کردن مقادیر اولیه به Database
    - **Auto Migrate** : آپدیت Database به آخرین Migration به صورت خودکار
    - **Clean String** : اصلاح و یک دست سازی حروف "ی" و "ک" عربی به فارسی و encoding اعداد فارسی در DbContext به صورت خودکار به هنگام SaveChanges
  - **Versioning** : نسخه بندی و مدیریت نسخه های پروژه + سفارشی سازی و ایجاد یک معماری حرفه ای
  - **ابزار (Swashbuckle) Swagger**
    - **Swagger UI** : ساخت یک ظاهر شکیل به همراه داکیومنت Aciton ها و Controller های پروژه و امکان تست API ها توسط [Swagger](https://github.com/domaindrivendev/Swashbuckle.AspNetCore) UI
    - **Versioning** : یکپارچه سازی اصولی Swagger با سیستم نسخه گذاری (Versioning)
    - **JWT Authentication** : یکپارچه سازی Swagger با سیستم احراز هویت بر اساس Jwt
    - **OAuth Authentication** : یکپارچه سازی Swagger با سیستم احراز هویت بر اساس OAuth
    - **Auto Summary Document Generation** : تولید خودکار داکیومنت (توضیحات) برای API های پروژه
    - **Advanced Customization** : سفارشی سازی های پیشرفته در Swagger
  - **دیگر قابلیت ها**
    - **API Standard Resulting** : استاندارد سازی و یک دست سازی خروجی API ها توسط ActionFilter
    - **Automatic Model Validation** : اعتبار سنجی خودکار
    - **AutoMapper** : جهت Mapping اشیاء توسط کتابخانه محبوب [AutoMapper](https://github.com/AutoMapper/AutoMapper) 
    - **Auto Mapping** :  سفارشی سازی وایجاد [یک معماری حرفه ای](https://github.com/mjebrahimi/auto-mapping) برای Mapping اشیا توسط Reflection 
    - **Generic Controller** : ساخت کنترلر برای عملیات CRUD بدون کد نویسی توسط ارث بری از CrudController
    - **Site Setting** : مدیریت تنظیمات پروژ توسط Configuration و ISnapshotOptions
    - **Postman** : آشنایی و کار با Postman جهت تست API ها
    - **Minimal Mvc** : حذف سرویس های اضافه MVC برای افزایش پرفرمنس در API نویسی
    - **Best Practices** : اعمال Best Practices ها جهت بهینه سازی، افزایش پرفرمنس و کدنویسی تمیز و اصولی
    - **و چندین نکته مفید دیگر ...**


 انتظار میره برای استفاده از این پروژه پیش نیاز های زیر رو داشته باشین :

1. تسلط نسبی بر روی زبان سی شارپ
2. آشنایی با پروتکل Http و REST
3. آشنایی با Entity Framework (ترجیحا EF Core)
4. آشنایی با ASP.NET MVC یا ASP.NET Core (و ترجیحا آشنایی با WebAPI)

</div>
