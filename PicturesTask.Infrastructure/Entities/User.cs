﻿using Microsoft.AspNetCore.Identity;

namespace PicturesTask.Infrastructure.Entities
{
    public class User : IdentityUser
    {
        public List<Friend> Friends { get; set; } = new();

        public List<Image> Images { get; set; }

        public List<Invation> Invations { get; set; }
    }
}
