using System.Collections.Generic;

namespace KnockoutCS.Example.Collection
{
    public class Parent
    {
        public IList<Child> Children { get; protected set; }
    }
}
