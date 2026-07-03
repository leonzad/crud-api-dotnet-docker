using CRUDDOTNET.Controllers;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Xunit;

namespace CRUDDOTNET.Tests;

public class UsersControllerTests
{
    private static UsersController NewController(out UserContext context)
    {
        var options = new DbContextOptionsBuilder<UserContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        context = new UserContext(options);
        return new UsersController(context);
    }

    private static User NewUser(int id = 0) => new()
    {
        Id = id,
        Nome = "Fulano",
        Cpf = "00000000000",
        Email = "fulano@example.com",
        DataNasc = new DateTime(1990, 1, 1, 0, 0, 0, DateTimeKind.Utc),
    };

    [Fact]
    public async Task PostUser_CreatesAndReturnsCreated()
    {
        var controller = NewController(out var context);

        var result = await controller.PostUser(NewUser());

        var created = Assert.IsType<CreatedAtActionResult>(result.Result);
        var user = Assert.IsType<User>(created.Value);
        Assert.Equal(1, await context.Users.CountAsync());
        Assert.Equal("Fulano", user.Nome);
    }

    [Fact]
    public async Task GetUser_ReturnsUser_WhenExists()
    {
        var controller = NewController(out var context);
        context.Users.Add(NewUser());
        await context.SaveChangesAsync();

        var result = await controller.GetUser(1);

        Assert.NotNull(result.Value);
        Assert.Equal("fulano@example.com", result.Value.Email);
    }

    [Fact]
    public async Task GetUser_ReturnsNotFound_WhenMissing()
    {
        var controller = NewController(out _);

        var result = await controller.GetUser(999);

        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task PutUser_ReturnsBadRequest_WhenIdMismatch()
    {
        var controller = NewController(out _);

        var result = await controller.PutUser(2, NewUser(id: 1));

        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task PutUser_ReturnsNotFound_WhenMissing()
    {
        var controller = NewController(out _);

        var result = await controller.PutUser(999, NewUser(id: 999));

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task PutUser_UpdatesFields()
    {
        var controller = NewController(out var context);
        context.Users.Add(NewUser());
        await context.SaveChangesAsync();

        var updated = NewUser(id: 1);
        updated.Nome = "Atualizado";
        var result = await controller.PutUser(1, updated);

        Assert.IsType<NoContentResult>(result);
        var stored = await context.Users.FindAsync(1);
        Assert.Equal("Atualizado", stored!.Nome);
    }

    [Fact]
    public async Task DeleteUser_RemovesUser()
    {
        var controller = NewController(out var context);
        context.Users.Add(NewUser());
        await context.SaveChangesAsync();

        var result = await controller.DeleteUser(1);

        Assert.IsType<NoContentResult>(result);
        Assert.Equal(0, await context.Users.CountAsync());
    }

    [Fact]
    public async Task DeleteUser_ReturnsNotFound_WhenMissing()
    {
        var controller = NewController(out _);

        var result = await controller.DeleteUser(999);

        Assert.IsType<NotFoundResult>(result);
    }
}
