using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autoshop.Models.Autoshop
{
    public class AutoshopManager
    {
        private static int _lastCarId = 0;
        private static Dictionary<int, Car> _carsStore = new Dictionary<int, Car>();

        public IEnumerable<Car> GetCars()
        {
            return _carsStore.Select(a => a.Value).ToArray();
        }

        public Car GetCarById(int id)
        {
            return _carsStore.ContainsKey(id) ? _carsStore[id] : null;
        }

        public void AddCar(Car car)
        {
            while (car.Id == null || _carsStore.ContainsKey((int)car.Id))
            {
                car.Id = ++_lastCarId;
            }
            _carsStore[(int)car.Id] = car;
        }
    }
}