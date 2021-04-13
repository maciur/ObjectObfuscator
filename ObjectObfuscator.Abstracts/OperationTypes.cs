namespace ObjectObfuscator.Abstracts
{
    public enum OperationTypes
    {
        /// <summary>
        /// String values will be obfuscate, for others types, default value will be set for field or property
        /// </summary>
        Obfuscate,
        /// <summary>
        /// Default value of type will be set for field or property
        /// </summary>
        Remove,
    }
}
