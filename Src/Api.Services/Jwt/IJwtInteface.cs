namespace Api.Services.Jwt
{
    using Api.Entities;
    using Api.Entities.Users;

    using Ccms.Common;
    using System.Collections.Generic;

    public interface IJwtInteface
    {

        /// <summary>
        /// این متود برای ساخت توکن و بازگرداندن توکن به کاربر میباشد
        /// </summary>
        /// <param name="user">اطلاعات کاربر را گرفته و برای ساخت توکن استفاده میکند</param>
        /// <returns> باز میگرداند AccessToken توکن در قالب </returns>
        AccessToken Generate(User user, Role role);
        AccessToken Generate(Dictionary<string, string> claims);
        /// <summary>
        /// ثبت دسترسی های کاربر در توکن
        /// </summary>
        /// <param name="user">دریافت اطلاعات کاربر</param>
        /// <param name="role">دریافت اطلاعات دسترسی کاربر</param>
        /// <returns>Task<IEnumerable<Claim>></returns>
        Dictionary<string, string> Read(string token);
    }
}
