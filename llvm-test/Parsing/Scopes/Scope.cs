using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace llvm_test.Parsing.Scopes
{
    class Scope
    {
        private Scope parent;
        private Dictionary<String, NamedVariable> identifiers = new Dictionary<String, NamedVariable>();
        public Scope(Scope parent)
        {
            this.parent = parent;
        }

        public bool introduce(NamedVariable v)
        {            
            bool contains = identifiers.ContainsKey(v.name);
            if(!contains)
            {
                identifiers[v.name] = v;
            }

            return !contains;
        }

        public NamedVariable this[string name]
        {
            get {
                if (identifiers.ContainsKey(name))
                {
                    return identifiers[name];
                }
                else if(parent != null)
                {
                    return parent[name];
                }
                else
                {
                    throw new NamedVariableNotFoundException(name);
                }
            }
        }
    }
}
