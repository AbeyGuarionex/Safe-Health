using System;
using System.Collections.Generic;

namespace SafeHealth.Domain
{
    public partial class Document
    {
        public string UserCodeFk1 { get; set; } = null!;
        public string UserEmailFk2 { get; set; } = null!;
        public string DocumentTitle { get; set; } = null!;
        public DateTime UploadedDocDate { get; set; }
        public byte[] Document1 { get; set; } = null!;
        public string DocumentType { get; set; } = null!;
        public string AuthorizedStatus { get; set; } = null!;

        public virtual User User { get; set; } = null!;
    }
}
