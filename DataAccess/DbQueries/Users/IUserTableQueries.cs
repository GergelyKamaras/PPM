﻿using DataAccess.Models.Users;

namespace DataAccess.DbQueries.Users;

public interface IUserTableQueries
{
    public void AddUser(ApplicationUser user);
    public void UpdateUser(ApplicationUser user);
    public void DeleteUser(string id);
    public ApplicationUser? GetUserById(string id);
    public ApplicationUser? GetUserByEmail(string email);
    public ApplicationUser? GetUserByUsername(string username);
}