﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _123PayApp.Models
{
    public class UploadFileModel
    {
        IFormFile fileToUpload { get; set; }
    }
}
