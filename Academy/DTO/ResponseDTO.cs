using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Academy.DTO;

internal class ResponseDTO
{
    public string Message { get; set; } = string.Empty;
    public bool IsSuccess { get; set; } = true;
}
