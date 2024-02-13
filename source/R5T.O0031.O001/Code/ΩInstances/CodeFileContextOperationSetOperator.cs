using System;


namespace R5T.O0031.O001
{
    public class CodeFileContextOperationSetOperator : ICodeFileContextOperationSetOperator
    {
        #region Infrastructure

        public static ICodeFileContextOperationSetOperator Instance { get; } = new CodeFileContextOperationSetOperator();


        private CodeFileContextOperationSetOperator()
        {
        }

        #endregion
    }
}
