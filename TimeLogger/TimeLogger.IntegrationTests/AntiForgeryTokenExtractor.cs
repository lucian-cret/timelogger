using System;
using System.Collections.Generic;
using System.Text;

namespace TimeLogger.IntegrationTests
{
    public static class AntiForgeryTokenExtractor
    {
        public static string AntiForgeryFieldName { get; } = "AntiForgeryTokenField";
        public static string AntiForgeryCookieName { get; } = "AntiForgeryTokenCookie";
    }
}
