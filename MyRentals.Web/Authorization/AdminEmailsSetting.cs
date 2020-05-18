namespace MyRentals.Web.Authorization
{
    using System;
    using System.Collections.Generic;

    public class AdminEmailsSetting
    {
        public IEnumerable<string> AdminEmails { get; } = Array.Empty<string>();
    }
}