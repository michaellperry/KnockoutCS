using KnockoutCS.Impl;
using UpdateControls.Collections;

namespace KnockoutCS.Example.Collection
{
    public class Parent__Observable : Parent
    {
        public Parent__Observable()
        {
            Children = new IndependentList<Child>();
        }
    }
}
