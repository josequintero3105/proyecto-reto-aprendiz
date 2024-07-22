using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enumerations
{
    public enum TransactionActions
    {
        CreateTransaction,
        GetTransaction
    }

    public enum TransactionCoreStatus
    {
        [Description("Transaction is Rejected")]
        Rejected = 4,

        [Description("Transaction is Pending")]
        Pending = 5,

        [Description("Transaction is Approved")]
        Approved = 6,
    }
}
