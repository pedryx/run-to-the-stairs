namespace GameLib.Math
{
    /// <summary>
    /// Represent an interface for provider of basic math operations.
    /// </summary>
    public interface IMathProvider
    {
        /// <summary>
        /// Concat (multiply) two matricies.
        /// </summary>
        IMatrix Concat(IMatrix matrix1, IMatrix matrix2);

        /// <summary>
        /// Create matrix from transform.
        /// </summary>
        IMatrix MatrixFromTransform(Transform transform);
    }
}
