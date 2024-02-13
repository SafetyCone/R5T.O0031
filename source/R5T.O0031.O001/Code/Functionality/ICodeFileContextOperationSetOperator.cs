using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0073.F001;
using R5T.L0073.T001;
using R5T.L0093.T000;
using R5T.T0132;
using R5T.T0221;


namespace R5T.O0031.O001
{
    [FunctionalityMarker]
    public partial interface ICodeFileContextOperationSetOperator : IFunctionalityMarker
    {
        public IEnumerable<Func<TCodeFileContext, Task>> Generate_ProgramFile<
            TCodeFileContext,
            TNamespaceDeclarationContext,
            TClassDeclarationContext,
            TMethodDeclarationContext,
            TStatementContext>(
            string namespaceName)
            where TCodeFileContext : IHasFilePath, IWithCompilationUnit
            where TNamespaceDeclarationContext : IWithCompilationUnit, IWithNamespaceDeclaration, new()
            where TClassDeclarationContext : IWithClassDeclaration, IWithMemberDeclarationType<ClassDeclarationSyntax>, IWithCompilationUnit, IWithNamespaceDeclaration, new()
            where TMethodDeclarationContext : IWithMethodDeclaration, IWithMemberDeclarationType<MethodDeclarationSyntax>, IWithCompilationUnit, IWithNamespaceDeclaration, IWithClassDeclaration, new()
            where TStatementContext : IWithStatement, IWithMethodDeclaration, IWithCompilationUnit, IWithNamespaceDeclaration, new()
        {
            var className = Instances.ClassNames._Strings.Program;

            return Instances.EnumerableOperator.From(
                //Instances.ContextOperations.DisplayContext_AtDesignTime_ForAsynchronous<CodeFileContext>(),
                Instances.CompilationUnitContextOperations.Set_CompilationUnit_ToNewEmpty,
                Instances.CompilationUnitContextOperations.Add_UsingNamespace<TCodeFileContext>(
                    Instances.NamespaceNames.System,
                    out var usingSystemNamespace
                ),
                Instances.CompilationUnitContextOperations.In_NamespaceDeclarationContext<TCodeFileContext, TNamespaceDeclarationContext>(
                    out _,
                    //Instances.ContextOperations.DisplayContext_AtDesignTime_ForAsynchronous<NamespaceDeclarationContext>(),
                    Instances.NamespaceDeclarationContextOperations.Set_NamespaceDeclaration_ToNewEmpty<TNamespaceDeclarationContext>(
                        namespaceName,
                        out _
                    ),
                    Instances.NamespaceDeclarationContextOperations.In_ClassDeclarationContext<TNamespaceDeclarationContext, TClassDeclarationContext>(
                        out _,
                        Instances.ClassDeclarationContextOperations.Set_ClassDeclaration_New<TClassDeclarationContext>(
                            className,
                            out var classDeclarationSet
                        ),
                        //Instances.ClassDeclarationContextOperations.Modify_Modifiers<TClassDeclarationContext>(
                        //    classDeclarationSet,
                        //    _ =>
                        //    {
                        //        var modifiersDescriptor = new ModifiersDescriptor
                        //        {
                        //            Accessibility = MemberAccessibilityLevel.Public,
                        //        };

                        //        var output = Instances.ModifiersOperator.Get_ModifiersTokenList(modifiersDescriptor);
                        //        return output;
                        //    }
                        //),
                        // Add a method.
                        Instances.ClassDeclarationContextOperations.In_MethodDeclarationContext<TClassDeclarationContext, TMethodDeclarationContext>(
                            out _,
                            //out var propertiesSet, // Results in CS8196, "Reference to an implicitly-typed out variable 'propertiesSet' is not permitted in the same argument list." error below.
                            //Instances.EnumerableOperator.From( // Does not resolve the CS8196 'propertiesSet' error, since still in the same arguments list.
                            Instances.MethodDeclarationContextOperations.Set_MethodDeclaration_New<TMethodDeclarationContext>(
                                Instances.MethodNames.Main,
                                Instances.Types.Void,
                                out IsSet<IHasMethodDeclaration> methodDeclarationSet),
                            Instances.MethodDeclarationContextOperations.Modify_Modifiers<TMethodDeclarationContext>(
                                methodDeclarationSet,
                                _ =>
                                {
                                    var modifiersDescriptor = new ModifiersDescriptor
                                    {
                                        //Accessibility = MemberAccessibilityLevel.Public,
                                        Is_Static = true,
                                    };

                                    var output = Instances.ModifiersOperator.Get_ModifiersTokenList(modifiersDescriptor);
                                    return output;
                                }
                            ),
                            Instances.MethodDeclarationContextOperations.In_StatementDeclarationContext<TMethodDeclarationContext, TStatementContext>(
                                out _,
                                Instances.StatementContextOperations.Set_Statement<TStatementContext>(
                                    Instances.Statements.Console_WriteLine_HelloWorld,
                                    out _),
                                Instances.StatementContextOperations.Add_Statement_ToMethodDeclaration
                            ),
                            Instances.MethodDeclarationContextOperations.Add_MethodDeclaration_ToClassDeclaration
                        ),
                        // Do this last so the class declaraion syntax object is finished.
                        Instances.ClassDeclarationContextOperations.Add_ClassDeclaration_ToNamespaceDeclaration
                    ),
                    // Do this last so the namespace declaration syntax object is finished.
                    Instances.NamespaceDeclarationContextOperations.Add_NamespaceDeclaration_ToCompilationUnit
                ),
                // Need to write out the compilation unit to the code file path.
                Instances.CodeFileContextOperations.Write_CompilationUnit_ToFilePath<TCodeFileContext>(
                    out _
                )
            );
        }
    }
}
