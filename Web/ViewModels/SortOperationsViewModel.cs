using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class SortOperationsViewModel
    {
        public SortState SumSort { get; private set; }
        public SortState DateTimeSort { get; private set; }
        public SortState CategorySort { get; private set; }
        public SortState Current { get; private set; }

        public SortOperationsViewModel(SortState sortOrder)
        {
            SumSort = sortOrder == SortState.SumAsc ? SortState.SumDesc : SortState.SumAsc;
            DateTimeSort = sortOrder == SortState.DateTimeAsc ? SortState.DateTimeDesc : SortState.DateTimeAsc;
            CategorySort = sortOrder == SortState.CategoryAsc ? SortState.CategoryDesc : SortState.CategoryAsc;
            Current = sortOrder;
        }
    }

    public enum SortState
    {
        DateTimeAsc,
        DateTimeDesc,
        SumAsc,
        SumDesc,
        CategoryAsc,
        CategoryDesc
    }
}
