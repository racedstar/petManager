//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PetGroomingManager.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Vw_Log
    {
        public int logSN { get; set; }
        public int petSN { get; set; }
        public int cosmetologySN { get; set; }
        public int BillNumber { get; set; }
        public int logPrice { get; set; }
        public System.DateTime CreateTime { get; set; }
        public string Remark { get; set; }
        public string petName { get; set; }
        public string customerName { get; set; }
        public string phone { get; set; }
        public string kindName { get; set; }
        public string varietyName { get; set; }
        public string cosmetologyName { get; set; }
        public Nullable<int> price { get; set; }
        public Nullable<int> customerSN { get; set; }
    }
}
