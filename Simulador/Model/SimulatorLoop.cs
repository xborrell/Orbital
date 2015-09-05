using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CalculadorOrbital;
using UnityEngine;

namespace Xb.Simulador.Model
{
    public class SimulatorLoop
    {
        public TimeSpan EllapsedTime { get; protected set; }
        public double AcceleratedTime { get; set; }
        public Satelite Satelite { get; protected set; }

        Task _task;
        CancellationTokenSource _cancelSource;
        const long intervaloFisicoEnTicks = 1000000;
        const long ticksPorSegundo = 10000000;
        Vector3 posicionInicial = new Vector3(-822.79774F, -4438.63582F, 5049.31502F);
        Vector3 velocidadInicial = new Vector3(7.418175658F, .709253354F, 1.828703177F);

        public SimulatorLoop()
        {
            EllapsedTime = new TimeSpan();
            AcceleratedTime = 1;
            Satelite = new Satelite(posicionInicial, velocidadInicial);
        }

        public void Start()
        {
            _cancelSource = new CancellationTokenSource();

            Task<Task> task = Task.Factory.StartNew(
                function: ExecuteAsync,
                cancellationToken: _cancelSource.Token,
                creationOptions: TaskCreationOptions.LongRunning,
                scheduler: TaskScheduler.Default);

            _task = task.Unwrap();
        }

        public void Stop()
        {
            _cancelSource.Cancel(); // request the cancellation

            _task.Wait(); // wait for the task to complete
        }

        async Task ExecuteAsync()
        {
            EllapsedTime = new TimeSpan();
            long tiempoAcumuladoEnTicks = DateTime.Now.Ticks;

            while (!_cancelSource.IsCancellationRequested)
            {
                long tiempoActualEnTicks = DateTime.Now.Ticks;

                if (tiempoActualEnTicks - tiempoAcumuladoEnTicks > intervaloFisicoEnTicks)
                {
                    PhysicsUpdate((long)(intervaloFisicoEnTicks * AcceleratedTime));
                    tiempoAcumuladoEnTicks += intervaloFisicoEnTicks;
                }
            }
        }

        private void PhysicsUpdate(long deltaEnTicks)
        {
            float deltaEnSegundos = deltaEnTicks / (float)ticksPorSegundo;
            Satelite.Update(deltaEnSegundos);
            EllapsedTime += TimeSpan.FromTicks(deltaEnTicks);
        }
    }
}
