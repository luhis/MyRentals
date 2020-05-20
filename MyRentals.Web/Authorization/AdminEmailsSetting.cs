namespace MyRentals.Web.Authorization
{
    using System;

    public class AdminEmailsSetting
    {
        public string[] AdminEmails { get; private set; } = Array.Empty<string>();
    }
}