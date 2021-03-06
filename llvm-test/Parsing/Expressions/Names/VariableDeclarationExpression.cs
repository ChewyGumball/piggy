﻿using llvm_test.Parsing.Expressions.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Expressions.Names
{
    public class VariableDeclarationExpression : Expression, IVisibilityExpression
    {
        public String name { get; protected set; }
        public TypeName typeName { get; protected set; }
        public Visibility visibility { get; set; }

        public VariableDeclarationExpression(String name, TypeName type, Visibility visibility = Visibility.None)
        {
            this.name = name;
            this.typeName = type;
            this.visibility = visibility;
        }

        public override string print(int indentation = 0)
        {
            StringBuilder b = new StringBuilder();
            b.Append('\t', indentation);
            b.AppendFormat("(Variable Declaration [name = {0}, visibility = {1}, type = {2}])", name, visibility, typeName.print()).AppendLine();

            return b.ToString();
        }
    }
}
