using BusBoiBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusBoiBackend.GraphQL.Payloads
{
    public record AddUserPayload(User user);
}
