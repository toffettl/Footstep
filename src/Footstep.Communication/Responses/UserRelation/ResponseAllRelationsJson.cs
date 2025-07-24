using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Footstep.Communication.Responses.UserRelation
{
    public class ResponseAllRelationsJson
    {
        public Guid UserId { get; set; }
        public List<ResponseFollowersJson> Followers  { get; set; }
        public List<ResponseFollowingJson> Following { get; set; }
    }
}
