using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Interfaces;

public interface ICurrentUserService
{
    public Guid UserId { get; }
}
