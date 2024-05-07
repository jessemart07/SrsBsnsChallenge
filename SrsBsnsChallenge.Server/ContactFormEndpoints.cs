using Microsoft.EntityFrameworkCore;
using SrsBsnsChallenge.Server.Data;
using SrsBsnsChallenge.Server.Data.Models;
using System;
using System.Xml.Linq;

namespace SrsBsnsChallenge.Server;

public static class ContactFormEndpoints
{
    // Extends WebApplication.
    public static void MapContactFormsEndpoints(this WebApplication app)
    {
        app.MapGet("/contact-forms", async (SrsBsnsChallengeDbContext dbContext) =>
        {
            List<ContactForm> allContactForms = await dbContext.ContactForms.ToListAsync();

            return Results.Ok(allContactForms);
        });

        app.MapGet("/contact-forms/{Id}", async (int Id, SrsBsnsChallengeDbContext dbContext) =>
        {
            ContactForm? post = await dbContext.ContactForms.FindAsync(Id);

            if (post != null)
            {
                return Results.Ok(post);
            }
            else
            {
                return Results.NotFound();
            }
        });

        app.MapPost("/contact-forms", async (ContactFormCreateUpdateDTO postToCreateDTO, SrsBsnsChallengeDbContext dbContext) =>
        {
            // Let EF Core auto-increment the ID.
            ContactForm ContactFormToCreate = new()
            {
                Id = 0,
                Name = postToCreateDTO.Name,
                Subject = postToCreateDTO.Subject,
                Email = postToCreateDTO.Email,
                Message = postToCreateDTO.Message,
            };

            dbContext.ContactForms.Add(ContactFormToCreate);

            bool success = await dbContext.SaveChangesAsync() > 0;

            if (success)
            {
                return Results.Created($"/contact-forms/{ContactFormToCreate.Id}", ContactFormToCreate);
            }
            else
            {
                // 500 = internal server error.
                return Results.StatusCode(500);
            }
        });

        app.MapPut("/contact-forms/{Id}", async (int Id, ContactFormCreateUpdateDTO updatedContactFormDTO, SrsBsnsChallengeDbContext dbContext) =>
        {
            var ContactFormToUpdate = await dbContext.ContactForms.FindAsync(Id);

            if (ContactFormToUpdate != null)
            {
                ContactFormToUpdate.Name = updatedContactFormDTO.Name;
                ContactFormToUpdate.Subject = updatedContactFormDTO.Subject;
                ContactFormToUpdate.Email = updatedContactFormDTO.Email;
                ContactFormToUpdate.Message = updatedContactFormDTO.Message;

                bool success = await dbContext.SaveChangesAsync() > 0;

                if (success)
                {
                    return Results.Ok(ContactFormToUpdate);
                }
                else
                {
                    // 500 = internal server error.
                    return Results.StatusCode(500);
                }
            }
            else
            {
                return Results.NotFound();
            }
        });

        app.MapDelete("/contact-forms/{Id}", async (int Id, SrsBsnsChallengeDbContext dbContext) =>
        {
            ContactForm? ContactFormToDelete = await dbContext.ContactForms.FindAsync(Id);

            if (ContactFormToDelete != null)
            {
                dbContext.ContactForms.Remove(ContactFormToDelete);

                bool success = await dbContext.SaveChangesAsync() > 0;

                if (success)
                {
                    return Results.NoContent();
                }
                else
                {
                    // 500 = internal server error.
                    return Results.StatusCode(500);
                }
            }
            else
            {
                return Results.NotFound();
            }
        });
    }
}