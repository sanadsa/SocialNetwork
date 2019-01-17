﻿namespace Social.Common.Models
{
    /// <summary>
    /// class for user in the social network app
    /// </summary>
    public class User
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
