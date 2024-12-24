namespace ShopManager.Controller.ResultHandler
{
    /// <summary>
    /// The base for any kind of application-defined error.
    /// </summary>
    public class Error
    {
        public uint ErrorCode { get; protected set; } = 0x01_00_00_00;
        public string Description { get; protected set; } = "";

        public Error() { }

        public Error(uint errorCode, string description)
        {
            this.ErrorCode = errorCode;
            this.Description = description;
        }

        public Error(ErrorType errorType, string description)
        {
            this.ErrorCode = (uint)errorType;
            this.Description = description;
        }

        /// <summary>
        /// The base for creating more meaningful error codes. These serve as the base error codes.
        /// </summary>
        public enum ErrorType : uint
        {
            /// <summary>
            /// Indicates no error.
            /// </summary>
            None = 0,
            /// <summary>
            /// Indicates a generic or unknown error.
            /// </summary>
            Unknown = 0x01_00_00_00,
            /// <summary>
            /// Indicates a generic validation error.
            /// </summary>
            Validation = 0x02_00_00_00,
            /// <summary>
            /// Indicates a generic database error.
            /// </summary>
            Database = 0x03_00_00_00
        }
    }
}
