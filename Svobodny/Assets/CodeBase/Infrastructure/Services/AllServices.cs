﻿using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.Services
{
    public class AllServices
    {
        private static AllServices _instance;
        private readonly Dictionary<Type, IService> _serviceInstances;

        public static AllServices Container => _instance ??= new AllServices();

        public AllServices()
        {
            _serviceInstances = new Dictionary<Type, IService>();
        }

        public void RegisterSingle<TService>(TService implementation) where TService : IService =>
            _serviceInstances.Add(typeof(TService), implementation);

        public TService GetSingle<TService>() where TService : class, IService =>
            _serviceInstances[typeof(TService)] as TService;
    }
}