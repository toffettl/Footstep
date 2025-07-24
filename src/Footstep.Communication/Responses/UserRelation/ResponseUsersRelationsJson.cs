using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Footstep.Communication.Responses.Traces;

namespace Footstep.Communication.Responses.UserRelation
{
    public class ResponseUsersRelationsJson
    {
        public List<ResponseUserRelationJson> Relations { get; set; } = [];
    }
}
