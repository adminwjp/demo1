using System;
using System.Collections.Generic;
using System.Text;
using Utility.Domain.Entities;

namespace Utility.Demo.Domain.Entities
{
    public class SmsEntity: SmsEntity<long>
    {

    }
    public class SmsEntity<Key> : Entity<Key>
    {
        private string appId;
        private string secrpt;

        public virtual string AppId { get => appId; set { Set(ref appId, value, "AppId"); } }
        public virtual string Secrpt { get => secrpt; set { Set(ref secrpt, value, "Secrpt"); } }
    }
}
