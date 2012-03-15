using System.Collections.Generic;
using UpdateControls.Collections;

namespace KnockoutCS
{
    public class ObservableList<T> : IndependentList<T>
    {
        public ObservableList()
        {
        }

        public ObservableList(IEnumerable<T> collection)
            : base(collection)
        {
        }
	}
}
