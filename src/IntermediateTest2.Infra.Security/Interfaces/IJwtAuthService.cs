using System;

namespace IntermediateTest2.Infra.Security.Interfaces
{
    public interface IJwtAuthService
    {
        string GenerateToken(string uniqueName, DateTime dateTimeCreated, DateTime dateTimeExpires);
    }
}