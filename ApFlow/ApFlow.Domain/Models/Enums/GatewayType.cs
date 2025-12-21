namespace ApFlow.Domain.Models.Enums
{
    public enum GatewayType
    {
        /// <summary>
        ///  排他网关 (XOR)
        /// </summary>
        Exclusive,
        /// <summary>
        /// 并行网关 (AND)
        /// </summary>
        Parallel,
        /// <summary>
        /// 包含网关 (OR)
        /// </summary>
        Inclusive
    }
}



