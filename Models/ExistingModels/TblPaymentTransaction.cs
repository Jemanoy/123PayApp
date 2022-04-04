using System;
using System.Collections.Generic;

#nullable disable

namespace _123PayApp.Models.ExistingModels
{
    public partial class TblPaymentTransaction
    {
        public string ReferenceNo { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public decimal Amount { get; set; }
        public string MobileNumber { get; set; }
        public string Remarks { get; set; }
        public int MerchantId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int StatusId { get; set; }
        public byte[] Attachments { get; set; }

        public virtual TblMerchant Merchant { get; set; }
        public virtual TblStatus Status { get; set; }
    }
}
