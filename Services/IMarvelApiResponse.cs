using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarvelGallery.Services
{
    public interface IMarvelApiResponse<T>
    {
        int Code { get; set; }
        string Status { get; set; }
        T Data { get; set; }
    }
}
