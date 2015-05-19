using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace GameHelpers.Classes
{
    public class GameServices
    {

        private static GameServiceContainer container;
        private static GameServiceContainer Instance
        {
            get
            {
                return container ?? (container = new GameServiceContainer());
            }
        }

        public static T GetService<T>()
        {
            var service = (T)Instance.GetService(typeof(T));

            if (service == null) throw new GameServiceException("Null services are not allowed.");

            return service;
        }

        public static void AddService<T>(T service)
        {
            if (service == null) throw new GameServiceException("Service can't be null");
            Instance.AddService(typeof(T), service);
        }

        public static void RemoveService<T>()
        {
            Instance.RemoveService(typeof(T));
        }

    }

    public class GameServiceException : Exception
    {
        public GameServiceException(string message)
            : base(message)
        {
        }

        public GameServiceException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
