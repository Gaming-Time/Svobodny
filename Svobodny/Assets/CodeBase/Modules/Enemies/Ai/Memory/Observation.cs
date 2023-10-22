using CodeBase.Modules.Enemies.Ai.Entity;
using UnityEngine;

namespace CodeBase.Modules.Enemies.Ai.Memory
{
    public class Observation
    {
         /// <summary>
        /// Initializes a new instance of the <see cref="Observation"/> class by copying/cloning the supplied observation.
        /// </summary>
        /// <param name="obs">The observation to copy from.</param>
        /// <param name="isVisible">if set to <c>true</c> the observation is visible, otherwise false.</param>
        public Observation(Observation obs, bool isVisible)
            : this(obs.entity, obs.position, isVisible, obs.timestamp)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Observation"/>.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="isVisible">if set to <c>true</c> the observation is visible, otherwise false.</param>
        public Observation(IEntity entity, bool isVisible)
            : this(entity, entity.Position, isVisible, Time.time)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Observation"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="position">The position.</param>
        /// <param name="isVisible">if set to <c>true</c> [is visible].</param>
        /// <param name="timestamp">The timestamp.</param>
        public Observation(IEntity entity, Vector3 position, bool isVisible, float timestamp)
        {
            this.entity = entity;
            this.position = position;
            this.isVisible = isVisible;
            this.timestamp = timestamp;
        }

        /// <summary>
        /// Gets the entity.
        /// </summary>
        /// <value>
        /// The entity.
        /// </value>
        public IEntity entity
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the timestamp.
        /// </summary>
        /// <value>
        /// The timestamp.
        /// </value>
        public float timestamp
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <value>
        /// The position.
        /// </value>
        public Vector3 position
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the observed entity is visible.
        /// </summary>
        /// <value>
        /// <c>true</c> if the observed entity is visible; otherwise, <c>false</c>.
        /// </value>
        public bool isVisible
        {
            get;
            set;
        }
    }
}