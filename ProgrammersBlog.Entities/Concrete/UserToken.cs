﻿using Microsoft.AspNetCore.Identity;
using ProgrammersBlog.Shared.Entities.Abstract;

namespace ProgrammersBlog.Entities.Concrete
{
    public class UserToken: IdentityUserToken<int>, IEntity
    {
        
    }
}