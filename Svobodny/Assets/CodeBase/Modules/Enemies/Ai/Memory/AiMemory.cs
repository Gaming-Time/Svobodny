using System.Collections.Generic;
using Apex.Utilities;
using CodeBase.Modules.Enemies.Ai.Entity;

namespace CodeBase.Modules.Enemies.Ai.Memory
{
    public class AiMemory
    {
        private List<Observation> _observations;

        /// <summary>
        /// Initializes a new instance of the <see cref="AIMemory"/> class.
        /// </summary>
        public AiMemory()
        {
            _observations = new List<Observation>(10);
        }

        /// <summary>
        /// Gets all current observations.
        /// </summary>
        /// <value>
        /// All current observations.
        /// </value>
        public List<Observation> allObservations
        {
            get { return _observations; }
        }

        /// <summary>
        /// Adds or updates an observation.
        /// </summary>
        /// <param name="observation">An observation.</param>
        public void AddOrUpdateObservation(Observation observation)
        {
            AddOrUpdateObservation(observation, false);
        }

        /// <summary>
        /// Adds or updates an observation.
        /// </summary>
        /// <param name="observation">An observation.</param>
        /// <param name="checkTimestamp">if set to <c>true</c>, checks timestamps on the passed observation towards existing observations in memory of the same entity. Only updates if the passed observation is newer..</param>
        public void AddOrUpdateObservation(Observation observation, bool checkTimestamp)
        {
            Ensure.ArgumentNotNull(observation, "observation");

            var count = _observations.Count;
            for (int i = 0; i < count; i++)
            {
                var obs = _observations[i];
                if (!object.ReferenceEquals(obs.entity, observation.entity))
                {
                    continue;
                }

                if (checkTimestamp && obs.timestamp >= observation.timestamp)
                {
                    // existing observation timestamp is newer, and we are checking timestamps
                    return;
                }

                _observations[i] = observation;
                return;
            }

            _observations.Add(observation);
        }

        /// <summary>
        /// Gets an observation for a given entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>An observation for the supplied entity, or null.</returns>
        public Observation GetObservation(IEntity entity)
        {
            Ensure.ArgumentNotNull(entity, "entity");

            var count = _observations.Count;
            for (int i = 0; i < count; i++)
            {
                var obs = _observations[i];
                if (object.ReferenceEquals(obs.entity, entity))
                {
                    return obs;
                }
            }

            return null;
        }

        /// <summary>
        /// Removes the observation at the supplied index.
        /// </summary>
        /// <param name="index">The index to remove the observation at.</param>
        public void RemoveObservationAt(int index)
        {
            Ensure.ArgumentInRange(() => index >= 0 && index < _observations.Count, "index", index);

            _observations.RemoveAt(index);
        }
    }
}