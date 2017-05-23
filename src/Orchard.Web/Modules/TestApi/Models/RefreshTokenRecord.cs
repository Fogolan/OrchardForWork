using System;

namespace TestApi.Models
{
    public class RefreshTokenRecord
    {
        public virtual int Id { get; set; }
        public virtual string Token { get; set; }
        public virtual string UserName { get; set; }
        public virtual DateTime IssuedUtc { get; set; }
        public virtual DateTime ExpiresUtc { get; set; }
        public virtual string ProtectedTicket { get; set; }
    }
}