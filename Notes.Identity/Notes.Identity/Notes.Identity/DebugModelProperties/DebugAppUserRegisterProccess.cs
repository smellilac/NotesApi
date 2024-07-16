using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Notes.Identity.Models;
using Serilog;
using System.Reflection;

namespace Notes.Identity.DebugModelProperties;

public class DebugAppUserRegisterProccess
{
    public static async Task CheckModelProperties(AppUser model)
    {
        Type type = model.GetType();
        PropertyInfo[] properties = type.GetProperties();

        foreach (PropertyInfo property in properties)
        {
            object value = property.GetValue(model);
            if (value == null)
            {
                Log.Fatal($"{property.Name} is not set.");
            }
            else
            {
                Log.Fatal($"{property.Name} is set to {value}");
            }
        }
    }
}
