﻿using Aplication.DTOs;
using Aplication.Interfaces.Querys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication.Interfaces.Services
{
    public interface IDeliveryTypeService
    {
        Task<List<GenericResponse>> GetAll();
    }
}
