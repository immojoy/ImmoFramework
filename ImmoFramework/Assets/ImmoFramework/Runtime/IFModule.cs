

namespace ImmoFramework.Runtime
{
    public abstract class IFModule
    {
        /// <summary>
        /// Gets the priority of the module.
        /// </summary>
        public virtual int Priority => 0;


        /// <summary>
        /// Updates the module.
        /// </summary>
        /// <param name="elapsedSeconds"> Logical elapsed time since last update.</param>
        /// <param name="realElapsedSeconds"> Real elapsed time since last update.</param>
        /// <remarks> 
        /// `elapsedSeconds` is affected by time scaling for certain game logic, for example, slow motion.<\br>
        /// while `realElapsedSeconds` is not.
        /// </remarks>
        public abstract void Update(float elapsedSeconds, float realElapsedSeconds);


        /// <summary>
        /// Shuts down and clean the module.
        /// </summary>
        public abstract void Shutdown();
    }
}