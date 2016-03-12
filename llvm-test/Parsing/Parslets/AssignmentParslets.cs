using llvm_test.Parsing.Expressions;
using llvm_test.Parsing.Expressions.Assignment;
using llvm_test.Parsing.Expressions.Names;
using llvm_test.Parsing.Expressions.Tuples;
using llvm_test.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Parslets
{
    class AssignmentParslets
    {
        public static Expression assignment(Parser p, Expression left, Token t)
        {   
            if(left is TupleDeclarationExpression)
            {
                return new TupleDeclarationAssignmentExpression(left as TupleDeclarationExpression, p.parseExpression(0));
            }
            else if(left is VariableReferenceExpression)
            {
                return new VariableAssignmentExpression((left as VariableReferenceExpression).name, p.parseExpression(0));
            }
            else if(left is VariableDeclarationExpression)
            {
                return new VariableDeclarationAssignmentExpression(left as VariableDeclarationExpression, p.parseExpression(0));
            }
            else
            {
                throw new Exception("The left side of an assignment must be a tuple or variable.");
            }
        }
    }
}
