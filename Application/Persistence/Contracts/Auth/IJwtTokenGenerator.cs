using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Persistence.Contracts.Auth;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
