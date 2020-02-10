using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class TodoPaginationDTO
    {
        public int page { get; set; }
        public long count { get; set; }
        public TodoDTO filters { get; set; }
        public TodoDTO[] data { get; set; }
    }
}
