using System;
using System.ComponentModel;

namespace TodoManager.Models.Enum
{
    public enum ResponseCodeEnum
    {
        #region infomation(0-99)

        [Description("OK")]
        ResponseCode_000 = 0,

        [Description("OK, But no data to return")]
        ResponseCode_001 = 1,

        #endregion

        #region failed information(100-199)

        [Description("Failed")]
        ResponseCode_100 = 100,

        [Description("Invalid parameters")]
        ResponseCode_101 = 101,

        [Description("Need to sign in")]
        ResponseCode_102 = 102,

        #endregion

        #region unhandled error(500-599)

        [Description("Server error")]
        ResponseCode_500 = 500,

        #endregion
    }
}
