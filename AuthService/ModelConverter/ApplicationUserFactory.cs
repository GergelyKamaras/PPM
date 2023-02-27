﻿using AuthService.Authentication;
using AuthServiceModelLibrary.ApplicationUser;
using AuthServiceModelLibrary.DTOs;
using AuthServiceModelLibrary.Enums;

namespace AuthService.ModelConverter
{
    public class ApplicationUserFactory : IApplicationUserFactory
    {
        private readonly ISecurityUtil _secUtil;
        public ApplicationUserFactory(ISecurityUtil secUtil)
        {
            _secUtil = secUtil;
        }
        public ApplicationUser Converter(UserRegistrationDTO userDTO)
        {
            string salt = _secUtil.CreateSalt();
            UserRole role;
            switch (userDTO.Role)
            {
                case "Administrator":
                    role = UserRole.Administrator;
                    break;
                case "Owner":
                    role = UserRole.Owner;
                    break;
                case "Tenant":
                    role = UserRole.Tenant;
                    break;
                default:
                    throw new ArgumentException("Invalid user role!");
            }

            Guid id = Guid.NewGuid();

            return new ApplicationUser()
            {
                Id = id.ToString(),
                Email = userDTO.Email,
                UserName = userDTO.Username,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Salt = salt,
                PasswordHash = _secUtil.HashPassword(userDTO.Password, salt),
                Role = role
            };
        }
    }
}
