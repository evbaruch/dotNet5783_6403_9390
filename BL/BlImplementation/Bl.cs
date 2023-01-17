﻿using BlApi;
using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation;

sealed internal class Bl : IBl
{
    public ICart Cart => new Cart();

    public IOrder Order => new Order();

    public IProduct Product => new Product();

    public IUser User => new User();
}
