namespace Meli.Data
{
    public abstract class Entity
    {
        #region Properties

        /// <summary>
        /// Defines creation date for domain entity.
        /// </summary>
        public virtual DateTime CreationDate { get; set; }
        /// <summary>
        /// Defines modified date for domain entity.
        /// </summary>
        public virtual DateTime? ModifiedDate { get; set; }

        #endregion
    }

    public abstract class Entity<TEntityId> : Entity
    {
        #region Members & Properties

        /// <summary>
        /// Defines the identifier for domain class
        /// </summary>
        public virtual TEntityId Id { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Use to update creation/modified date for domain entity
        /// </summary>
        public virtual void UpdateModifiedDate()
        {
            if (this.CreationDate != default(DateTime))
            {
                this.ModifiedDate = DateTime.UtcNow;
            }
        }

        #endregion

    }
}